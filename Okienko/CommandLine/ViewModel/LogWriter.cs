﻿using System;
using System.Diagnostics;

namespace CommandLine.ViewModel
{
    public class LogWriter
    { 
        static TextWriterTraceListener logsListener = new TextWriterTraceListener("Logs.log", "logsListener");

        private string m_exePath = string.Empty;
        public LogWriter(string logMessage)
        {
            LogWrite(logMessage);
        }
        public void LogWrite(string logMessage)
        {
            string text = "";
            text += "\r\nLog Entry : ";
            text += DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();
            text += "  : " + logMessage;
            logsListener.WriteLine(text);
            logsListener.Flush();
        }
    }
}