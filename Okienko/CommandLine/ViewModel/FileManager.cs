using Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLine.ViewModel
{
    public class FileManager
    {
        LogWriter logWriter;
        public string PathVariable { get; set; }

        public Reflector reflector { get; set; }

        public Uri Uri { get; set; }

        public FileManager(Uri uri)
        {
            logWriter = new LogWriter("Start: FileManager");
            PathVariable = uri.LocalPath;
            Uri = uri;
            reflector = new Reflector();
        }     

        public FileManager(string path)
        {
            logWriter = new LogWriter("Start: FileManager");
            PathVariable = path;
            reflector = new Reflector();
        }

        public bool CreateUri(string path)
        {
            bool check = true;
            try
            {
                Uri = new Uri(path);
            }
            catch (UriFormatException error)
            {
                Console.WriteLine("ERROR - WRONG PATH : " + error.Message);
                check = false;
            }
            return check;
        }

        public void OpenFile()
        {
            logWriter.LogWrite("Start: FileManger.OpenFile");
            if (Uri.IsFile)
            {
                Console.WriteLine("Otwieram\n\n");
   
                if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
                {
                    reflector.Reflect(PathVariable);                    
                }
                else
                {
                    Console.WriteLine("URI IS NOT DLL FILE");
                    return;
                }

                char chosenOne;
                TypeMetadata tempType;

                Reflect();
                Console.WriteLine();
                Console.WriteLine("What to do (E-Exit, 'Number' - show property with number): ");
                chosenOne = Console.ReadKey().KeyChar;
                logWriter.LogWrite("FileManager.OpenFile: User_Input: " + chosenOne);
                Console.WriteLine();

                while ((chosenOne != 'E') && (chosenOne != 'e'))
                {
                    if (((Char.GetNumericValue(chosenOne)) >= 0) && ((Char.GetNumericValue(chosenOne)) <= 9))
                    {
                        tempType = FindTypeWithNumber(Char.GetNumericValue(chosenOne));

                        if (tempType != null)
                        {
                            More(FindTypeWithNumber(Char.GetNumericValue(chosenOne)), "");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("WRONG NUMBER - ERROR");
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("WRONG NUMBER - ERROR");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }

                    Console.WriteLine();

                    Reflect();
                    Console.WriteLine();
                    Console.WriteLine("What to do (E-Exit, 'Numer' - show property with numer): ");
                    chosenOne = Console.ReadKey().KeyChar;
                    logWriter.LogWrite("FileManager.OpenFile: User_Input: " + chosenOne);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("URI IS NOT FILE");
            }
            logWriter.LogWrite("FileManager.OpenFile: Closing");
        }

        public void More(TypeMetadata type, string offset)
        {
            logWriter.LogWrite("Start: FileManager.More");
            logWriter.LogWrite("More: Searched_Type" + type.m_NamespaceName + "_" + type.m_typeName + "_" + type.ToString());
            char anotherChoice;
            TypeMetadata temp;
            ReflectType(type, offset);
            Console.WriteLine();
            Console.WriteLine("What to do now (B-Back, 'Numer' - show property with numer): ");
            anotherChoice = Console.ReadKey().KeyChar;
            logWriter.LogWrite("FileManager.More: User_Input: " + anotherChoice);
            Console.WriteLine();

            while (((anotherChoice != 'B') && (anotherChoice != 'b')))
            {
                if (((Char.GetNumericValue(anotherChoice)) >= 0) && ((Char.GetNumericValue(anotherChoice)) <= 9))
                {
                    temp = FindPropertyWithNumber(Char.GetNumericValue(anotherChoice), type);
                    if (temp != null)
                    {
                        More(temp, offset + "\t");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("WRONG NUMBER");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("WRONG NUMBER");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }

                ReflectType(type, offset);
                Console.WriteLine();
                Console.WriteLine("What to do now (B-Back, 'Numer' - show property with numer): ");
                anotherChoice = Console.ReadKey().KeyChar;
                logWriter.LogWrite("FileManager.More: User_Input: " + anotherChoice);
                Console.WriteLine();
            }
            logWriter.LogWrite("Stop: FileManager.More");
            return;
        }

        public TypeMetadata FindTypeWithNumber(double Number)
        {
            logWriter.LogWrite("Start: FileManager.FindTypeWithNumber");
            int i = 1;
            foreach (TypeMetadata type in reflector.m_AssemblyModel.Types)
            {
                if (i++ == Number)
                {
                    logWriter.LogWrite("Stop: FileManager.FindTypeWithNumber");
                    return type;
                }
            }
            logWriter.LogWrite("Stop: FileManager.FindTypeWithNumber");
            return null;
        }

        public TypeMetadata FindPropertyWithNumber(double Number, TypeMetadata type)
        {
            logWriter.LogWrite("Start: FileManager.FindPropertyWithNumber");
            int i = 1;
            foreach (PropertyMetadata property in type.Properties)
            {
                if (i == Number)
                {
                    foreach (TypeMetadata typee in reflector.m_AssemblyModel.Types)
                    {
                        if(typee.m_typeName.ToString() == property.m_TypeMetadata.m_typeName.ToString())
                        {
                            logWriter.LogWrite("Stop: FileManager.FindPropertyWithNumber");
                            return typee;
                        }
                    }
                }
                i++;
            }
            logWriter.LogWrite("Stop: FileManager.FindPropertyWithNumber");
            return null;
        }

        public bool Reflect()
        {
            logWriter.LogWrite("Start: FileManager.Reflect");
            Console.WriteLine(PathVariable);
            Console.WriteLine();
            Console.WriteLine(reflector.m_AssemblyModel.m_Name);

            int i = 1;
            foreach (TypeMetadata type in reflector.m_AssemblyModel.Types)
            {
                Console.WriteLine(i++ + ".\t" + type.m_NamespaceName + " : " + type.m_typeName);
            }
            logWriter.LogWrite("Stop: FileManager.Reflect");

            return true;
        }

        public void ReflectType(TypeMetadata type, string offset)
        {
            logWriter.LogWrite("Start: FileManager.ReflectType");
            int j = 1;
            Console.WriteLine(offset + "\t" + type.m_NamespaceName + " : " + type.m_typeName);
            foreach (PropertyMetadata property in type.Properties)
            {
                Console.WriteLine(offset + "\t" + j++ +  ". " + property.m_Name);
            }
            logWriter.LogWrite("Stop: FileManager.Reflect");
        }     
    }
}