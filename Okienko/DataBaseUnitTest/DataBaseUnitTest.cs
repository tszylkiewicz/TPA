using System;
using System.Configuration;
using DataBaseModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.ViewModel;

namespace DataBaseUnitTest
{
    [TestClass]
    public class DataBaseUnitTest
    {
        [TestMethod]
        public void DataBaseLoadTest()
        {
            MyViewModel myViewModel = new MyViewModel();
            myViewModel.PathVariable = ConfigurationManager.AppSettings["AADllPath"];
            Console.WriteLine(myViewModel.PathVariable);
            myViewModel.LoadDLL();
            myViewModel.Save();

            DataBaseData dataBaseData = new DataBaseData();

            DataBaseAssembly aaa = (DataBaseAssembly)dataBaseData.Read(myViewModel.PathForSerialization);
            Assert.AreEqual(aaa.Name, "TPA.ApplicationArchitecture.dll");
        }
    }
}
