using Composition;
using MEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;

namespace Logging
{
    [Export(typeof(ILogWriter))]
    public class FileLogger : ILogWriter
    {
        public void LogIt(LogWriter logW)
        {
            using (TextWriter fileStream = new StreamWriter(File.Open("Logs.txt", FileMode.Append)))
            {
                fileStream.WriteLineAsync(logW.Text);
            }
        }
    }
}
