using System.Runtime.Serialization;

namespace BaseModel
{
    public abstract class BaseParameter
    {
        public virtual string Name { get; set; }
        public virtual BaseType Type { get; set; }
    }
}
