using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandLine.View;
using Model.Model;
using System.Collections.ObjectModel;
using Model.ViewModel;
using System.Collections.Generic;

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
            fileManager.treeViewAssembly = new TreeViewAssembly(fileManager.reflector.AssemblyModel);

            TreeViewType testTreeView = null;

            foreach (TreeViewNamespace treeViewNamespace in fileManager.GetNamespaces())
            {
                foreach (TreeViewType treeViewType in fileManager.GetTypes(treeViewNamespace))
                {
                    testTreeView = treeViewType;
                }
            }

            Assert.AreEqual(testTreeView.Name, "ClassMcDonald");
        }

        [TestMethod]
        public void FindPropertyWithNumberTest()
        {
            FileManager fileManager;
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            fileManager = new FileManager(path);
            fileManager.reflector.Reflect(path);
            fileManager.treeViewAssembly = new TreeViewAssembly(fileManager.reflector.AssemblyModel);

            TreeViewItem testTreeView = null;

            foreach (TreeViewNamespace treeViewNamespace in fileManager.GetNamespaces())
            {
                foreach (TreeViewType treeViewType in fileManager.GetTypes(treeViewNamespace))
                {
                    testTreeView = fileManager.FindPropertyWithNumber(1, treeViewType);
                }
            }
            Assert.AreEqual(testTreeView.Name, "ClassKFC");
        }

        [TestMethod]
        public void ReflectTest()
        {
            FileManager fileManager;
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            fileManager = new FileManager(path);
            fileManager.reflector.Reflect(path);
            fileManager.treeViewAssembly = new TreeViewAssembly(fileManager.reflector.AssemblyModel);
            Assert.IsTrue(fileManager.ReflectNamespace());
        }

        [TestMethod]
        public void GetNamespacesTest()
        {
            FileManager fileManager;
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            fileManager = new FileManager(path);
            fileManager.reflector.Reflect(path);
            fileManager.treeViewAssembly = new TreeViewAssembly(fileManager.reflector.AssemblyModel);

            ObservableCollection<TreeViewItem> treeViewItems = fileManager.GetNamespaces();
            Assert.AreEqual(treeViewItems[0].Name, "DataToTest");
        }
        [TestMethod]
        public void GetTypesTest()
        {
            FileManager fileManager;
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            fileManager = new FileManager(path);
            fileManager.reflector.Reflect(path);
            fileManager.treeViewAssembly = new TreeViewAssembly(fileManager.reflector.AssemblyModel);

            ObservableCollection<TreeViewItem> treeViewItems = null;

            foreach (TreeViewNamespace treeViewNamespace in fileManager.GetNamespaces())
            {
                treeViewItems = fileManager.GetTypes(treeViewNamespace);
            }

            Assert.AreEqual(treeViewItems[0].Name, "ClassA");
        }
        [TestMethod]
        public void GetPropertyTest()
        {
            FileManager fileManager;
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            fileManager = new FileManager(path);
            fileManager.reflector.Reflect(path);
            fileManager.treeViewAssembly = new TreeViewAssembly(fileManager.reflector.AssemblyModel);

            ObservableCollection<TreeViewItem> treeViewItems = null;

            foreach (TreeViewNamespace treeViewNamespace in fileManager.GetNamespaces())
            {
                foreach (TreeViewType treeViewType in fileManager.GetTypes(treeViewNamespace))
                {
                    treeViewItems = fileManager.GetProperty(treeViewType);
                }
            }

            Assert.AreEqual(treeViewItems[0].Name, "classKFC");
        }
    }
}
