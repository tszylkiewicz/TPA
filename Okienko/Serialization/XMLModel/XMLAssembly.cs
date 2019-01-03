using BaseModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.Composition;

namespace XMLSerializer.XMLModel
{
    //[DataContract(IsReference = true)]
    [Export(typeof(BaseAssembly))]
    public class XMLAssembly : BaseAssembly
    {
        [DataMember] public override string Name { get; set; }
        [DataMember] public new List<XMLNamespace> Namespaces { get; set; }
    }
}
