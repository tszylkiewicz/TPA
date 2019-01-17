using BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMLData.XMLModel
{
    public class XMLParameter : BaseParameter
    {
        public override string Name { get; set; }
        public new XMLType Type { get; set; }
    }
}
