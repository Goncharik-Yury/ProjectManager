using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.ViewModels;
using Microsoft.Extensions.Logging;
using TrainingTask.ApplicationCore.Repository;
using TrainingTask.ApplicationCore.Dto;
using TrainingTask.Common;

namespace TrainingTask.Controllers
{
    public class EmployeeController : Controller
    {
        private ILogger Logger;
        readonly IRepositoryService<EmployeeDto> EmployeeRepositoryService;
        readonly IConvert<EmployeeVm, EmployeeDto> ConvertToEmployeeDto;
        readonly IConvert<EmployeeDto, EmployeeVm> ConvertToEmployeeVm;
        public EmployeeController(ILogger logger,
            IRepositoryService<EmployeeDto> employeeRepositoryService,
            IConvert<EmployeeVm, EmployeeDto> convertToEmployeeDto,
            IConvert<EmployeeDto, EmployeeVm> convertToEmployeeVm
            )
        {
            Logger = logger;

            EmployeeRepositoryService = employeeRepositoryService;
            ConvertToEmployeeDto = convertToEmployeeDto;
            ConvertToEmployeeVm = convertToEmployeeVm;
        }

        public IActionResult Index()
        {
            Logger.LogDebug($"Employee.Index is called");
            IList<EmployeeDto> EmployeesDto = EmployeeRepositoryService.GetAll();
            List<EmployeeVm> EmployeesVm = new List<EmployeeVm>();
            foreach (var item in EmployeesDto)
            {
                EmployeesVm.Add(ConvertToEmployeeVm.Convert(item));
            }
            return View(EmployeesVm);
        }

        public IActionResult Create()
        {
            Logger.LogDebug($"Employee.Create is called");
            ViewBag.AspAction = "Create";
            return View("CreateOrEdit");
        }

        public IActionResult Edit(int id)
        {
            Logger.LogDebug($"Employee.Edit is called");
            EmployeeDto EmployeeDto = EmployeeRepositoryService.GetSingle(id);
            EmployeeVm model = ConvertToEmployeeVm.Convert(EmployeeDto);
            ViewBag.AspAction = "Edit";
            return View("CreateOrEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeVm employee)
        {
            Logger.LogDebug($"Employee.Create is called");

            try
            {
                if (employee == null)
                    throw new NullReferenceException();
                if (ModelState.IsValid)
                {
                    EmployeeRepositoryService.Create(ConvertToEmployeeDto.Convert(employee));
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
        public IActionResult Edit(EmployeeVm employee)
        {
            Logger.LogDebug($"Employee.Edit is called");
            try
            {
                if (employee == null)
                    throw new NullReferenceException();
                if (ModelState.IsValid)
                {
                    EmployeeRepositoryService.Update(ConvertToEmployeeDto.Convert(employee));
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
        public IActionResult Delete(int id)
        {
            Logger.LogDebug($"Employee.Delete is called");
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