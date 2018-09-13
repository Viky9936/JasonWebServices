using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TestServiceApp
{
    /// <summary>
    /// Summary description for Jasonparse
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Jasonparse : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]

        public void Parsedata(string json)
        {
            //create an instance of HttpClient
            HttpClient client = new HttpClient();

            List<EmpDetails> listEmpDetails = new List<EmpDetails>();

            //DefaultRequestHeader to Json
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Create an instance of HttpResponse & invoke the service asynchronously
            //HttpResponseMessage response = client.GetAsync("http://localhost:51545/TestWebService.asmx/TestAllEmployees").Result;


            //Read response content result into string variable
            //string json = response.Content.ReadAsStringAsync().Result;

            JavaScriptSerializer js = new JavaScriptSerializer();

            //Deserialize to strongly typed class i.e., RootObject
            EmpDetails obj = js.Deserialize<EmpDetails>(json);
            Employee employee = new Employee();
            Address addr = new Address();
            Salary sal = new Salary();
            EmpDetails empd = new EmpDetails();



            employee = obj.employee;
            int emp_id = employee.emp_id;
            string emp_name = employee.emp_name;
            int salary = employee.salary;
            addr = obj.addr;
            string address1 = addr.address1;
            string address2 = addr.address2;
            string city = addr.city;
            sal = obj.sal;
            int hourly_wages = sal.hourly_wages;
            string payment_Date = sal.payment_Date;

            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["servicestring"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"INSERT INTO Employee(emp_id, emp_name, salary) VALUES(@emp_id, @emp_name, @salary);
            INSERT INTO Address(address1, address2, city) VALUES(@address1, @address2, @city);
            INSERT INTO Salary(hourly_wages, payment_Date) VALUES(@hourly_wages, @payment_Date)";

            cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = emp_id;
            cmd.Parameters.Add("@emp_name", SqlDbType.VarChar).Value = emp_name;
            cmd.Parameters.Add("@salary", SqlDbType.VarChar).Value = salary;

            cmd.Parameters.Add("@address1", SqlDbType.VarChar).Value = address1;
            cmd.Parameters.Add("@address2", SqlDbType.VarChar).Value = address2;
            cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = city;

            cmd.Parameters.Add("@hourly_wages", SqlDbType.VarChar).Value = hourly_wages;
            cmd.Parameters.Add("@payment_Date", SqlDbType.VarChar).Value = payment_Date;

            if (con.State == ConnectionState.Open) { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


            //String Name = "abc";

            //String Address = "abc address";
            //String Company = "xyz company";
            //String Address2 = "xyz company address";

            //SqlCommand sqlcmd = new SqlCommand();
            //SqlConnection cn = new SqlConnection();
            //cn.ConnectionString = ConfigurationManager.ConnectionStrings["LOC_MASTERConnectionString"].ToString();
            //sqlcmd.CommandType = CommandType.Text;
            //sqlcmd.Connection = cn;

            //sqlcmd.CommandText = @"INSERT INTO Employee(emp_id, emp_name, salary) VALUES(@emp_id, @emp_name, @salary);
            //INSERT INTO Table2(Company, Address2) VALUES(@Company, @Address2)";

            //sqlcmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            //sqlcmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = Address;

            //sqlcmd.Parameters.Add("@Company", SqlDbType.VarChar).Value = Company;
            //sqlcmd.Parameters.Add("@Address2", SqlDbType.VarChar).Value = Address2;

            //if (cn.State == ConnectionState.Open) { cn.Close(); }
            //cn.Open();
            //sqlcmd.ExecuteNonQuery();
            //cn.Close();

        }

    }

}