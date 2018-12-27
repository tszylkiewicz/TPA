using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public abstract class BaseParameter
    {
        public virtual string Name { get; set; }
        public virtual BaseType Type { get; set; }
    }
}
