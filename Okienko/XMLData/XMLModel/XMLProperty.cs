using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLData.XMLModel
{
    public  class XMLProperty : BaseProperty
    {
        public override string Name { get; set; }
        public new XMLType PropertyType { get; set; }
    }
}
