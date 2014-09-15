using Notebook.Common;
using Notebook.Helpers;
using Notebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notebook.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            try
            {
                return View(NoteDM.DisplayAll(Global.filter));
            }
            catch (Exception e)
            {
                Logger.Log("Index failed", e);
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}