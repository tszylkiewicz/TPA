using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class ParameterMetadata
    {
        public string Name { get; set; }
        public TypeMetadata Type { get; set; }
        public ParameterMetadata(string name, TypeMetadata type)
        {
            this.Name = name;
            this.Type = type;
        }        
    }
}
