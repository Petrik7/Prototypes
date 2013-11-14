using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConfigForm.Models;

namespace ConfigForm.Controllers
{
    public class HomeController : Controller
    {

        //public ConfigItem _configItem = new ConfigItem("Faarm", "Link to faaarm");

        public ActionResult Index()
        {
            ViewBag.Message = "Home.Index";

            ConfigData configData = new ConfigData();

            configData.AddConfigItem(new ConfigItem("Rules.", "Rules", "Home"));
            configData.AddConfigItem(new ConfigItem("Devices.", "Devices", "Home"));
            configData.AddConfigItem(new ConfigItem("General.", "General", "Home"));
            configData.AddConfigItem(new ConfigItem("Faarm.", "Farm", "Home"));

            configData.AddRuleOverviewItem(new RuleOverview("Movements On The Desk", RuleOverview.RuleState.Enabled));
            configData.AddRuleOverviewItem(new RuleOverview("Any movement on the road", RuleOverview.RuleState.Disabled));
            configData.AddRuleOverviewItem(new RuleOverview("Any movement in the lab", RuleOverview.RuleState.Enabled));

            return View(configData);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Rules()
        {
            ViewBag.Message = "Home.Rule";

            ConfigData configData = new ConfigData();

            configData.AddConfigItem(new ConfigItem("Rules.", "Rules", "Home"));
            configData.AddConfigItem(new ConfigItem("Devices.", "Devices", "Home"));
            configData.AddConfigItem(new ConfigItem("General.", "General", "Home"));
            configData.AddConfigItem(new ConfigItem("Faarm.", "Farm", "Home"));

            configData.AddRuleOverviewItem(new RuleOverview("RR Movements On The Desk", RuleOverview.RuleState.Enabled));
            configData.AddRuleOverviewItem(new RuleOverview("RR Any movement on the road", RuleOverview.RuleState.Disabled));
            configData.AddRuleOverviewItem(new RuleOverview("RR Any movement in the lab", RuleOverview.RuleState.Enabled));

            return View(configData);
        }

        public ActionResult Devices()
        {
            ViewBag.Message = "Home.Devises";

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

        public ActionResult TestPage1()
        {
            return View();
        }
    }
}
