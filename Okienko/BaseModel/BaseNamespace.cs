using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public abstract class BaseNamespace
    {
        public virtual string Name { get; set; }
        public virtual List<BaseType> Types { get; set; }
    }
}
