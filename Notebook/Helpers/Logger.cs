using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;
using System.Reflection;

namespace Notebook.Helpers
{
    public static class Logger
    {
        private static readonly ILog log;

        static Logger()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(Logger));
        }

        public static void Log(string message, Exception e)
        {
            log.Debug(message, e);
        }
    }
}