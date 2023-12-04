using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace TwincatLibraryTests
{
    [TestClass]
    public class LibraryPropertyReaderTests
    {
        [TestMethod]
        [DeploymentItem(@"ExampleLibraries\TxMatrix.library")]
        public void TcMatrixTest()
        {
            var libraryInfo = TwincatLibraryUtilities.LibraryPropertyReader.getLibraryInfo(@"ExampleLibraries\TcMatrix.library");

            Assert.AreEqual("TcMatrix", libraryInfo.Name);
            Assert.AreEqual("Matrix arithmatic library", libraryInfo.Description);
            Assert.AreEqual("Andrew Burks", libraryInfo.Author);
            Assert.AreEqual("Burks Engineering", libraryInfo.Company);
            Assert.AreEqual("1.4.3", libraryInfo.Version);

            Assert.AreEqual(4,libraryInfo.Dependencies.Count);
        }

        [TestMethod]
        [DeploymentItem(@"ExampleLibraries\TxTransform.library")]
        public void TcTransformTest()
        {
            var libraryInfo = TwincatLibraryUtilities.LibraryPropertyReader.getLibraryInfo(@"ExampleLibraries\TcTransform.library");

            Assert.AreEqual("TcTransform", libraryInfo.Name);
            Assert.AreEqual("Coordinate system transformation library", libraryInfo.Description);
            Assert.AreEqual("Andrew Burks", libraryInfo.Author);
            Assert.AreEqual("Burks Engineering", libraryInfo.Company);
            Assert.AreEqual("0.3.36", libraryInfo.Version);

            var tcMatrixDependency = libraryInfo.Dependencies.FirstOrDefault(d => string.Equals("TcMatrix", d.Name, System.StringComparison.OrdinalIgnoreCase));
            Assert.IsNotNull(tcMatrixDependency);
            Assert.AreEqual("1.4.3", tcMatrixDependency.Version);
            Assert.AreEqual("Burks Engineering", tcMatrixDependency.Company);
            Assert.AreEqual("TcMatrix", tcMatrixDependency.Namespace);
        }
    }
}