using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.Models;
using TrainingTask.ApplicationCore.Functional;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Web.Functional;
using TrainingTask.ApplicationCore.Validation;

namespace TrainingTask.Controllers
{
    public class EmployeeController : Controller
    {
        readonly DBEmployeeManipulator dbManipulator = new DBEmployeeManipulator();

        public ActionResult Index()
        {
            return View(ConverterViewModel.EmployeeDTOtoViewModel(dbManipulator.GetEmployeesList()));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel employee)
        {
            EmployeeValidate(employee);
            if (ModelState.IsValid)
            {
                try
                {
                    DBEmployeeManipulator.CreateEmployee(ConverterViewModel.EmployeeViewModelToDTO(employee));
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public ActionResult Edit(int id)
        {
            EmployeeViewModel model = ConverterViewModel.EmployeeDTOtoViewModel(dbManipulator.GetEmployeeById(id))[0];
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeViewModel employee)
        {
            EmployeeValidate(employee);
            if (ModelState.IsValid)
            {
                try
                {
                    DBEmployeeManipulator.EditEmployee(employee.Id, ConverterViewModel.EmployeeViewModelToDTO(employee));

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    throw;
                }

            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                DBEmployeeManipulator.DeleteEmployee(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(nameof(Index));
            }
        }

        private void EmployeeValidate(EmployeeViewModel employee)
        {
            const string TooLongString = "Too long";
            const string InvalidValue = "Invalid value";
            const int MaxLength = 50;

            if (!Validator.NameIsValid(employee.LastName))
            {
                ModelState.AddModelError("LastName", InvalidValue);
            }
            if (!Validator.LengthIsValid(employee.LastName, MaxLength))
            {
                ModelState.AddModelError("LastName", TooLongString);
            }
            if (!Validator.NameIsValid(employee.FirstName))
            {
                ModelState.AddModelError("FirstName", InvalidValue);
            }
            if (!Validator.LengthIsValid(employee.FirstName, MaxLength))
            {
                ModelState.AddModelError("FirstName", TooLongString);
            }
        }
    }
}