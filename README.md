# TwinCAT Library Utilities

Tools for working with TwinCAT compiled libraries

## Library Property Reader

Used to read a few pieces of metadata from a TwinCAT library:
* Name (Namespace)
* Description
* Author
* Company
* Version

Or to read the dependencies of the library:
* Name
* Namespace
* Company
* Version

### Usage

See the [test file](TwincatLibraryTests/LibraryPropertyReaderTests.cs) with this example:

```c#
var propertyReader = new TwincatLibraryUtilities.LibraryPropertyReader();
var libraryInfo = propertyReader.getLibraryInfo(@"ExampleLibraries\TcMatrix.library");

Assert.AreEqual("TcMatrix", libraryInfo.Name);
Assert.AreEqual("Matrix arithmatic library", libraryInfo.Description);
Assert.AreEqual("Andrew Burks", libraryInfo.Author);
Assert.AreEqual("Burks Engineering", libraryInfo.Company);
Assert.AreEqual("1.4.3", libraryInfo.Version);
```