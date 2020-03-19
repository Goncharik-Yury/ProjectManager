using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.ViewModels;
using TrainingTask.ApplicationCore.DBManipulators;
using TrainingTask.ApplicationCore.Validators;
using TrainingTask.Web.Converters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.Extensions.Primitives;

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
            return View(VMConverter.DTOtoVM(dbManipulator.GetEmployeesList()));
        }

        //public ActionResult Create(int id = -1)
        //{
        //    Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
        //    if (id < 0)
        //    {
        //        ViewBag.IsCreateNotEdit = true;
        //        return View();
        //    }
        //    else
        //    {
        //        ViewBag.IsCreateNotEdit = false;
        //        EmployeeVM model = ViewModelsConverter.DTOtoVM(dbManipulator.GetEmployeeById(id))[0];
        //        return View(model);
        //    }
        //}

        public ActionResult CreateOrEdit(int id = -1)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");

            ViewBag.ska = "skaka";

            if (id < 0)
            {
                ViewBag.IsCreateNotEdit = "true";
                return View();
            }
            else
            {
                ViewBag.IsCreateNotEdit = "false";
                EmployeeVM model = VMConverter.DTOtoVM(dbManipulator.GetEmployeeById(id))[0];
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(EmployeeVM employee, bool IsCreateNotEdit = false)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                Logger.LogTrace("Checking for employee is not null");
                if (employee == null)
                    throw new NullReferenceException();
                Logger.LogTrace("Running validation");
                //EmployeeValidate(employee); // TODO: Comment on testing
                Logger.LogTrace("Checking if ModelState IsValid");
                if (ModelState.IsValid)
                {
                    if (IsCreateNotEdit)
                    {
                        Logger.LogTrace("Create employee in database");
                        EmployeeDBManipulator.CreateEmployee(VMConverter.VMToDTO(employee));
                    }
                    else
                    {
                        Logger.LogTrace("Edit employee in database");
                        EmployeeDBManipulator.EditEmployee(employee.Id, VMConverter.VMToDTO(employee));
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

        private void EmployeeValidate(EmployeeVM employee)
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