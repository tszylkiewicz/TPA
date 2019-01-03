using System.Runtime.Serialization;

namespace BaseModel
{
    public abstract class BaseProperty
    {
        public virtual string Name { get; set; }
        public virtual BaseType PropertyType { get; set; }
    }
}
