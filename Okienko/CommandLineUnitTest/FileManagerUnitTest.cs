using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandLine.View;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class FileManagerUnitTest
    {
        [TestMethod]
        public void OpenFileTest()
        {
            FileManager fileManager;
            string path = "file";
            fileManager = new FileManager(path);
            Assert.IsFalse(fileManager.OpenFile());
        }

        [TestMethod]
        public void FileManagerTest()
        {
            FileManager fileManager;
            string path = @"..\..\..\DataToTest\bin\Debug\DataToTest.dll";
            fileManager = new FileManager(path);
            //Assert.AreEqual(fileManager.PathVariable, path);      // tymczasowo
            Assert.AreEqual("aaa", path);
        }
    }
}
