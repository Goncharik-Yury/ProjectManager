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
        private readonly ILogger logger;
        private readonly IService<EmployeeDto> EmployeeService;
        private readonly IConvert<EmployeeVm, EmployeeDto> ConvertToEmployeeDto;
        private readonly IConvert<EmployeeDto, EmployeeVm> ConvertToEmployeeVm;
        public EmployeeController(ILogger logger,
            IService<EmployeeDto> employeeService,
            IConvert<EmployeeVm, EmployeeDto> convertToEmployeeDto,
            IConvert<EmployeeDto, EmployeeVm> convertToEmployeeVm
            )
        {
            this.logger = logger;

            EmployeeService = employeeService;
            ConvertToEmployeeDto = convertToEmployeeDto;
            ConvertToEmployeeVm = convertToEmployeeVm;
        }

        [HttpGet]
        public IActionResult Index()
        {
            logger.LogDebug($"Employee.Index is called");
            IList<EmployeeDto> EmployeesDto = EmployeeService.GetAll();
            List<EmployeeVm> EmployeesVm = new List<EmployeeVm>();
            foreach (var item in EmployeesDto)
            {
                EmployeesVm.Add(ConvertToEmployeeVm.Convert(item));
            }

            return View(EmployeesVm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            logger.LogDebug($"Employee.Create [get] is called");
            return View("CreateOrEdit");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            logger.LogDebug($"Employee.Edit [get] is called");
            EmployeeDto EmployeeDto = EmployeeService.Get(id);
            EmployeeVm model = ConvertToEmployeeVm.Convert(EmployeeDto);
            return View("CreateOrEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeVm employee)
        {
            logger.LogDebug($"Employee.Create [post] is called");

            try
            {
                if (employee == null)
                    throw new ArgumentException();
                if (ModelState.IsValid)
                {
                    EmployeeService.Create(ConvertToEmployeeDto.Convert(employee));
                }
                else
                {
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error");
            }

            return RedirectToAction(nameof(Index));


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeVm employee)
        {
            logger.LogDebug($"Employee.Edit [post] is called");
            try
            {
                if (employee == null)
                    throw new ArgumentException();
                if (ModelState.IsValid)
                {
                    EmployeeService.Update(ConvertToEmployeeDto.Convert(employee));
                }
                else
                {
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            logger.LogDebug($"Employee.Delete is called");
            try
            {
                EmployeeService.Delete(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }
    }

}