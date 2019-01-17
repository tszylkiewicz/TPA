using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations.Schema;
using BaseModel;

namespace DataBaseModel
{
    [Table ("AssemblyMetadata")]
    [Export(typeof(BaseAssembly))]
    public class DataBaseAssembly : BaseAssembly
    {
        public override string Name { get; set; }
        public new List<DataBaseNamespace> Namespaces { get; set; }
        public int Id { get; set; }     
    }
}
