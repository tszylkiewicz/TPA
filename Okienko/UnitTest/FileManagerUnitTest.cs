using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandLine.ViewModel;
using Model.Model;

namespace UnitTest
{
    [TestClass]
    public class FileManagerUnitTest
    {
        [TestMethod]
        public void CreateUriTest()
        {
            FileManager fileManager = new FileManager("CCC");
            Assert.IsFalse(fileManager.CreateUri("CCC"));
        }

        [TestMethod]
        public void FindTypeWithNumberTest()
        {
            FileManager fileManager;
            Uri uri = new Uri("C:\\Users\\Marcin\\Documents\\TPA.ApplicationArchitecture.dll");
            fileManager = new FileManager(uri);
            fileManager.reflector.Reflect(uri.LocalPath);
            TypeMetadata metadata = fileManager.FindTypeWithNumber(1);
            Assert.AreEqual(metadata.m_typeName, "View");
        }

        [TestMethod]
        public void FindPropertyWithNumberTest()
        {
            FileManager fileManager;
            Uri uri = new Uri("C:\\Users\\Marcin\\Documents\\TPA.ApplicationArchitecture.dll");
            fileManager = new FileManager(uri);
            fileManager.reflector.Reflect(uri.LocalPath);
            TypeMetadata metadata = fileManager.FindTypeWithNumber(4);
            TypeMetadata property = fileManager.FindPropertyWithNumber(1, metadata);
            Assert.AreEqual(property.m_typeName, "ServiceC");
        }

        [TestMethod]
        public void ReflectTest()
        {
            FileManager fileManager;
            Uri uri = new Uri("C:\\Users\\Marcin\\Documents\\TPA.ApplicationArchitecture.dll");
            fileManager = new FileManager(uri);
            fileManager.reflector.Reflect(uri.LocalPath);
            Assert.IsTrue(fileManager.Reflect());           
        }
    }
}
