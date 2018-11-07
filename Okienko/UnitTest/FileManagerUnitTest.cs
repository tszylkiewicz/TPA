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
        public void FindTypeWithNumberTest()
        {
            FileManager fileManager;
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            fileManager = new FileManager(path);
            fileManager.reflector.Reflect(path);
            TypeMetadata metadata = fileManager.FindTypeWithNumber(1);
            Assert.AreEqual(metadata.m_typeName, "ClassA");
        }

        [TestMethod]
        public void FindPropertyWithNumberTest()
        {
            FileManager fileManager;
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            fileManager = new FileManager(path);
            fileManager.reflector.Reflect(path);
            TypeMetadata metadata = fileManager.FindTypeWithNumber(4);
            TypeMetadata property = fileManager.FindPropertyWithNumber(1, metadata);
            Assert.AreEqual(property.m_typeName, "ClassA");
        }

        [TestMethod]
        public void ReflectTest()
        {
            FileManager fileManager;
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            fileManager = new FileManager(path);
            fileManager.reflector.Reflect(path);
            Assert.IsTrue(fileManager.Reflect());
        }
    }
}
