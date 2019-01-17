using BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModel
{
    [Export(typeof(ISerializer))]
    public class DataBaseData : ISerializer
    {

        private const string _connectionString =
          @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Okienko;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework";

        public BaseAssembly Deserialize(string path)
        {
            throw new NotImplementedException();
        }

        public void Serialize(string path, BaseAssembly obj)
        {
            //clearDataBase();
            using (DataBaseContext context = new DataBaseContext())
           {
                Console.WriteLine("Hello");
                DataBaseAssembly assemblyMetadata = (DataBaseAssembly)obj;
                context.AssemblyMetadatas.Add(assemblyMetadata);
               // context.SaveChanges();
            }
        }

        public void clearDataBase()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM ParameterMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM PropertyMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM MethodMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM TypeMetadata ");
                context.Database.ExecuteSqlCommand("DELETE FROM NamespaceMetadata WHERE ID != -1");
                context.Database.ExecuteSqlCommand("DELETE FROM AssemblyMetadata WHERE ID != -1");
                context.SaveChanges();
            }
        }
    }
}
