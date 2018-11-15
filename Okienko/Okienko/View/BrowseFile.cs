using Microsoft.Win32;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Okienko.View
{
    public class BrowseFile : IBrowseFile
    {
        string PathVariable;
        public string ChooseFile()
        {
            OpenFileDialog test = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll"
            };
            test.ShowDialog();
            if (test.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
            }
            else
            {
                PathVariable = test.FileName;
                return PathVariable;
            }
            return null;
        }
    }
}
