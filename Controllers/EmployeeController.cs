using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeCRUD.Models;

namespace EmployeeCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployeeData()
        {
            using (EmployeeCRUDDBEntities db = new EmployeeCRUDDBEntities())
            {
                var employees = db.EmployeeAccounts.OrderBy(a => a.employeeLastName).ToList();
                return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (EmployeeCRUDDBEntities db = new EmployeeCRUDDBEntities())
            {
                var v = db.EmployeeAccounts.Where(a => a.employeeID == id).FirstOrDefault();
                return View(v);
            }
        }

        [HttpPost]
        public ActionResult Save(EmployeeAccount employeeAccount)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (EmployeeCRUDDBEntities db = new EmployeeCRUDDBEntities())
                {
                    if (employeeAccount.employeeID > 0)
                    {
                        // edit
                        var v = db.EmployeeAccounts.Where(a => a.employeeID == employeeAccount.employeeID).FirstOrDefault();
                        
                        if (v != null)
                        {
                            v.employeeFirstName = employeeAccount.employeeFirstName;
                            v.employeeLastName = employeeAccount.employeeLastName;
                            v.employeePosition = employeeAccount.employeePosition;
                            v.employeeOffice = employeeAccount.employeeOffice;
                            v.employeeSalary = employeeAccount.employeeSalary;
                        }
                    }
                    else
                    {
                        // save
                        db.EmployeeAccounts.Add(employeeAccount);
                    }

                    db.SaveChanges();
                    status = true;
                }
            }

            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (EmployeeCRUDDBEntities db = new EmployeeCRUDDBEntities())
            {
                var v = db.EmployeeAccounts.Where(a => a.employeeID == id).FirstOrDefault();

                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;
            using (EmployeeCRUDDBEntities db = new EmployeeCRUDDBEntities())
            {
                var v = db.EmployeeAccounts.Where(a => a.employeeID == id).FirstOrDefault();
                if (v != null)
                {
                    db.EmployeeAccounts.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}