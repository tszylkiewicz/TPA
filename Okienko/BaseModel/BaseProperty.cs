using System.Runtime.Serialization;

namespace BaseModel
{
    [DataContract (IsReference = true)]
    public abstract class BaseProperty
    {
        [DataMember] public virtual string Name { get; set; }
        public virtual BaseType PropertyType { get; set; }
    }
}
