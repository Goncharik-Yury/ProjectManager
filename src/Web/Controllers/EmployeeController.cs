using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.Models;
using TrainingTask.ApplicationCore.DBManipulators;
using TrainingTask.ApplicationCore.Validators;
using TrainingTask.Web.Converters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace TrainingTask.Controllers
{
    public class EmployeeController : Controller
    {

        readonly EmployeeDBManipulator dbManipulator = new EmployeeDBManipulator();

        private ILogger Logger;
        public EmployeeController(ILogger logger)
        {
            Logger = logger;
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
        }

        public ActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            return View(EmployeeConverter.DTOtoViewModel(dbManipulator.GetEmployeesList()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(EmployeeViewModel employee)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                Logger.LogTrace("Checking for employee is not null");
                if (employee == null)
                    throw new NullReferenceException();
                Logger.LogTrace("Running validation");
                EmployeeValidate(employee); // TODO: Comment on testing
                Logger.LogTrace("Checking if ModelState IsValid");
                if (ModelState.IsValid)
                {
                    Logger.LogTrace("CreateOrEdit employee in database");
                    if (employee.IsCreateNotEdit)
                    {
                        EmployeeDBManipulator.CreateEmployee(EmployeeConverter.ViewModelToDTO(employee));
                    }
                    else
                    {
                        EmployeeDBManipulator.EditEmployee(employee.Id, EmployeeConverter.ViewModelToDTO(employee));
                    }
                }
                else
                {
                    Logger.LogTrace("returning last page to fix the issues");
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                Logger.LogTrace("Logging error occured");
                Logger.LogError(ex.Message);
            }
            Logger.LogTrace("Redirecting to action");
            return RedirectToAction(nameof(Index));
        }

        public ActionResult CreateOrEdit(int id = -1)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            if (id < 0)
            {
                ViewBag.IsCreateNotEdit = true;
                return View();
            }
            else
            {
                ViewBag.IsCreateNotEdit = false;
                EmployeeViewModel model = EmployeeConverter.DTOtoViewModel(dbManipulator.GetEmployeeById(id))[0];
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                EmployeeDBManipulator.DeleteEmployee(id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        private void EmployeeValidate(EmployeeViewModel employee)
        {
            const string TooLongString = "Too long";
            const string InvalidValue = "Invalid value";
            const int MaxLength = 50;
            Logger.LogTrace("Checking employee.LastName is valid value");
            if (!Validator.NameIsValid(employee.LastName))
            {
                ModelState.AddModelError("LastName", InvalidValue);
            }
            Logger.LogTrace("Checking employee.LastName is valid length");
            if (!Validator.LengthIsValid(employee.LastName, MaxLength))
            {
                ModelState.AddModelError("LastName", TooLongString);
            }
            Logger.LogTrace("Checking employee.FirstName is valid value");
            if (!Validator.NameIsValid(employee.FirstName))
            {
                ModelState.AddModelError("FirstName", InvalidValue);
            }
            Logger.LogTrace("Checking employee.FirstName is valid length");
            if (!Validator.LengthIsValid(employee.FirstName, MaxLength))
            {
                ModelState.AddModelError("FirstName", TooLongString);
            }
        }
    }
}