using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public abstract class BaseProperty
    {
        public virtual string Name { get; set; }
        public virtual BaseType PropertyType { get; set; }
    }
}
