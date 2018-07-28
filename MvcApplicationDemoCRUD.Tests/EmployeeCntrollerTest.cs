using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc; //adding the MVC namespace
using MvcApplicationDemoCRUD.Controllers; //adding the namespace of the project whose method needs to be tested

namespace MvcApplicationDemoCRUD.Tests
{
    [TestClass]
    public class EmployeeCntrollerTest
    {
        [TestMethod]
        public void IndexActionReturnsIndexView()
        {
            string expected = "Index";
            EmployeeController controller = new EmployeeController();

            var result = controller.Index() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        [HttpGet]
        public void CreateActionReturnsCreateView()
        {
            string expected = "Create";
            EmployeeController controller = new EmployeeController();

            var result = controller.Index() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }
    }
}
