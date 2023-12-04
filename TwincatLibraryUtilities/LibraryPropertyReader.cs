using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwincatLibraryUtilities
{
    public class LibraryPropertyReader
    {
        const string ProjectInfoGuid = @"$11c0fc3a-9bcf-4dd8-ac38-efb93363e521";

        public LibraryInfo getLibraryInfo(string libraryFile)
        {
            LibraryInfo libraryInfo = new LibraryInfo();

            List<string> properties = getPropertyList(libraryFile);

            libraryInfo.Name = getPropertyFromList("DefaultNameSpace",properties);
            libraryInfo.Description = getPropertyFromList("Description", properties);
            libraryInfo.Author = getPropertyFromList("Author", properties);
            libraryInfo.Company = getPropertyFromList("Company", properties);
            libraryInfo.Version = getPropertyFromList("Version", properties);

            return libraryInfo;

        }

        public string getPropertyFromList(string propertyName, List<string> propertyList)
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

        public List<string> getPropertyList(string libraryFile)
        {
            byte[] fileData = File.ReadAllBytes(libraryFile);
            string fileText = System.Text.Encoding.ASCII.GetString(fileData);

            int filePosition = fileText.IndexOf(ProjectInfoGuid) - 1;
            if (filePosition < 0)
            {
                throw new Exception($"Unable to locate Project Info GUID {ProjectInfoGuid} in library file");
            }


            List<string> properties = new List<string>();
            int valueLength = 0;
            int index = 0;
            

            while(filePosition < fileData.Length-1)
            {
                index = fileData[filePosition++];
                valueLength = fileData[filePosition++];
                if (filePosition + valueLength < fileData.Length)
                {
                    properties.Add(Encoding.UTF8.GetString(fileData, filePosition, valueLength));
                    filePosition += valueLength;
                }
            }

            return properties;
        }
    }
}
