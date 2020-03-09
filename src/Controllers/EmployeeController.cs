using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training_task.Utility;

namespace Training_task.Models
{
    public class EmployeeController : Controller
    {
        DBEmployeeManipulator dbManipulator = new DBEmployeeManipulator();

        public ActionResult Index()
        {
            return View(dbManipulator.GetEmployeesList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            try
            {
                dbManipulator.CreateEmployee(employee);

                //return RedirectToAction(nameof(Index));

            }
            catch
            {
                throw;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(dbManipulator.GetEmployeeById(id)[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                dbManipulator.EditEmployee(employee.Id, employee);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
                //return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                dbManipulator.DeleteEmployee(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}