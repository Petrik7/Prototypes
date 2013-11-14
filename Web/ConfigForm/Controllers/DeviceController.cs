using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConfigForm.Models;

namespace ConfigForm.Controllers
{
    public class DeviceController : Controller
    {
        //
        // GET: /Device/

        public ActionResult Index()
        {
            //return View();
            return DeviceConfigNetwork("111", "_Layout");
        }

        public ActionResult DeviceConfigNetwork(string id, string layout)
        {
            ViewBag.Message = "Home.Devises";

            ConfigData configData = new ConfigData();

            configData.AddConfigItem(new ConfigItem("Rules.", "Rules", "Home"));
            configData.AddConfigItem(new ConfigItem("Devices.", "Devices", "Home"));
            configData.AddConfigItem(new ConfigItem("General.", "General", "Home"));
            configData.AddConfigItem(new ConfigItem("Faarm.", "Farm", "Home"));

            configData.AddDeviceOverviewItem(new DeviceOverview("Axis " + id, 1));
            configData.AddDeviceOverviewItem(new DeviceOverview("Bosch 222", 2));
            configData.AddDeviceOverviewItem(new DeviceOverview("Vivotek 333", 3));
            configData.AddDeviceOverviewItem(new DeviceOverview("Vivotek 444", 4));
            configData.AddDeviceOverviewItem(new DeviceOverview("Vivotek 555", 5));
            configData.AddDeviceOverviewItem(new DeviceOverview("Vivotek 888", 8));

            return View("DeviceConfigNetwork", layout, configData);
            //return View("DeviceConfigNetwork", "_EmptyLayout", configData);
            //return View(configData);
        }

        public ActionResult DeviceList()
        {
            ViewBag.Message = "Device.DeviceList";

            ConfigData configData = new ConfigData();

            configData.AddConfigItem(new ConfigItem("Rules.", "Rules", "Home"));
            configData.AddConfigItem(new ConfigItem("Devices.", "Devices", "Home"));
            configData.AddConfigItem(new ConfigItem("General.", "General", "Home"));
            configData.AddConfigItem(new ConfigItem("Faarm.", "Farm", "Home"));

            configData.AddDeviceOverviewItem(new DeviceOverview("Axis 111", 1));
            configData.AddDeviceOverviewItem(new DeviceOverview("Bosch 222", 2));
            configData.AddDeviceOverviewItem(new DeviceOverview("Vivotek 333", 3));
            configData.AddDeviceOverviewItem(new DeviceOverview("Vivotek 444", 4));
            configData.AddDeviceOverviewItem(new DeviceOverview("Vivotek 555", 5));
            configData.AddDeviceOverviewItem(new DeviceOverview("Vivotek 888", 8));

            return View(configData);
        }
    }
}
