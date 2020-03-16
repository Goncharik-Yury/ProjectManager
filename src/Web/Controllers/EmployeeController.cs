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

        readonly DBEmployeeManipulator dbManipulator = new DBEmployeeManipulator();

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

        public ActionResult Create()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel employee)
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
                    Logger.LogTrace("Adding employee to database");
                    DBEmployeeManipulator.CreateEmployee(EmployeeConverter.ViewModelToDTO(employee));
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

        public ActionResult Edit(int id)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            EmployeeViewModel model = EmployeeConverter.DTOtoViewModel(dbManipulator.GetEmployeeById(id))[0];
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeViewModel employee)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                if (employee == null)
                    throw new NullReferenceException();
                EmployeeValidate(employee);
                if (ModelState.IsValid)
                {
                    DBEmployeeManipulator.EditEmployee(employee.Id, EmployeeConverter.ViewModelToDTO(employee));
                }
                else
                {
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                DBEmployeeManipulator.DeleteEmployee(id);
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