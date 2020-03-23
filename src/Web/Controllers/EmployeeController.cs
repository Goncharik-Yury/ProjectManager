using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.ViewModels;
using TrainingTask.Web.Converters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using ApplicationCore.Repository;
using TrainingTask.ApplicationCore.Dto;

namespace TrainingTask.Controllers
{
    public class EmployeeController : Controller
    {
        private ILogger Logger;
        readonly IRepositoryService<EmployeeDto> EmployeeRepositoryService;
        readonly IConvertWeb<EmployeeVm, EmployeeDto> EmployeeConverter;
        public EmployeeController(ILogger logger, IConvertWeb<EmployeeVm, EmployeeDto> EmployeeConverter)
        {
            Logger = logger;
            Logger.LogDebug($"Employee controller is created");
            EmployeeRepositoryService = new EmployeeRepositoryService();
            this.EmployeeConverter = EmployeeConverter;
        }

        public ActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            List<EmployeeDto> EmployeesDto = EmployeeRepositoryService.GetAll();
            List<EmployeeVm> EmployeesVm = new List<EmployeeVm>();
            foreach (var item in EmployeesDto)
            {
                EmployeesVm.Add(EmployeeConverter.Convert(item));
            }
            return View(EmployeesVm);
        }

        public ActionResult CreateOrEdit(int id = -1)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");

            if (id < 0)
            {
                ViewBag.IsCreateNotEdit = "true";
                return View();
            }
            else
            {
                ViewBag.IsCreateNotEdit = "false";
                EmployeeVm model = EmployeeConverter.Convert(EmployeeRepositoryService.GetSingle(id));
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(EmployeeVm employee, bool IsCreateNotEdit = false)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                if (employee == null)
                    throw new NullReferenceException();
                if (ModelState.IsValid)
                {
                    if (IsCreateNotEdit)
                    {
                        EmployeeRepositoryService.Create(EmployeeConverter.Convert(employee));
                    }
                    else
                    {
                        EmployeeRepositoryService.Update(EmployeeConverter.Convert(employee));
                    }
                }
                else
                {
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return View("Error");
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
                EmployeeRepositoryService.Delete(id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }
    }

}