using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

            Assert.AreEqual(reflector.m_AssemblyModel.m_Name, "DataToTest.dll");
        }

        [TestMethod]
        public void NamespacesTest()
        {
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            Reflector reflector = new Reflector();
            reflector.Reflect(path);

            List<NamespaceMetadata> namespaces = new List<NamespaceMetadata>(reflector.m_AssemblyModel.Namespaces);

            Assert.AreEqual(namespaces[0].m_NamespaceName, "DataToTest");
        }

        [TestMethod]
        public void TypesTest()
        {
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            Reflector reflector = new Reflector();
            reflector.Reflect(path);

            List<TypeMetadata> types = new List<TypeMetadata>(reflector.m_AssemblyModel.Types);

            Assert.AreEqual(types[0].m_typeName, "ClassA");
            Assert.AreEqual(types[1].m_typeName, "ClassB");
            Assert.AreEqual(types[2].m_typeName, "ClassBurgerKing");
            Assert.AreEqual(types[3].m_typeName, "ClassC");
            Assert.AreEqual(types[4].m_typeName, "ClassKFC");
            Assert.AreEqual(types[5].m_typeName, "ClassMcDonald");
        }

        [TestMethod]
        public void PropertiesTest()
        {
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            Reflector reflector = new Reflector();
            reflector.Reflect(path);

            List<TypeMetadata> types = new List<TypeMetadata>(reflector.m_AssemblyModel.Types);
            List<PropertyMetadata> properties = new List<PropertyMetadata>();

            foreach (TypeMetadata type in types)
            {
                if (type.Properties.Count > 0)
                {
                    properties.Add(type.Properties[0]);
                }
            }

            Assert.AreEqual(properties[0].m_Name, "classB");
            Assert.AreEqual(properties[1].m_Name, "classC");
            Assert.AreEqual(properties[2].m_Name, "ClassA");
            Assert.AreEqual(properties[3].m_Name, "ClassKFC");
        }
    }
}
