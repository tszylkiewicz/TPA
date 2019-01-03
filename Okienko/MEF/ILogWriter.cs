using MEF;

namespace Composition
{
    public interface ILogWriter
    {
        void LogIt(LogWriter logW);
    }
}
