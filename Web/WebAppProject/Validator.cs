using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProject
{
    public class Validator
    {

        public static void ThrowIfNullOrEmpty<T>(string parameter, string message, Exception innerException = null) where T : Exception
        {
            if(string.IsNullOrEmpty(parameter))
                throw Activator.CreateInstance(typeof(T), innerException) as T;
        }

        public static void ThrowIfNull<T>(object parameter, string message, Exception innerException = null) where T : Exception
        {
            if (parameter == null)
                throw Activator.CreateInstance(typeof(T), innerException) as T;
        }

        public static void ThrowIfTrue<T>(bool condition, string message, Exception innerException = null) where T : Exception
        {
            if (condition)
                throw Activator.CreateInstance(typeof(T), innerException) as T;
        }
    }
}