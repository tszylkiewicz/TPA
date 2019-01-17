using MEF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModel
{
    public class DataBaseContext : DbContext
    {
        public virtual DbSet<DataBaseAssembly> AssemblyMetadatas { get; set; }
        public virtual DbSet<DataBaseNamespace> NamespaceMetadatas { get; set; }
        public virtual DbSet<DataBaseType> TypeMetadatas { get; set; }
        public virtual DbSet<DataBaseProperty> PropertyMetadatas { get; set; }
        public virtual DbSet<DataBaseMethod> MethodMetadatas { get; set; }
        public virtual DbSet<DataBaseParameter> ParameterMetadatas { get; set; }
        public virtual DbSet<LogModel> Log { get; set; }

        private static String GetString()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            path = path.Remove(path.Length - 8);
            return "data source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=" + path + "\\TPASerializationDB.mdf;integrated security = True; MultipleActiveResultSets=True;App=EntityFramework";
        }

        public DataBaseContext() : base(GetString())
        {
        }
    }
}
