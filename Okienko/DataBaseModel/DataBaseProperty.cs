using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModel
{
    public class DataBaseProperty : BaseProperty
    {
        public override string Name { get; set; }
        public new DataBaseType PropertyType { get; set; }
        public int Id { get; set; }

        public virtual ICollection<DataBaseType> TypeProperties { get; set; }

        public DataBaseProperty()
        {
            TypeProperties = new HashSet<DataBaseType>();
        }
    }
}
