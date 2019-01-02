using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public interface ISerializer<T>
    {
        void Serialize(string path, T obj);
        T Deserialize(string path);
    }
}
