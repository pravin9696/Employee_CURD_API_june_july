using Employee_CURD_API_june_july.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Employee_CURD_API_june_july.Controllers
{
    [HandleError(View = "Error")]
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
        [HandleError]
        public ActionResult temp()
        {
            ViewBag.msg = "welcome";
            int i = Convert.ToInt32("");
            return View();
        }
        [HandleError]
        public ActionResult aa()
        {
            int a = 0;
            int b = 100 / a;
            return View();
        }

        [HttpGet]
        public ActionResult InsertEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertEmployee(tblEmployee emp)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44365/api/employeeapi");
            
            var response = client.PostAsJsonAsync<tblEmployee>("employeeapi",emp);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowAllEmployee");
            }
            else
            {
                ViewBag.msg = "Record not Inserted!!!";
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44365/api/employeeapi");
            var response = client.GetAsync("employeeapi/" + id);
            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                var temp = test.Content.ReadAsAsync<tblEmployee>();
                temp.Wait();
                var e = temp.Result;
                return View(e);
            }
            else
            {

                return RedirectToAction("ShowAllEmployee");
            }
        }
        [HttpPost]
        public ActionResult Edit(tblEmployee emp)
        {
            if (ModelState.IsValid)
            {

            HttpClient client = new HttpClient();
            client.BaseAddress=new Uri("https://localhost:44365/api/employeeapi");
            var response = client.PutAsJsonAsync<tblEmployee>("employeeapi", emp);
                response.Wait();

                var test = response.Result;
                if (test.IsSuccessStatusCode)
                {
                    return RedirectToAction("ShowAllEmployee");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
    }
}