using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConfigForm.Models
{
    public class ConfigItem
    {
        private string _name;
        private string _link;
        private string _route;

        public ConfigItem(string name, string link, string route)
        {
            _name = name;
            _link = link;
            _route = route;
        }

        public string Name { get { return _name; ;} /*set;*/ }
        public string Link { get { return _link; ;} /*set;*/ }
        public string Route { get { return _route; ;} /*set;*/ }
    }
}
