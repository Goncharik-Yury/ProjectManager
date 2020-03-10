using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.WEB.Models;
using TrainingTask.BLL.Functional;
using TrainingTask.BLL.DTO;
using TrainingTask.WEB.Functional;

namespace TrainingTask.Controllers
{
    public class EmployeeController : Controller
    {
        DBEmployeeManipulator dbManipulator = new DBEmployeeManipulator();

        public ActionResult Index()
        {            
            return View(ViewModelConverter.EmployeeDTOtoViewModel(dbManipulator.GetEmployeesList()));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeDTO employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            try
            {
                dbManipulator.CreateEmployee(employee);
            }
            catch
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            return View(ViewModelConverter.EmployeeDTOtoViewModel(dbManipulator.GetEmployeeById(id))[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeDTO employee)
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
                return View(nameof(Index));
            }
        }
    }
}