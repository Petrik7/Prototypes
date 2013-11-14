using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConfigForm.Models
{
    public class RuleOverview
    {
        public enum RuleState { Enabled, Disabled };
        
        private string _name;
        private RuleState _state;

        public RuleOverview(string name, RuleState state)
        {
            _name = name;
            _state = state;
        }

        public string Name { get { return _name; ;} /*set;*/ }
        public RuleState State { get { return _state; ;} /*set;*/ }
    }
}