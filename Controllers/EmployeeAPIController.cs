using Employee_CURD_API_june_july.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Employee_CURD_API_june_july.Controllers
{
    public class EmployeeAPIController : ApiController
    {
        DB_employee_JuneJulyEntities dbo = new DB_employee_JuneJulyEntities();
        [HttpGet]
        public IHttpActionResult SelectAllEmployees()
        {
            List<tblEmployee> EmpList = dbo.tblEmployees.ToList();
            if (EmpList != null)
            {
                return Ok(EmpList);
            }
            return NotFound();
            //return Ok(dbo.tblEmployees.ToList());
        }
        [HttpGet]
        public IHttpActionResult SelectAllEmployees(string ename)
        {
            List<tblEmployee> EmpList = dbo.tblEmployees.Where(x => x.name == ename).ToList();
            if (EmpList.Count>0)
            {
                return Ok(EmpList);
            }
            return NotFound();

        }
        [HttpPost]
        public IHttpActionResult AddEmp(tblEmployee emp)
        {
            dbo.tblEmployees.Add(emp);
            if (dbo.SaveChanges()>0)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet]
        // provide employee object as per id 
        public IHttpActionResult  searchEmpById(int id)
        {
            var emp = dbo.tblEmployees.Find(id);
            if (emp==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(emp);
            }
        }
        [HttpPut]
        public IHttpActionResult updateEmp(tblEmployee emp)
        {
            var e = dbo.tblEmployees.FirstOrDefault(x => x.id == emp.id);
            if (e==null)
            {
                return NotFound();
            }
            else
            {
                e.name = emp.name;
                e.salary = emp.salary;
                e.Dept = emp.Dept;
                if (dbo.SaveChanges()>0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }

        }

    }
}
