using Composition.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composition
{
    [Export(typeof(ILogWriter))]
    public abstract class ILogWriter 
    {
        public abstract void LogIt(LogWriter log);
    }
}
