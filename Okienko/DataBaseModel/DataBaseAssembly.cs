using System.Collections.Generic;
using System.ComponentModel.Composition;
using BaseModel;

namespace DataBaseModel
{
    [Export(typeof(BaseAssembly))]
    public class DataBaseAssembly : BaseAssembly
    {
        public override string Name { get; set; }
        public new List<DataBaseNamespace> Namespaces { get; set; }
        public int Id { get; set; }     
    }
}
