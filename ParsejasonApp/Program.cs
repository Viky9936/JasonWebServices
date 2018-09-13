using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ParsejasonApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //create an instance of HttpClient
            HttpClient client = new HttpClient();

            //DefaultRequestHeader to Json
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Create an instance of HttpResponse & invoke the service asynchronously
            HttpResponseMessage response = client.GetAsync("http://localhost:51545/TestWebService.asmx/TestAllEmployees").Result;

            if (response.IsSuccessStatusCode)
            {
                //Read response content result into string variable
                string json = response.Content.ReadAsStringAsync().Result;

                //Deserialize to strongly typed class i.e., RootObject
                EmpDetails jobj = JsonConvert.DeserializeObject<EmpDetails>(json);

                //loop through the list and insert into database
                foreach(Employee emp in jobj.Employee)
                {

                }
            }
        }
    }
}
