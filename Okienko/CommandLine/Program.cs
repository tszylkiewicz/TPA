using CommandLine.ViewModel;
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
            LogWriter logWriter = new LogWriter("Start: CommandLine.exe");
            logWriter.LogWrite("Start: Program");

            Console.WriteLine("Path:");
            string url = Console.ReadLine();
            logWriter.LogWrite("Program: User_Input: " + url);

            FileManager fileManager;

            if (url.Equals("21"))
            {
                Uri uri = new Uri("C:\\Users\\Marcin\\Documents\\GitHub\\TPA\\Okienko\\DataToTest\\bin\\Debug\\DataToTest.dll");
                //Uri uri = new Uri("C:\\Users\\Marcin\\Documents\\TPA.ApplicationArchitecture.dll");
                fileManager = new FileManager(uri);
                logWriter.LogWrite("Program: Fixing_Path: " + "C:\\Users\\Marcin\\Documents\\TPA.ApplicationArchitecture.dll");
            }
            else
            {
                fileManager = new FileManager(url);
                if (fileManager.CreateUri(url) == false)
                {
                    Console.WriteLine("This path is wrong, closing program");
                    logWriter.LogWrite("Program: User_Input: Wrong Path");
                    return;
                }
            }
            fileManager.OpenFile();
            Console.WriteLine("END");
            Console.ReadLine();
            logWriter.LogWrite("Stop: Program");
        }
    }
}
