using BaseModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace XMLData.XMLModel
{
    [Export(typeof(BaseAssembly))]
    public class XMLAssembly : BaseAssembly
    {
        public override string Name { get; set; }
        public new List<XMLNamespace> Namespaces { get; set; }
    }
}
