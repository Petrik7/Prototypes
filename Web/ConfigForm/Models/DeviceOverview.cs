using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConfigForm.Models
{
    public class DeviceOverview
    {
       
        private string _name;
        private int _id;

        public DeviceOverview(string name, int id)
        {
            _name = name;
            _id = id;
        }

        public string Name { get { return _name; ;} /*set;*/ }
        public int Id { get { return _id; ;} /*set;*/ }
    }
}