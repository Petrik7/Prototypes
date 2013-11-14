using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc3App.Models;

namespace Mvc3App.Controllers
{
    public class DeviceController : Controller
    {
        //
        // GET: /Device/

        public ActionResult Index()
        {
            return View(DBDevice.GetDevice(4));
        }

    }
}
