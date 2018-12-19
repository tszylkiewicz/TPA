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
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            Reflector reflector = new Reflector();
            reflector.Reflect(path);
            TreeViewAssembly treeViewAssembly = new TreeViewAssembly(reflector.AssemblyModel);
            Assert.IsTrue(SingletonDictionary.Instance.ContainsKey("ClassA"));
        }

        [TestMethod]
        public void GetTest()
        {
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            Reflector reflector = new Reflector();
            reflector.Reflect(path);
            TreeViewAssembly treeViewAssembly = new TreeViewAssembly(reflector.AssemblyModel);
            TypeMetadata metadata = SingletonDictionary.Instance.Get("ClassA");
            Assert.AreEqual(metadata.Name, "ClassA");
        }
    }
}
