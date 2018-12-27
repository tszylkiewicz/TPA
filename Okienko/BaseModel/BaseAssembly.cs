using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public abstract class BaseAssembly
    {
        public virtual string Name { get; set; }
        public virtual List<BaseNamespace> Namespaces { get; set; }
    }
}
