using BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModel
{
    [Table("ParameterMetadata")]
    public class DataBaseParameter : BaseParameter
    {
        public override string Name { get; set; }
        public new DataBaseType Type { get; set; }
        public int Id { get; set; }

        public virtual ICollection<DataBaseMethod> MethodParameters { get; set; }
        public virtual ICollection<DataBaseType> TypeFields { get; set; }

        public DataBaseParameter()
        {
            MethodParameters = new HashSet<DataBaseMethod>();
            TypeFields = new HashSet<DataBaseType>();
        }
    }
}
