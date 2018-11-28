using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Model;

namespace Datas
{
    public class DatasDBContext : DbContext
    {
        public DbSet<AssemblyMetadata> AssemblyMetadatas { get; set; }
        public DbSet<NamespaceMetadata> NamespaceMetadatas { get; set; }
        public DbSet<TypeMetadata> TypeMetadatas { get; set; }
        public DbSet<PropertyMetadata> PropertyMetadatas { get; set; }
        public DbSet<MethodMetadata> MethodMetadatas { get; set; }
        public DbSet<ParameterMetadata> ParameterMetadatas { get; set; }

        public DatasDBContext()
            : base("ReflectionsDataBaseB")
        {
        }
    }
}
