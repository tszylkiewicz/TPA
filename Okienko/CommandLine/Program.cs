using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            //FileManager fileManager = new FileManager("C:\\Users\\Marcin\\Downloads\\TP-master\\TP-master\\TPA\\ApplicationArchitecture\\bin\\Debug\\TPA.ApplicationArchitecture.dll");

            //string road = "";

            //Uri uri = new Uri("C:\\Users\\Marcin\\Downloads\\TP - master\\TP - master\\TPA\\ApplicationArchitecture\\bin\\Debug\\TPA.ApplicationArchitecture.dll");
            Console.WriteLine("Podaj sciezke:");
            //String url = Console.ReadLine();
            Uri uri = new Uri("C:\\Users\\Marcin\\Documents\\TPA.ApplicationArchitecture.dll");
           // Uri uri = new Uri(url);
            FileManager fileManager = new FileManager(uri);

            fileManager.OpenFile();

            //Console.WriteLine(uri.);

            Console.WriteLine("END");
            Console.ReadLine();
        }
    }
}
