using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestServiceApp
{
    public class Employee
    {
        public int emp_id { get; set; }
        public string emp_name { get; set; }
        public int salary { get; set; }
    }

    public class Address
    {
        public string address1 { get; set; }

        public string address2 { get; set; }

        public string city { get; set; }
    }

    public class Salary
    {
        public int hourly_wages { get; set; }
        public string payment_Date { get; set; }
    }

    public class EmpDetails
    {
        public Employee employee { get; set; }
        public Address addr { get; set; }
        public Salary sal { get; set; }

    }
}
