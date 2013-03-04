using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProject.App_SrcCode
{
    [Serializable]
    public class Employee1
    {
        private int _employeeID;
        private string _lastName;
        private string _firstName;

        public int EmployeeID
        {
            get { return _employeeID; }
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public Employee1(int employeeID, string lastName, string firstName)
        {
            _employeeID = employeeID;
            _lastName = lastName;
            _firstName = firstName;
        }
    }
}