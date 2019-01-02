using MEF.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF
{
    public interface ILogWriter 
    {
        void LogIt(LogWriter log);
    }
}
