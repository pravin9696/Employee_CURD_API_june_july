using Employee_CURD_API_june_july.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Employee_CURD_API_june_july.Controllers
{
    public class TryAPIController : ApiController
    {
        DB_employee_JuneJulyEntities dbo = new DB_employee_JuneJulyEntities();
        [HttpPost]
        public IHttpActionResult insertEmployee(tblEmployee emp)
        {
            dbo.tblEmployees.Add(emp);
            if (dbo.SaveChanges()>0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPut]
        public IHttpActionResult UpdateEmp(tblEmployee emp)
        {
            var e = dbo.tblEmployees.FirstOrDefault(x => x.id == emp.id);
            if (e==null)
            {
                return NotFound();
            }
            else
            {
                e.Dept = emp.Dept;
                e.salary = emp.salary;
                e.name = emp.name;
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
        [HttpDelete]
        public IHttpActionResult deleteEmp(int id)
        {
            var emp = dbo.tblEmployees.FirstOrDefault(x => x.id == id);
            if (emp==null)
            {
                return NotFound();
            }
            else
            {
                dbo.tblEmployees.Remove(emp);
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
