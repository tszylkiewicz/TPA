using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public interface ISerializer
    {
        void Save(string path, BaseAssembly obj);
        BaseAssembly Read(string path);
    }
}
