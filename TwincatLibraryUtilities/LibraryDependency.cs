namespace TwincatLibraryUtilities
{
    public class LibraryDependency
    {
        public string Namespace { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Company {  get; set; }

        public bool isSpecificVersion { get { return !String.Equals("*",Version); } }
        public bool isDifferentVersionOf(LibraryDependency other)
        {
            return equalNamesAndCompany(other) && !String.Equals(Version, other.Version);
        }
        public bool isEqual(LibraryDependency other)
        {
            return equalNamesAndCompany(other) && String.Equals(Version, other.Version);
        }

        bool equalNamesAndCompany(LibraryDependency other)
        {
            if (other == null) return false;
            if (!String.Equals(Namespace, other.Namespace)) return false;
            if (!String.Equals(Name, other.Name)) return false;
            if (!String.Equals(Company, other.Company)) return false;
            return true;
        }
        
    }
}
