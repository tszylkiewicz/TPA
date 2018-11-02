using Okienko.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLine
{
    public class FileManager
    {
        LogWriter logWriter;
        public string PathVariable { get; set; }

        public Reflector reflector { get; set; }

        public FileManager(Uri uri)
        {
            logWriter = new LogWriter("FileManager_Class");
            PathVariable = uri.LocalPath;
            Uri = uri;
            reflector = new Reflector();
        }

        public Uri Uri { get; set; }

        public FileManager(string path)
        {
            PathVariable = path;
        }

        public void OpenFile()
        {
            logWriter.LogWrite("OpenFile_function");
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
                Console.WriteLine("What to do (E-Exit, 'Numer' - show property with numer): ");
                chosenOne = Console.ReadKey().KeyChar;
                Console.WriteLine();

                while ((chosenOne != 'E') && (chosenOne != 'e'))
                {
                    if (((Char.GetNumericValue(chosenOne)) >= 0) && ((Char.GetNumericValue(chosenOne)) <= 9))
                    {
                        tempType = FindTypeWithNumber(Char.GetNumericValue(chosenOne));

                        if (tempType != null)
                        {
                            more(FindTypeWithNumber(Char.GetNumericValue(chosenOne)), "");
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
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("URI IS NOT FILE");
            }
        }

        public void more(TypeMetadata type, string offset)
        {
            logWriter.LogWrite("more_function_TypeMetada:_" + type.m_NamespaceName + "_" + type.m_typeName + "_" + type.ToString());
            char anotherChoice;
            TypeMetadata temp;
            ReflectType(type, offset);
            Console.WriteLine();
            Console.WriteLine("What to do now (B-Back, 'Numer' - show property with numer): ");
            anotherChoice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            while (((anotherChoice != 'B') && (anotherChoice != 'b')))
            {
                if (((Char.GetNumericValue(anotherChoice)) >= 0) && ((Char.GetNumericValue(anotherChoice)) <= 9))
                {
                    temp = FindPropertyWithNumber(Char.GetNumericValue(anotherChoice), type);
                    if (temp != null)
                    {
                        more(temp, offset + "\t");
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
                Console.WriteLine();
            }
            return;
        }

        public TypeMetadata FindTypeWithNumber(double Number)
        {
            int i = 1;
            foreach (TypeMetadata type in reflector.m_AssemblyModel.Types)
            {
                if (i++ == Number)
                {
                    return type;
                }
            }
            return null;
        }

        public TypeMetadata FindPropertyWithNumber(double Number, TypeMetadata type)
        {
            int i = 1;
            foreach (PropertyMetadata property in type.Properties)
            {
                if (i == Number)
                {
                    foreach (TypeMetadata typee in reflector.m_AssemblyModel.Types)
                    {
                        if(typee.m_typeName.ToString() == property.m_Name.ToString())
                        {
                            return typee;
                        }
                    }
                }
            }
            return null;
        }

        public void Reflect()
        {     
            Console.WriteLine(PathVariable);
            Console.WriteLine();
            Console.WriteLine(reflector.m_AssemblyModel.m_Name);

            int i = 1;
            foreach (TypeMetadata type in reflector.m_AssemblyModel.Types)
            {
                Console.WriteLine(i++ + ".\t" + type.m_NamespaceName + " : " + type.m_typeName);
            }
        }

        public void ReflectType(TypeMetadata type, string offset)
        {
            int j = 1;
            Console.WriteLine(offset + "\t" + type.m_NamespaceName + " : " + type.m_typeName);
            foreach (PropertyMetadata property in type.Properties)
            {
                Console.WriteLine(offset + "\t" + j++ +  ". " + property.m_Name);
            }
        }     
    }
}