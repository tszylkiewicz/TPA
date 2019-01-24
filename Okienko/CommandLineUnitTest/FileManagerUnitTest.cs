using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandLine.View;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Configuration;

namespace UnitTest
{
    [TestClass]
    public class FileManagerUnitTest
    {
        [TestMethod]
        public void OpenFileTest()
        {
            FileManager fileManager;
            string path = "fileNiedobry";
            fileManager = new FileManager(path);
            Assert.IsFalse(fileManager.OpenFile());
        }

        [TestMethod]
        public void FileManagerTest()
        {
            FileManager fileManager;
            string path = ConfigurationManager.AppSettings["AADllPath"];
            fileManager = new FileManager(path);
            Assert.AreEqual(fileManager.getPath(), path);
        }
    }
}
