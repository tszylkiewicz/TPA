using System.Runtime.Serialization;

namespace BaseModel
{
    [DataContract (IsReference = true)]
    public abstract class BaseParameter
    {
        [DataMember] public virtual string Name { get; set; }
        public virtual BaseType Type { get; set; }
    }
}
