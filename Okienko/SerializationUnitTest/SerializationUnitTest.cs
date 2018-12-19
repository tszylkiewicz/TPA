using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.ViewModel;
using Serialization;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class SerializationUnitTest
    {
        [TestMethod]
        public void SerializationTest()
        {
            ISerializer Serializer = new XMLSerializer();
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            Reflector reflector = new Reflector();
            reflector.Reflect(path);
            Serializer.Serialize("test.xml", reflector.AssemblyModel);
            Assert.IsTrue(File.Exists("test.xml"));
        }
    }
}
