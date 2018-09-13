using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;



namespace TestServiceApp
{
    /// <summary>
    /// Summary description for TestWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TestWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public DataSet getEmp()
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["servicestring"].ConnectionString;
            string query = "select [Emp_ID],[Emp_Name],[Salary] from Employee where [Emp_ID] = 4";
            SqlDataAdapter adp = new SqlDataAdapter(query, cs);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            return ds;
        }

        [WebMethod]
        public string getEmployee()
        {
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["servicestring"].ConnectionString;
            string query = "select [Emp_ID],[Emp_Name],[Salary] from Employee";
            SqlDataAdapter adp = new SqlDataAdapter(query, cs);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            return JsonConvert.SerializeObject(ds,Newtonsoft.Json.Formatting.Indented);
        }

        [WebMethod]
        public void GetAllEmployees()
        {
            List<EmpDetails> listEmpDetails = new List<EmpDetails>();
           
            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["servicestring"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query1 = ("select e.Emp_ID, e.Emp_Name, e.Salary, a.Address1, a.Address2, a.city, s.Hourly_wages, s.Payment_Date from Employee as e inner join Address as a on e.Emp_ID = a.Emp_ID inner join Salary as s on e.Emp_ID = s.Emp_id; ");
            SqlCommand cmd = new SqlCommand(query1, con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Employee employee = new Employee();
                Address addr = new Address();
                Salary sal = new Salary();
                EmpDetails empd = new EmpDetails();
                employee.emp_id = Convert.ToInt32(rdr["Emp_ID"]);
                employee.emp_name = rdr["Emp_Name"].ToString();
                employee.salary = Convert.ToInt32(rdr["Salary"]);
                addr.address1 = rdr["Address1"].ToString();
                addr.address2 = rdr["Address2"].ToString();
                addr.city = rdr["city"].ToString();
                sal.hourly_wages = Convert.ToInt32(rdr["Hourly_wages"]);
                sal.payment_Date = rdr["Payment_Date"].ToString();
                empd.employee = employee;
                empd.addr = addr;
                empd.sal = sal;

                    listEmpDetails.Add(empd);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listEmpDetails));
            
            //return JsonConvert.SerializeObject(listEmployees, Newtonsoft.Json.Formatting.Indented);
        }

        [WebMethod]
        public void TestAllEmployees()
        {
            List<EmpDetails> listEmpDetails = new List<EmpDetails>();

            string cs = System.Configuration.ConfigurationManager.ConnectionStrings["servicestring"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            //string query1 = ("select e.Emp_ID, e.Emp_Name, e.Salary, a.Address1, a.Address2, a.city, s.Hourly_wages, s.Payment_Date from Employee as e inner join Address as a on e.Emp_ID = a.Emp_ID inner join Salary as s on e.Emp_ID = s.Emp_id; ");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "WebserviceDB";
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Employee employee = new Employee();
                Address addr = new Address();
                Salary sal = new Salary();
                EmpDetails empd = new EmpDetails();
                employee.emp_id = Convert.ToInt32(rdr["Emp_ID"]);
                employee.emp_name = rdr["Emp_Name"].ToString();
                employee.salary = Convert.ToInt32(rdr["Salary"]);
                addr.address1 = rdr["Address1"].ToString();
                addr.address2 = rdr["Address2"].ToString();
                addr.city = rdr["city"].ToString();
                sal.hourly_wages = Convert.ToInt32(rdr["Hourly_wages"]);
                sal.payment_Date = rdr["Payment_Date"].ToString();
                empd.employee = employee;
                empd.addr = addr;
                empd.sal = sal;

                listEmpDetails.Add(empd);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(listEmpDetails));

            //return JsonConvert.SerializeObject(listEmpDetails, Newtonsoft.Json.Formatting.Indented);
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

       
    }
}
