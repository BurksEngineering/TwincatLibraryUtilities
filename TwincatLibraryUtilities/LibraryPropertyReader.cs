using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwincatLibraryUtilities
{
    public static class LibraryPropertyReader
    {
        const string ProjectInfoGuid = @"11c0fc3a-9bcf-4dd8-ac38-efb93363e521";
        const string LibraryManagerGuid = @"adb5cb65-8e1d-4a00-b70a-375ea27582f3";

        public static LibraryInfo getLibraryInfo(string libraryFile)
        {
            LibraryInfo libraryInfo = new LibraryInfo();

            List<string> properties = getPropertyList(libraryFile);

            libraryInfo.Name = getPropertyFromList("DefaultNameSpace",properties);
            libraryInfo.Description = getPropertyFromList("Description", properties);
            libraryInfo.Author = getPropertyFromList("Author", properties);
            libraryInfo.Company = getPropertyFromList("Company", properties);
            libraryInfo.Version = getPropertyFromList("Version", properties);
            libraryInfo.Dependencies = GetLibraryDependenciesFromList(properties);

            return libraryInfo;

        }

        public static string getPropertyFromList(string propertyName, List<string> propertyList)
        {
            int index = propertyList.FindIndex(p => String.Equals(p, propertyName, StringComparison.OrdinalIgnoreCase))+1;
            if(index > 0 && index < propertyList.Count)
            {
                return propertyList[index];
            } else
            {
                return "";
            }
        }

        public static List<LibraryDependency> GetLibraryDependenciesFromList(List<string> propertyList)
        {
            int index = propertyList.FindIndex(p => String.Equals(p, LibraryManagerGuid, StringComparison.OrdinalIgnoreCase)) + 1;
            List<LibraryDependency> dependencies = new List<LibraryDependency>();
            
            const string referencePattern = @"(\S+), (\S+) \((.+)\)";
            Regex rx = new Regex(referencePattern);

            int skippedProperties = 0;

            while (index < (propertyList.Count-1) && skippedProperties < 2)
            {
                if(!rx.IsMatch(propertyList[index]))
                {
                    skippedProperties++;
                    index++;
                    continue;
                }
                Match match = rx.Match(propertyList[index]);
                LibraryDependency dependency = getLibraryDependencyFromMatch(match, propertyList[index + 1]);

                //check for version-pegged dependencies
                if (index+2 < propertyList.Count && !dependency.isSpecificVersion && rx.IsMatch(propertyList[index+2]))
                {
                    match = rx.Match(propertyList[index+2]);
                    LibraryDependency peggedDependency = getLibraryDependencyFromMatch(match, propertyList[index + 1]);
                    if(peggedDependency.isSpecificVersion && peggedDependency.isDifferentVersionOf(dependency))
                    {
                        dependency = peggedDependency;
                        index++;
                    }
                }

                dependencies.Add(dependency);
                index += 2;
                skippedProperties = 0;
            }

            return dependencies;
        }

        static LibraryDependency getLibraryDependencyFromMatch(Match match, string @namespace)
        {
            return new LibraryDependency()
            {
                Namespace = @namespace,
                Name = match.Groups[1].Value,
                Version = match.Groups[2].Value,
                Company = match.Groups[3].Value
            };
        }

        public static List<string> getPropertyList(string libraryFile)
        {
            byte[] fileData = File.ReadAllBytes(libraryFile);
            string fileText = System.Text.Encoding.ASCII.GetString(fileData);

            int filePosition = fileText.IndexOf(@"$"+ProjectInfoGuid) - 1;
            if (filePosition < 0)
            {
                throw new Exception($"Unable to locate Project Info GUID {ProjectInfoGuid} in library file");
            }


            List<string> properties = new List<string>();
            int valueLength = 0;
            int index = -1;
            int nextIndex = 0;

            while(filePosition < fileData.Length-1)
            {
                nextIndex = parseNumber(fileData, ref filePosition);
                if (index +1 != nextIndex)
                {
                    break;
                }
                index = nextIndex;
                valueLength = parseNumber(fileData, ref filePosition);
                if (filePosition + valueLength < fileData.Length)
                {
                    properties.Add(Encoding.UTF8.GetString(fileData, filePosition, valueLength));
                    filePosition += valueLength;
                }
            }

            return properties;
        }

        public static int parseNumber(byte[] buffer, ref int filePosition)
        {
            int value = buffer[filePosition++];
            if(value >= 128)
            {
                value += 128 * (buffer[filePosition++] - 1);
            }
            return value;
        }
    }
}
