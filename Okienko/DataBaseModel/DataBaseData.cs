using BaseModel;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Data.Entity;

namespace DataBaseModel
{
    [Export(typeof(ISerializer))]
    public class DataBaseData : ISerializer
    {
        public BaseAssembly Read(string path)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                context.NamespaceMetadatas
                    .Include(n => n.Types)
                    .Load();
                context.TypeMetadatas
                    .Include(t => t.Constructors)
                    .Include(t => t.BaseTyp)
                    .Include(t => t.DeclaringType)
                    .Include(t => t.Attributes)
                    .Include(t => t.ImplementedInterfaces)
                    .Include(t => t.GenericArguments)
                    .Include(t => t.Methods)
                    .Include(t => t.NestedTypes)
                    .Include(t => t.Properties)
                    .Include(t => t.TypeGenericArguments)
                    .Include(t => t.TypeImplementedInterfaces)
                    .Include(t => t.TypeNestedTypes)
                    .Include(t => t.MethodGenericArguments)
                    .Include(t => t.TypeBaseTypes)
                    .Include(t => t.TypeDeclaringTypes)
                    .Load();
                context.ParameterMetadatas
                    .Include(p => p.Type)
                    .Include(p => p.TypeFields)
                    .Include(p => p.MethodParameters)
                    .Load();
                context.MethodMetadatas
                    .Include(m => m.GenericArguments)
                    .Include(m => m.Parameters)
                    .Include(m => m.ReturnType)
                    .Include(m => m.TypeConstructors)
                    .Include(m => m.TypeMethods)
                    .Load();
                context.PropertyMetadatas
                    .Include(p => p.PropertyType)
                    .Include(p => p.TypeProperties)
                    .Load();


                DataBaseAssembly dbAssemblyMetadata = context.AssemblyMetadatas
                    .Include(a => a.Namespaces)
                    .ToList().FirstOrDefault();
                if (dbAssemblyMetadata == null)
                    throw new ArgumentException("Database is empty");
                return dbAssemblyMetadata;
            }
        }

        public void Save(string path, BaseAssembly obj)
        {
            clearDataBase();
            using (DataBaseContext context = new DataBaseContext())
            {
                Console.WriteLine("Hello");
                DataBaseAssembly assemblyMetadata = (DataBaseAssembly)obj;
                context.AssemblyMetadatas.Add(assemblyMetadata);
                context.SaveChanges();
            }
        }

        public void clearDataBase()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM ParameterMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM PropertyMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM MethodMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM TypeMetadata");
                context.Database.ExecuteSqlCommand("DELETE FROM NamespaceMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM AssemblyMetadata WHERE ID != -1");
                context.SaveChanges();
            }
        }
    }
}
