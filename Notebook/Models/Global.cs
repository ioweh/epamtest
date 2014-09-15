using Notebook.BLL;
using Notebook.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Notebook.Models
{
    public class Global
    {
        public static Business business;
        public static NoteFilter filter;

        static Global()
        {
            business = new Business(ConfigurationManager.AppSettings["connectionString"]);
            filter = new NoteFilter();
        }
    }
}