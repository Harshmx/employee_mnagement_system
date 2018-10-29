using MvcApplicationDemoCRUD.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace MvcApplicationDemoCRUD.Controllers
{
    public class EmployeeController : Controller
    {

        //displays the list of employees
        public ActionResult Index()
        {
            string UrlStart = ConfigurationManager.AppSettings["UrlStart"];
            string Url = UrlStart + "/employee/GetEmployees";
            //string Url = "https://employeemanagementapplication.azurewebsites.net/api/employee/GetEmployees";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader resStream = new StreamReader(response.GetResponseStream());
            string jSonResponse = resStream.ReadToEnd();
            List<Employee> Employees = JsonConvert.DeserializeObject<List<Employee>>(jSonResponse);
            return View(Employees);
        }

        //displays the form to create an employee
        public ActionResult Create()
        {
            return View("Create");
        }

        //creates an employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee Employee)
        {
            if (ModelState.IsValid)
            {
                string UrlStart = ConfigurationManager.AppSettings["UrlStart"];
                string JsonInput = JsonConvert.SerializeObject(Employee);
                byte[] dataStream = Encoding.UTF8.GetBytes(JsonInput);
                string request = UrlStart + "/employee/AddEmployee";
                WebRequest webRequest = WebRequest.Create(request);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                webRequest.ContentLength = dataStream.Length;
                Stream newStream = webRequest.GetRequestStream();
                newStream.Write(dataStream, 0, dataStream.Length);
                newStream.Close();
                WebResponse webResponse = webRequest.GetResponse();
                string responsestring = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                return RedirectToAction("Index");
            }

            return View(Employee);
        }

        //displays the form to edit an employee's detail
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string UrlStart = ConfigurationManager.AppSettings["UrlStart"];
            string Url =UrlStart + "/employee/GetEmployees?Id=" + id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader resStream = new StreamReader(response.GetResponseStream());
            string jSonResponse = resStream.ReadToEnd();
            List<Employee> Employee = JsonConvert.DeserializeObject<List<Employee>>(jSonResponse);
            if (Employee == null)
            {
                return HttpNotFound();
            }
            return View(Employee[0]);
        }

        //updates the employee's detail 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee Employee)
        {
            if (ModelState.IsValid)
            {
                string UrlStart = ConfigurationManager.AppSettings["UrlStart"];
                string JsonInput = JsonConvert.SerializeObject(Employee);
                byte[] dataStream = Encoding.UTF8.GetBytes(JsonInput);
                string request = UrlStart+ "/employee/UpdateEmployee?Id=" + Employee.ID;
                WebRequest webRequest = WebRequest.Create(request);
                webRequest.Method = "PUT";
                webRequest.ContentType = "application/json";
                webRequest.ContentLength = dataStream.Length;
                Stream newStream = webRequest.GetRequestStream();
                newStream.Write(dataStream, 0, dataStream.Length);
                newStream.Close();
                WebResponse webResponse = webRequest.GetResponse();
                string responsestring = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                return RedirectToAction("Index");
            }
            return View(Employee);
        }

        //displays the form to delete an employee's detail
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string UrlStart = ConfigurationManager.AppSettings["UrlStart"];
            string Url = UrlStart + "/employee/GetEmployees?Id=" + id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader resStream = new StreamReader(response.GetResponseStream());
            string jSonResponse = resStream.ReadToEnd();
            List<Employee> Employee = JsonConvert.DeserializeObject<List<Employee>>(jSonResponse);
            if (Employee == null)
            {
                return HttpNotFound();
            }
            return View(Employee[0]);
        }

        //deletes an employee
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //call API
            string JsonInput = JsonConvert.SerializeObject(id);
            byte[] dataStream = Encoding.UTF8.GetBytes(JsonInput);
            string UrlStart = ConfigurationManager.AppSettings["UrlStart"];
            string request = UrlStart + "/employee/DeleteEmployee?Id=" + id;
            WebRequest webRequest = WebRequest.Create(request);
            webRequest.Method = "DELETE";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = dataStream.Length;
            Stream newStream = webRequest.GetRequestStream();
            // Send the data.
            newStream.Write(dataStream, 0, dataStream.Length);
            newStream.Close();
            WebResponse webResponse = webRequest.GetResponse();
            string responsestring = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
            return RedirectToAction("Index");
        }

    
    }
}
