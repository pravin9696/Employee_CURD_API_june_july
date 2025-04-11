using Employee_CURD_API_june_july.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Employee_CURD_API_june_july.Controllers
{
    public class EmpController : Controller
    {
        // GET: Emp
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowAllEmployee()
        {
            List<tblEmployee> empList = new List<tblEmployee>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44365/api/employeeapi");
            var response = client.GetAsync("employeeapi");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode==true)
            {
                var temp = test.Content.ReadAsAsync<List<tblEmployee>>();
                temp.Wait();
               empList= temp.Result;
            }
            return View(empList);
        }
        public ActionResult SearchEmpByName()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchEmpByName(tblEmployee emp)
        {
            List<tblEmployee> empList = new List<tblEmployee>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44365/api/employeeapi");
            var response = client.GetAsync("employeeapi?ename="+emp.name);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode == true)
            {
                var temp = test.Content.ReadAsAsync<List<tblEmployee>>();
                temp.Wait();
                empList = temp.Result;
            }
            TempData["abcd"] = empList;
            return RedirectToAction("showEmps", empList);
           
        }
        [HttpGet]
        public ActionResult showEmps(List<tblEmployee> elist)
        {
            elist = (List<tblEmployee>)TempData["abcd"];
            return View(elist);
        }
    }
}