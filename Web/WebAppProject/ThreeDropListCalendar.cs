using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebAppProject
{
    public class ThreeDropListCalendar
    {
        private DropDownList _monthDropDownList;
        private DropDownList _dayDropDownList;
        private DropDownList _yearDropDownList;

        public ThreeDropListCalendar(DropDownList monthDropDownList, DropDownList dayDropDownList, DropDownList yearDropDownList)
        {
            Validator.ThrowIfNull<ArgumentNullException>(monthDropDownList, "monthDropDownList");
            Validator.ThrowIfNull<ArgumentNullException>(dayDropDownList, "dayDropDownList");
            Validator.ThrowIfNull<ArgumentNullException>(yearDropDownList, "yearDropDownList");

            _monthDropDownList = monthDropDownList;
            _dayDropDownList = dayDropDownList;
            _yearDropDownList = dayDropDownList;
        }
    }
}