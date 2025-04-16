using Employee_CURD_API_june_july.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace Employee_CURD_API_june_july.Controllers
{
    public class TryController : Controller
    {
        // GET: Try
        public ActionResult Index()
        {
            DB_employee_JuneJulyEntities dbo = new DB_employee_JuneJulyEntities();
            List<tblEmployee> emplist = dbo.tblEmployees.ToList();
            return View(emplist);
        }
        [HttpGet]
        public ActionResult insertEmp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult insertEmp(tblEmployee emp)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44365/api/TryAPI");
                var response = client.PostAsJsonAsync("TryAPI", emp);
                response.Wait();

                var test = response.Result;
                if (test.IsSuccessStatusCode)
                {
                    ViewBag.msg = "Record inserted Successfully";
                    return View();
                }

            }
            ViewBag.msg = "Record not inserted!!!!!";
            return View();
        }
        [HttpGet]
        public ActionResult updateEmp(int id)
        {
            DB_employee_JuneJulyEntities dbo = new DB_employee_JuneJulyEntities();
            var emp = dbo.tblEmployees.FirstOrDefault(x => x.id == id);

            return View(emp);
        }
        [HttpPost]
        public ActionResult updateEmp(tblEmployee emp)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress=new Uri("https://localhost:44365/api/TryAPI");
                var response = client.PutAsJsonAsync("TryAPI", emp);
                response.Wait();

                var test = response.Result;
                if (test.IsSuccessStatusCode)
                {

                    return RedirectToAction("index");

                }
            }
            ViewBag.msg = "record NOT updated!!!!";
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress=new Uri("https://localhost:44365/api/TryAPI");
            var response = client.DeleteAsync("TryAPI/" + id);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("index");

        }
       
    }
}