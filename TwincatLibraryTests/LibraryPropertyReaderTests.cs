using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TwincatLibraryTests
{
    [TestClass]
    public class LibraryPropertyReaderTests
    {
        [TestMethod]
        [DeploymentItem(@"ExampleLibraries\TxMatrix.library")]
        public void TcMatrixTest()
        {
            var propertyReader = new TwincatLibraryUtilities.LibraryPropertyReader();
            var libraryInfo = propertyReader.getLibraryInfo(@"ExampleLibraries\TcMatrix.library");

            Assert.AreEqual("TcMatrix", libraryInfo.Name);
            Assert.AreEqual("Matrix arithmatic library", libraryInfo.Description);
            Assert.AreEqual("Andrew Burks", libraryInfo.Author);
            Assert.AreEqual("Burks Engineering", libraryInfo.Company);
            Assert.AreEqual("1.4.3", libraryInfo.Version);
        }

        [TestMethod]
        [DeploymentItem(@"ExampleLibraries\TxTransform.library")]
        public void TcTransformTest()
        {
            var propertyReader = new TwincatLibraryUtilities.LibraryPropertyReader();
            var libraryInfo = propertyReader.getLibraryInfo(@"ExampleLibraries\TcTransform.library");

            Assert.AreEqual("TcTransform", libraryInfo.Name);
            Assert.AreEqual("Coordinate system transformation library", libraryInfo.Description);
            Assert.AreEqual("Andrew Burks", libraryInfo.Author);
            Assert.AreEqual("Burks Engineering", libraryInfo.Company);
            Assert.AreEqual("0.3.36", libraryInfo.Version);
        }
    }
}