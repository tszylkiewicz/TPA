using BaseModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseModel
{
    [Table("NamespaceMetadata")]
    public class DataBaseNamespace : BaseNamespace
    {
        public override string Name { get; set; }
        public new List<DataBaseType> Types { get; set; }
        public int Id { get; set; }  
    }
}
