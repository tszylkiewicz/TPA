using CommandLine.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Model;
using Model.Singleton;
using Model.ViewModel;
using Model.ViewModel.TreeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class SingletonDictionaryUnitTest
    {
        [TestMethod]
        public void ContainsKeyTest()
        {
            FileManager fileManager;
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            fileManager = new FileManager(path);
            fileManager.reflector.Reflect(path);
            fileManager.treeViewAssembly = new TreeViewAssembly(fileManager.reflector.AssemblyModel);
            TreeViewItem metadata = fileManager.FindTypeWithNumber(1);       
            Assert.IsTrue(SingletonDictionary.Instance.ContainsKey("ClassA"));
        }

        [TestMethod]
        public void GetTest()
        {
            FileManager fileManager;
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            fileManager = new FileManager(path);
            fileManager.reflector.Reflect(path);
            fileManager.treeViewAssembly = new TreeViewAssembly(fileManager.reflector.AssemblyModel);
            TypeMetadata metadata = SingletonDictionary.Instance.Get("ClassA");
            Assert.AreEqual(metadata.Name, "ClassA");
        }
    }
}
