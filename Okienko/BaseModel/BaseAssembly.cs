using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BaseModel
{   
    public abstract class BaseAssembly
    {
        public virtual string Name { get; set; }
        public virtual List<BaseNamespace> Namespaces { get; set; }
    }
}
