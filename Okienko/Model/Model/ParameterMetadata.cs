using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class ParameterMetadata
    {
        [Key]
        public int idParameter { get; set; }
        public string Name { get; set; }
        public TypeMetadata Type { get; set; }
        public ParameterMetadata(string name, TypeMetadata type)
        {
            this.Name = name;
            this.Type = type;
        }        
    }
}
