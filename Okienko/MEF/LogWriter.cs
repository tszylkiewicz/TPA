using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF
{
    public class LogWriter
    {
        //static TextWriterTraceListener logsListener = new TextWriterTraceListener("Logs.log", "logsListener");

        // private string m_exePath = string.Empty;

        public string Text;

        public LogWriter(string logMessage)
        {
            Text = "";
            LogWrite(logMessage);           
        }

        //public LogWriter()
        //{
        //    Text = "";
        //}

        public void LogWrite(string logMessage)
        {
            string text = "";
            text += "\r\nLog Entry : ";
            text += DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();
            text += "  : " + logMessage;

            Text += text;

            //logsListener.WriteLine(text);
            //logsListener.Flush();
        }
    }
}
