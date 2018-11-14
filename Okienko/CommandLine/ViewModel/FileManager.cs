using Model.Logger;
using Model.Model;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;


namespace CommandLine.ViewModel
{
    public class FileManager
    {
        LogWriter logWriter;
        public string PathVariable { get; set; }
        public Reflector reflector { get; set; }

        private TreeViewAssembly treeViewAssembly;

        public FileManager(string path)
        {
            logWriter = new LogWriter("Start: FileManager");
            PathVariable = path;
            reflector = new Reflector();
        }

        public void OpenFile()
        {
            logWriter.LogWrite("Start: FileManger.OpenFile");

            string chosenOne;
            int chosenOneInt = 0;
            TreeViewItem tempTreeViewItem;

            if (File.Exists(PathVariable))
            {
                Console.WriteLine("Otwieram\n\n");

                if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
                {
                    reflector.Reflect(PathVariable);
                    treeViewAssembly = new TreeViewAssembly(reflector.AssemblyModel);
                }
                else
                {
                    Console.WriteLine("IT IS NOT DLL FILE");
                    return;
                }

                ReflectNamespace();

                Console.WriteLine();
                Console.WriteLine("What to do (E-Exit, 'Number' - show property with number): ");
                chosenOne = Console.ReadLine();
                logWriter.LogWrite("FileManager.OpenFile: User_Input: " + chosenOne);
                Console.WriteLine();

                while (!(chosenOne.Equals("E")) && !(chosenOne.Equals("e")))
                {
                    if (Int32.TryParse(chosenOne, out chosenOneInt))
                    {
                        if ((chosenOneInt >= 0) && ((chosenOneInt <= 99)))
                        {
                            tempTreeViewItem = FindTypeWithNumber(chosenOneInt);

                            if (tempTreeViewItem != null)
                            {
                                More(FindTypeWithNumber(chosenOneInt), "");
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
                    ReflectNamespace();
                    Console.WriteLine();
                    Console.WriteLine("What to do (E-Exit, 'Numer' - show property with numer): ");
                    chosenOne = Console.ReadLine();
                    logWriter.LogWrite("FileManager.OpenFile: User_Input: " + chosenOne);
                    Console.WriteLine();

                }

                closeNamespace();
            }
            else
            {
                Console.WriteLine("IT IS NOT FILE");
            }
            logWriter.LogWrite("FileManager.OpenFile: Closing");
        }

        public void More(TreeViewItem type, string offset)
        {
            logWriter.LogWrite("Start: FileManager.More");
            logWriter.LogWrite("More: Searched_Type" + type.Name + "_" + type.Name + "_" + type.ToString());
            string anotherChoice;
            int anotherChoiceInt = 0;
            TreeViewItem tempTreeViewItem;

            ReflectType(type, offset);

            Console.WriteLine();
            Console.WriteLine("What to do now (B-Back, 'Numer' - show property with numer): ");
            anotherChoice = Console.ReadLine();
            logWriter.LogWrite("FileManager.More: User_Input: " + anotherChoice);
            Console.WriteLine();

            while (((!anotherChoice.Equals("B")) && (!anotherChoice.Equals("b"))))
            {
                if (Int32.TryParse(anotherChoice, out anotherChoiceInt))
                {
                    if (((Int32.Parse(anotherChoice)) >= 0) && ((Int32.Parse(anotherChoice)) <= 99))
                    {
                        tempTreeViewItem = FindPropertyWithNumber(Int32.Parse(anotherChoice), type);
                        if (tempTreeViewItem != null)
                        {
                            More(tempTreeViewItem, offset + "\t");
                            closeProperty(tempTreeViewItem);
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
                anotherChoice = Console.ReadLine();
                logWriter.LogWrite("FileManager.More: User_Input: " + anotherChoice);
                Console.WriteLine();
            }
            logWriter.LogWrite("Stop: FileManager.More");

            closeType(type);

            return;
        }

        public TreeViewItem FindTypeWithNumber(double Number)
        {
            logWriter.LogWrite("Start: FileManager.FindTypeWithNumber");
            int i = 1;

            foreach (TreeViewNamespace treeViewNamespace in GetNamespaces())
            {
                foreach (TreeViewType treeViewType in GetTypes(treeViewNamespace))
                {
                    if (i++ == Number)
                    {
                        logWriter.LogWrite("Stop: FileManager.FindTypeWithNumber");
                        return treeViewType;
                    }
                }
            }
            logWriter.LogWrite("Stop: FileManager.FindTypeWithNumber");
            return null;
        }

        public TreeViewItem FindPropertyWithNumber(double Number, TreeViewItem type)
        {
            logWriter.LogWrite("Start: FileManager.FindPropertyWithNumber");
            int i = 1;

            foreach (TreeViewProperty property in GetProperty(type))
            {
                if (i == Number)
                {
                    foreach (TreeViewNamespace treeViewNamespace in GetNamespaces())
                    {
                        foreach (TreeViewType treeViewType in GetTypes(treeViewNamespace))
                        {
                            if (treeViewType.Name.ToString() == property.Property.PropertyType.Name.ToString())//property.Name.ToString())// property.PropertyType.Name.ToString())
                            {
                                logWriter.LogWrite("Stop: FileManager.FindPropertyWithNumber");
                                return treeViewType;
                            }
                        }
                    }
                }
                i++;
            }

            logWriter.LogWrite("Stop: FileManager.FindPropertyWithNumber");
            return null;
        }

        public ObservableCollection<TreeViewItem> GetNamespaces()
        {
            treeViewAssembly.IsExpanded = true;
            return treeViewAssembly.Children;
        }

        public void closeNamespace()
        {
            treeViewAssembly.IsExpanded = false;
        }

        public ObservableCollection<TreeViewItem> GetTypes(TreeViewItem namespaceTreeView)
        {
            namespaceTreeView.IsExpanded = true;
            return namespaceTreeView.Children;
        }

        public void closeType(TreeViewItem namespaceTreeView)
        {
            namespaceTreeView.IsExpanded = false;
        }

        public ObservableCollection<TreeViewItem> GetProperty(TreeViewItem typeTreeView)
        {
            typeTreeView.IsExpanded = true;
            return typeTreeView.Children;
        }

        public void closeProperty(TreeViewItem typeTreeView)
        {
            typeTreeView.IsExpanded = false;
        }

        public bool ReflectNamespace()
        {
            logWriter.LogWrite("Start: FileManager.Reflect");

            Console.WriteLine(PathVariable);
            Console.WriteLine();

            foreach (TreeViewNamespace treeViewNamespace in GetNamespaces())
            {
                Console.WriteLine(treeViewNamespace.Name);

                int i = 1;
                foreach (TreeViewType treeViewType in GetTypes(treeViewNamespace))
                {
                    Console.WriteLine(i++ + ".\t" + treeViewType.Name);
                }
            }
            return true;
        }

        public void ReflectType(TreeViewItem type, string offset)
        {
            logWriter.LogWrite("Start: FileManager.ReflectType");
            int j = 1;
            Console.WriteLine(offset + "\t" + "" + " : " + type.Name);
            foreach (TreeViewProperty property in GetProperty(type))
            {
                Console.WriteLine(offset + "\t" + j++ + ". " + property.Name);
            }
            logWriter.LogWrite("Stop: FileManager.Reflect");
        }
    }
}