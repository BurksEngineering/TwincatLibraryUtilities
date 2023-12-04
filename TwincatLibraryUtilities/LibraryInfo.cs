namespace TwincatLibraryUtilities
{
    public class LibraryInfo
    {
        //DefaultNamespace
        public string Name { get; set; }

        //Description
        public string Description { get; set; }

        //Author
        public string Author { get; set; }

        //Comapny
        public string Company { get; set; }

        //Version
        public string Version { get; set; }

        public List<LibraryDependency> Dependencies { get; set; }

    }
}