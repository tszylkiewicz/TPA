using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Model;

namespace UnitTest
{
    [TestClass]
    public class ReflectorUnitTest
    {
        [TestMethod]
        public void ReflectTest()
        {
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            Reflector reflector = new Reflector();
            reflector.Reflect(path);

            int i = 1;
            TypeMetadata temp = null;
            PropertyMetadata property = null;

            foreach (TypeMetadata type in reflector.m_AssemblyModel.Types)
            {
                temp = type;
                foreach (PropertyMetadata prop in type.Properties)
                {
                   property = prop;
                }
            }

            Assert.AreEqual(reflector.m_AssemblyModel.m_Name, "DataToTest.dll");
            Assert.AreEqual(temp.m_typeName, "ClassMcDonald");
            Assert.AreEqual(property.m_Name, "ClassBurgerKing");
        }
    }
}
