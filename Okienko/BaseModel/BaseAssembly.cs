using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BaseModel
{
    [DataContract (IsReference = true)]
    public abstract class BaseAssembly
    {
        [DataMember] public virtual string Name { get; set; }
        public virtual List<BaseNamespace> Namespaces { get; set; }
    }
}
