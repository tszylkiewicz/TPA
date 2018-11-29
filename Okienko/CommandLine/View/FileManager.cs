using Model.Logger;
using Model.Model;
using Model.ViewModel;
using Model.ViewModel.TreeView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Datas;
using Model.Serialization;

namespace CommandLine.View
{
    public class FileManager
    {
        LogWriter logWriter;
        public string PathVariable { get; set; }
        public Reflector reflector { get; set; }
        public TreeViewAssembly treeViewAssembly { get; set; }
        public string savePath { get; set; }
        public ISerializer serializer = new XMLSerializer();


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
                                Console.WriteLine("\n\n\nWRONG NUMBER - ERROR\n\n\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\n\nWRONG NUMBER - ERROR\n\n\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\n\nWRONG NUMBER - ERROR\n\n\n");
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

            if (type.GetType() != typeof(TreeViewMethod))
            {
                ReflectType(type, offset);
            }
            else
            {
                ReflectMethod(type, offset);
            }

            Console.WriteLine();
            Console.WriteLine("What to do now (B-Back, 'Numer' - show property with numer, S - SAVE to File): ");
            anotherChoice = Console.ReadLine();
            logWriter.LogWrite("FileManager.More: User_Input: " + anotherChoice);
            Console.WriteLine();

            while (((!anotherChoice.Equals("B")) && (!anotherChoice.Equals("b"))))
            {
                if (Int32.TryParse(anotherChoice, out anotherChoiceInt))
                {
                    if (((Int32.Parse(anotherChoice)) >= 0) && ((Int32.Parse(anotherChoice)) <= 99))
                    {
                        if (type.GetType() != typeof(TreeViewMethod))
                        {
                            tempTreeViewItem = FindPropertyWithNumber(Int32.Parse(anotherChoice), type);
                            if (tempTreeViewItem != null)
                            {
                                More(tempTreeViewItem, offset + "\t");
                                closeProperty(tempTreeViewItem);
                            }
                            else
                            {
                                Console.WriteLine("\n\n\nWRONG NUMBER - ERROR\n\n\n");
                            }
                        }
                        else if (type.GetType() == typeof(TreeViewMethod))
                        {
                            tempTreeViewItem = FindMethodObjectWithNumber(Int32.Parse(anotherChoice), type);
                            if (tempTreeViewItem != null)
                            {
                                    More(tempTreeViewItem, offset + "\t");
                                    closeProperty(tempTreeViewItem);
                            }
                            else
                            {
                                Console.WriteLine("\n\n\nWRONG NUMBER - ERROR\n\n\n");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\n\nWRONG NUMBER - ERROR\n\n\n");
                    }
                }
                else if (anotherChoice.Equals("S") || anotherChoice.Equals("s"))
                {
                    Console.WriteLine("\n\n\nInsert path to save file: ");
                    savePath = Console.ReadLine();
                    if (savePath != "")
                    {
                        savePath += ".xml";
                        serializer.Serialize(savePath, reflector.AssemblyModel);
                        Console.WriteLine("XML file has been saved\n\n\n");
                    }
                    else
                    {
                        Console.WriteLine("WRONG PATH - ERROR\n\n\n");
                    }
                }
                else
                {
                    Console.WriteLine("\n\n\nWRONG NUMBER - ERROR\n\n\n");
                }

                if (type.GetType() != typeof(TreeViewMethod))
                {
                    ReflectType(type, offset);
                }
                else
                {
                    ReflectMethod(type, offset);
                }


                Console.WriteLine();
                Console.WriteLine("What to do now (B-Back, 'Numer' - show property with numer, S - SAVE to File): ");
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

            foreach (TreeViewItem meta in GetProperty(type))
            {
                if (i == Number)
                {
                    if (meta.GetType() == typeof(TreeViewProperty))
                    {
                        logWriter.LogWrite("Stop: FileManager.FindPropertyWithNumber");
                        return GetProperty(meta)[0];
                    }
                    else if (meta.GetType() == typeof(TreeViewMethod))
                    {
                        return meta;
                    }
                }
                i++;
            }

            logWriter.LogWrite("Stop: FileManager.FindPropertyWithNumber");
            return null;
        }

        public TreeViewItem FindMethodObjectWithNumber(double Number, TreeViewItem type)
        {
            logWriter.LogWrite("Start: FileManager.FindMethodObjectWithNumber");
            int i = 1;

            foreach (TreeViewItem meta in GetProperty(type))
            {
               // Console.WriteLine("COUTN: " + GetProperty(meta).Count);
                if (i == Number)
                {
                    if (GetProperty(meta).Count > 0)
                    {
                        if (GetProperty(meta)[i - 1].GetType() != typeof(void))
                        {
                            return GetProperty(meta)[i - 1];
                        }
                        else
                        {
                            return null;
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
            foreach (TreeViewItem meta in GetProperty(type))
            {
                if (meta.GetType() == typeof(TreeViewProperty))
                {
                    Console.WriteLine(offset + "\t" + j++ + ". P " + meta.Name);
                }
                if (meta.GetType() == typeof(TreeViewMethod))
                {
                    Console.WriteLine(offset + "\t" + j++ + ". M " + meta.Name);
                }
            }
            logWriter.LogWrite("Stop: FileManager.Reflect");
        }

        public void ReflectMethod(TreeViewItem type, string offset)
        {
            logWriter.LogWrite("Start: FileManager.ReflectMethod");
            int j = 1;
            Console.WriteLine(offset + "\t" + "" + " : " + type.Name);
            foreach (TreeViewItem meta in GetProperty(type))
            {
                if (meta.GetType() == typeof(TreeViewParameter))
                {
                    TreeViewParameter temp = (TreeViewParameter)meta;
                    Console.WriteLine(offset + "\t" + j++ + ". MP " + temp.Parameter.Type.Name);
                }
                if (meta.GetType() == typeof(TreeViewType))
                {
                    Console.WriteLine(offset + "\t" + j++ + ". RT " + meta.Name);
                }
            }
            logWriter.LogWrite("Stop: FileManager.Reflect");
        }
    }
}