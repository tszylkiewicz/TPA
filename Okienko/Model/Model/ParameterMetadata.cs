using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    [DataContract(IsReference = true)]
    public class ParameterMetadata
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public TypeMetadata Type { get; set; }
        public ParameterMetadata(string name, TypeMetadata type)
        {
            this.Name = name;
            this.Type = type;
        }        
    }
}
