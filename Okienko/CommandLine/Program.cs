﻿using CommandLine.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Path:");
            string url = Console.ReadLine();
            FileManager fileManager;
           // string path = @"..\\..\\..\\DataToTest\\bin\\Debug\\DataToTest.dll";
            //string path2 = @"..\\..\\..\\AADllFile\\TPA.ApplicationArchitecture.dll";
            string path = ConfigurationManager.AppSettings["AADllPath"];

            if (url.Equals("21"))
            {
                fileManager = new FileManager(path);
            }
            else
            {
                fileManager = new FileManager(url);
            }
            fileManager.OpenFile();

            Console.WriteLine("END");
            Console.ReadLine();
        }

    }
}
