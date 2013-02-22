using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Helpers
{
    class PropertiesMonitor <T>
    {
        private SortedDictionary<string, string> _properties = new SortedDictionary<string, string>();
        T _instance;

        public void CalculatePropertiesSnapshot(T instance)
        {
            _instance = instance;
            PropertyInfo[] allProperties = GetPublicInstanceProperties();
            foreach (PropertyInfo propertyInfo in allProperties)
            {
                string propertyName = propertyInfo.Name;
                string propertyValue = propertyInfo.GetValue(instance, null).ToString();
                _properties[propertyName] = propertyValue;
            }
        }

        public bool AnyPropetryChanges()
        {
            PropertyInfo[] allProperties = GetPublicInstanceProperties();
            foreach (PropertyInfo propertyInfo in allProperties)
            {
                string currentValue = propertyInfo.GetValue(_instance, null).ToString();
                Debug.Assert(_properties.ContainsKey(propertyInfo.Name));
                string originalValue = _properties[propertyInfo.Name];
                if (currentValue != originalValue)
                    return true;
            }
            return false;
        }

        private PropertyInfo[] GetPublicInstanceProperties()
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
    }
}