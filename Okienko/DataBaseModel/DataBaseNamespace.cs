using BaseModel;
using System.Collections.Generic;

namespace DataBaseModel
{
    public class DataBaseNamespace : BaseNamespace
    {
        public override string Name { get; set; }
        public new List<DataBaseType> Types { get; set; }
        public int Id { get; set; }  
    }
}
