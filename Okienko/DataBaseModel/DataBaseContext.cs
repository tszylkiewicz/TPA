using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModel
{
    class DataBaseContext : DbContext
    {
        public DbSet<DataBaseAssembly> AssemblyMetadatas { get; set; }
        public DbSet<DataBaseNamespace> NamespaceMetadatas { get; set; }
        public DbSet<DataBaseType> TypeMetadatas { get; set; }
        public DbSet<DataBaseProperty> PropertyMetadatas { get; set; }
        public DbSet<DataBaseMethod> MethodMetadatas { get; set; }
        public DbSet<DataBaseParameter> ParameterMetadatas { get; set; }

        private const string connectionString =
          @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Okieneczko;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework";


        public DataBaseContext()
            : base(connectionString)
        {
        }
    }
}
