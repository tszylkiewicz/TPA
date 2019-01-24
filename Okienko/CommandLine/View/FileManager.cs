using Model.ViewModel;
using Model.ViewModel.TreeView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;

namespace CommandLine.View
{
    public class FileManager
    {
        public MyViewModel MyViewModel = new MyViewModel();

        public FileManager(string path)
        {           
            MyViewModel.PathVariable = path;
        }

        public string getPath()
        {
            return MyViewModel.PathVariable;
        }

        public bool OpenFile()
        {
            string chosenOne;
            int chosenOneInt = 0;
            TreeViewItem tempTreeViewItem;

            if (File.Exists(MyViewModel.PathVariable))
            {
                Console.WriteLine("Otwieram\n\n");

                if (MyViewModel.PathVariable.Substring(MyViewModel.PathVariable.Length - 4) == ".dll")
                {
                    MyViewModel.reflector.Reflect(MyViewModel.PathVariable);
                    MyViewModel.treeViewAssembly = new TreeViewAssembly(MyViewModel.reflector.AssemblyModel);
                }
                else
                {
                    Console.WriteLine("IT IS NOT DLL FILE");
                    return false;
                }

                ReflectNamespace();

                Console.WriteLine();
                Console.WriteLine("What to do (E-Exit, 'Number' - show property with number): ");
                chosenOne = Console.ReadLine();
                Console.WriteLine();

                while (!(chosenOne.Equals("E")) && !(chosenOne.Equals("e")))
                {
                    if (Int32.TryParse(chosenOne, out chosenOneInt))
                    {
                        if ((chosenOneInt >= 0) && ((chosenOneInt <= 999)))
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
                    Console.WriteLine();

                }
                closeNamespace();
            }
            else
            {
                Console.WriteLine("IT IS NOT FILE");
                return false;
            }
            return true;
        }

        public void More(TreeViewItem type, string offset)
        {
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
            Console.WriteLine();

            while (((!anotherChoice.Equals("B")) && (!anotherChoice.Equals("b"))))
            {
                if (Int32.TryParse(anotherChoice, out anotherChoiceInt))
                {
                    if (((Int32.Parse(anotherChoice)) >= 0) && ((Int32.Parse(anotherChoice)) <= 999))
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
                    //Console.WriteLine("\n\n\nInsert path to save file: ");
                    MyViewModel.PathForSerialization = ConfigurationManager.AppSettings["saveXMLPath"];
                    if (MyViewModel.PathForSerialization != "")
                    {
                        //MyViewModel.PathForSerialization += ".xml";
                        MyViewModel.Save();

                        Console.WriteLine("Save Completed\n\n\n");
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
                Console.WriteLine();
            }

            closeType(type);

            return;
        }

        public TreeViewItem FindTypeWithNumber(double Number)
        {
            int i = 1;

            foreach (TreeViewNamespace treeViewNamespace in GetNamespaces())
            {
                foreach (TreeViewType treeViewType in GetTypes(treeViewNamespace))
                {
                    if (i++ == Number)
                    {
                        return treeViewType;
                    }
                }
            }
            return null;
        }

        public TreeViewItem FindPropertyWithNumber(double Number, TreeViewItem type)
        {
            int i = 1;

            foreach (TreeViewItem meta in GetProperty(type))
            {
                if (i == Number)
                {
                    if (meta.GetType() == typeof(TreeViewProperty))
                    {
                        return GetProperty(meta)[0];
                    }
                    else if (meta.GetType() == typeof(TreeViewMethod))
                    {
                        return meta;
                    }
                }
                i++;
            }
            return null;
        }

        public TreeViewItem FindMethodObjectWithNumber(double Number, TreeViewItem type)
        {
            int i = 1;

            foreach (TreeViewItem meta in GetProperty(type))
            {
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
            return null;
        }


        public ObservableCollection<TreeViewItem> GetNamespaces()
        {
            MyViewModel.treeViewAssembly.IsExpanded = true;
            return MyViewModel.treeViewAssembly.Children;
        }

        public void closeNamespace()
        {
            MyViewModel.treeViewAssembly.IsExpanded = false;
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

            Console.WriteLine(MyViewModel.PathVariable);
            Console.WriteLine();

            int i = 1;

            foreach (TreeViewNamespace treeViewNamespace in GetNamespaces())
            {
                Console.WriteLine(treeViewNamespace.Name);
                
                foreach (TreeViewType treeViewType in GetTypes(treeViewNamespace))
                {
                    Console.WriteLine(i++ + ".\t" + treeViewType.Name);
                }
            }
            return true;
        }

        public void ReflectType(TreeViewItem type, string offset)
        {
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
        }

        public void ReflectMethod(TreeViewItem type, string offset)
        {
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
        }
    }
}