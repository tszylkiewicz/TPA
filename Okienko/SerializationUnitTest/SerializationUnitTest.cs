
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.ViewModel;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class SerializationUnitTest
    {
        [TestMethod]
        public void DeserializationTest()
        {
            MyViewModel myViewModel = new MyViewModel();
            myViewModel.PathVariable = @"..\..\..\DataToTest\bin\Debug\DataToTest.dll";
            myViewModel.LoadDLL();
            myViewModel.PathForSerialization = "textSave.xml";
            myViewModel.Save();
            Assert.IsTrue(File.Exists("textSave.xml"));
        }
    }
}
