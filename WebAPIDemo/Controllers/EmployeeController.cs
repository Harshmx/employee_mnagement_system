using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class EmployeeController : ApiController
    {
        private EmployeeDB db = new EmployeeDB();

        [ActionName("GetEmployees")]
        public List<Employee> Get(Nullable<int> Id)
        {
            if(Id == null)
            return db.Employees.ToList();
            else
                return db.Employees.Where(emp => emp.ID == Id).ToList();
        }
        [ActionName("AddEmployee")]
        public HttpStatusCode Post(Employee emp)
        {
            db.Employees.Add(emp);
            db.SaveChanges();
            return HttpStatusCode.OK;
            
        }
        [ActionName("UpdateEmployee")]
        public HttpStatusCode Put(Employee emp)
        {
            db.Entry(emp).State = EntityState.Modified;
            db.SaveChanges();
            return HttpStatusCode.OK;
        }
        [ActionName("DeleteEmployee")]
        public HttpStatusCode Delete(int Id)
        {
            Employee employee = db.Employees.Find(Id);
            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
            }    
            return HttpStatusCode.OK;
        }
       
    }
}
