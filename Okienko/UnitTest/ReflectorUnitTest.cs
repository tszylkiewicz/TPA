using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Model;
using Model.ViewModel;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class ReflectorUnitTest
    {
        [TestMethod]
        public void AssemblyTest()
        {
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            Reflector reflector = new Reflector();
            reflector.Reflect(path);

            Assert.AreEqual(reflector.AssemblyModel.Name, "DataToTest.dll");
        }

        [TestMethod]
        public void NamespacesTest()
        {
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            Reflector reflector = new Reflector();
            reflector.Reflect(path);

            List<NamespaceMetadata> namespaces = new List<NamespaceMetadata>(reflector.AssemblyModel.Namespaces);

            Assert.AreEqual(namespaces[0].Name, "DataToTest");
        }

        [TestMethod]
        public void TypesTest()
        {
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            Reflector reflector = new Reflector();
            reflector.Reflect(path);

            List<TypeMetadata> types = new List<TypeMetadata>();

            foreach (NamespaceMetadata namespaces in reflector.AssemblyModel.Namespaces)
            {
                foreach (TypeMetadata typeMetadata in namespaces.Types)
                {
                    types.Add(typeMetadata);
                }
            }

            Assert.AreEqual(types[0].Name, "ClassA");
            Assert.AreEqual(types[1].Name, "ClassB");
            Assert.AreEqual(types[2].Name, "ClassBurgerKing");
            Assert.AreEqual(types[3].Name, "ClassC");
            Assert.AreEqual(types[4].Name, "ClassKFC");
            Assert.AreEqual(types[5].Name, "ClassMcDonald");
        }

        [TestMethod]
        public void PropertiesTest()
        {
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            Reflector reflector = new Reflector();
            reflector.Reflect(path);

            List<TypeMetadata> types = new List<TypeMetadata>();

            foreach (NamespaceMetadata namespaces in reflector.AssemblyModel.Namespaces)
            {
                foreach (TypeMetadata typeMetadata in namespaces.Types)
                {
                    types.Add(typeMetadata);
                }
            }

            List<PropertyMetadata> properties = new List<PropertyMetadata>();

            foreach (TypeMetadata type in types)
            {
                if (type.Properties.Count > 0)
                {
                    properties.Add(type.Properties[0]);
                }
            }

            Assert.AreEqual(properties[0].Name, "classB");
            Assert.AreEqual(properties[1].Name, "classC");
            Assert.AreEqual(properties[2].Name, "classA");
            Assert.AreEqual(properties[3].Name, "classKFC");
        }
    }
}
