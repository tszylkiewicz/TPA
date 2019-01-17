using MEF;

namespace MEF
{
    public interface ILogWriter
    {
        void LogIt(LogWriter logW);
    }
}
