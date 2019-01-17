using MEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseModel;


namespace DatabaseLogger
{
    [Export(typeof(ILogWriter))]
    public class DatabaseLogger : ILogWriter
    {
        public void LogIt(LogWriter logW)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                context.Log.Add(new LogModel()
                {
                    Message = logW.Text
                });
                context.SaveChanges();
            }
        }
    }
}
