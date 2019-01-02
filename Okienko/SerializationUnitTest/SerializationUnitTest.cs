using BaseModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;

namespace UnitTest
{
    [TestClass]
    public class SerializationUnitTest
    {
        [TestMethod]
        public void SerializationTest()
        {
            ISerializer Serializer = new XMLSerializer.XMLSerializer();
            string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            Serializer.Serialize("test.xml", path);
            Assert.IsTrue(File.Exists("test.xml"));
        }
    }
}
