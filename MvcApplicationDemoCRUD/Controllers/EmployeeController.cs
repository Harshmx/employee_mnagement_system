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

namespace MvcApplicationDemoCRUD.Controllers
{
    public class EmployeeController : Controller
    {


        public ActionResult Index()
        {
            //return View(db.Students.ToList());

            string Url = "http://localhost:16559/api/employee/GetEmployees";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader resStream = new StreamReader(response.GetResponseStream());
            string jSonResponse = resStream.ReadToEnd();
            List<Employee> Employees = JsonConvert.DeserializeObject<List<Employee>>(jSonResponse);
            return View(Employees);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // Student student = db.Students.Find(id);
            //if (student == null)
            //{
            //    return HttpNotFound();
            //}
           // return View(student);
            return View();
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee Employee)
        {
            if (ModelState.IsValid)
            {
                string JsonInput = JsonConvert.SerializeObject(Employee);
                byte[] dataStream = Encoding.UTF8.GetBytes(JsonInput);
                string request = "http://localhost:16559/api/employee/AddEmployee";
                WebRequest webRequest = WebRequest.Create(request);
                webRequest.Method = "POST";
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

            return View(Employee);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //to be asked
            string Url = "http://localhost:16559/api/employee/GetEmployees?Id=" + id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader resStream = new StreamReader(response.GetResponseStream());
            string jSonResponse = resStream.ReadToEnd();
            List<Employee> Employee = JsonConvert.DeserializeObject<List<Employee>>(jSonResponse);
            //Employee Employee = null;// db.Students.Find(id);
            if (Employee == null)
            {
                return HttpNotFound();
            }
            return View(Employee[0]);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee Employee)
        {
            if (ModelState.IsValid)
            {
                //call API
                string JsonInput = JsonConvert.SerializeObject(Employee);
                byte[] dataStream = Encoding.UTF8.GetBytes(JsonInput);
                string request = "http://localhost:16559/api/employee/UpdateEmployee?Id=" + Employee.ID;
                WebRequest webRequest = WebRequest.Create(request);
                webRequest.Method = "PUT";
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
            return View(Employee);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //to be asked
            string Url = "http://localhost:16559/api/employee/GetEmployees?Id=" + id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader resStream = new StreamReader(response.GetResponseStream());
            string jSonResponse = resStream.ReadToEnd();
            List<Employee> Employee = JsonConvert.DeserializeObject<List<Employee>>(jSonResponse);

            //Employee Employee = null;//call API
            if (Employee == null)
            {
                return HttpNotFound();
            }
            return View(Employee[0]);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //call API
            string JsonInput = JsonConvert.SerializeObject(id);
            byte[] dataStream = Encoding.UTF8.GetBytes(JsonInput);
            string request = "http://localhost:16559/api/employee/DeleteEmployee?Id=" + id;
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
