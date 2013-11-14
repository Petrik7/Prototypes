using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

namespace ConfigForm.Models
{
    public class ConfigData
    {
        private List<ConfigItem> _configItems = new List<ConfigItem>();
        private List<RuleOverview> _ruleOverviews = new List<RuleOverview>();
        private List<DeviceOverview> _deviceOverviews = new List<DeviceOverview>();

        public void AddConfigItem(ConfigItem item)
        {
            _configItems.Add(item);        
        }

        public void AddRuleOverviewItem(RuleOverview item)
        {
            _ruleOverviews.Add(item);
        }

        public void AddDeviceOverviewItem(DeviceOverview item)
        {
            _deviceOverviews.Add(item);
        }

        public ReadOnlyCollection<ConfigItem> ConfigItems
        {
            get { return new ReadOnlyCollection<ConfigItem>(_configItems); }
        }

        public ReadOnlyCollection<RuleOverview> RuleOverviews
        {
            get { return new ReadOnlyCollection<RuleOverview>(_ruleOverviews); }
        }

        public ReadOnlyCollection<DeviceOverview> DeviceOverviews
        {
            get { return new ReadOnlyCollection<DeviceOverview>(_deviceOverviews); }
        }
    }
}