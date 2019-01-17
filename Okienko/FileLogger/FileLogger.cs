using MEF;
using System.ComponentModel.Composition;
using System.IO;

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
