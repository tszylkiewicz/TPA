using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModel
{
    public class DataBaseParameter : BaseParameter
    {
        public override string Name { get; set; }
        public new DataBaseParameter Type { get; set; }
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
