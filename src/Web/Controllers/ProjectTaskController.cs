using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.ViewModels;
using TrainingTask.ApplicationCore.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingTask.ApplicationCore.Repository;
using TrainingTask.Common;

namespace TrainingTask.Controllers
{
    public class ProjectTaskController : Controller
    {
        private readonly ILogger logger;
        IProjectTaskService<ProjectTaskDto> ProjectTaskService;
        IService<ProjectDto> ProjectService;
        IService<EmployeeDto> EmployeeService;

        private readonly IConvert<ProjectTaskVm, ProjectTaskDto> ConvertToProjectTaskDto;
        private readonly IConvert<ProjectTaskDto, ProjectTaskVm> ConvertToProjectTaskVm;

        private readonly string[] ProjectTaskStatuses = { "NotStarted", "InProcess", "Completed", "Delayed" };

        public ProjectTaskController(
            ILogger logger,
            IProjectTaskService<ProjectTaskDto> projectTaskService,
            IService<ProjectDto> projectService,
            IService<EmployeeDto> employeeService,
        IConvert<ProjectTaskVm, ProjectTaskDto> convertToProjectTaskDto,
            IConvert<ProjectTaskDto, ProjectTaskVm> convertToProjectTaskVm
            )
        {
            this.logger = logger;

            ProjectTaskService = projectTaskService;
            ProjectService = projectService;
            EmployeeService = employeeService;

            ConvertToProjectTaskDto = convertToProjectTaskDto;
            ConvertToProjectTaskVm = convertToProjectTaskVm;
        }

        [HttpGet]
        public IActionResult Index()
        {
            logger.LogDebug($"ProjectTask.Index [post] is called");
            IList<ProjectTaskVm> ProjectTasksVm = ConvertToProjectTaskVm.Convert(ProjectTaskService.GetAll());
            return View(ProjectTasksVm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            logger.LogDebug($"ProjectTask.Create [get] is called");
            ViewBag.EmployeeSelectList = FillEmployeeSelectList();
            ViewBag.ProjectSelectList = GetProjectSelectList();
            ViewBag.ProjectTaskStatuses = ProjectTaskStatuses;
            return View("CreateOrEdit");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            logger.LogDebug($"ProjectTask.Edit [get] is called");
            ViewBag.EmployeeSelectList = FillEmployeeSelectList();
            ViewBag.ProjectSelectList = GetProjectSelectList();
            ViewBag.ProjectTaskStatuses = ProjectTaskStatuses;
            ProjectTaskVm model = ConvertToProjectTaskVm.Convert(ProjectTaskService.Get(id));
            return View("CreateOrEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProjectTaskVm projectTask)
        {
            logger.LogDebug($"ProjectTask.Create [post] is called");
            try
            {
                if (projectTask == null)
                    throw new ArgumentException();
                ViewBag.ProjectTaskStatuses = ProjectTaskStatuses;
                if (ModelState.IsValid)
                {
                    ProjectTaskDto projectTaskDto = ConvertToProjectTaskDto.Convert(projectTask);
                    ProjectTaskService.Create(projectTaskDto);
                }
                else
                {
                    ViewBag.EmployeeSelectList = FillEmployeeSelectList();
                    ViewBag.ProjectSelectList = GetProjectSelectList();
                    return View(projectTask);
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
        public IActionResult Edit(ProjectTaskVm projectTask)
        {
            logger.LogDebug($"ProjectTask.Edit [post] is called");
            try
            {
                if (projectTask == null)
                    throw new ArgumentException();
                ViewBag.ProjectTaskStatuses = ProjectTaskStatuses;
                if (ModelState.IsValid)
                {
                    ProjectTaskDto projectTaskDto = ConvertToProjectTaskDto.Convert(projectTask);
                    ProjectTaskService.Update(projectTaskDto);
                }
                else
                {
                    ViewBag.EmployeeSelectList = FillEmployeeSelectList();
                    ViewBag.ProjectSelectList = GetProjectSelectList();
                    return View(projectTask);
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
            logger.LogDebug($"ProjectTask.Delete is called");
            try
            {
                ProjectTaskService.Delete(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

        private SelectList GetProjectSelectList()
        {
            IList<ProjectDto> ProjectsList = ProjectService.GetAll();
            List<ProjectSelectListItem> ProjectSelectList = new List<ProjectSelectListItem>();
            foreach (var item in ProjectsList)
            {
                ProjectSelectList.Add(new ProjectSelectListItem
                {
                    Id = item.Id,
                    ShortName = item.ShortName
                });
            }

            return new SelectList(ProjectSelectList, "Id", "ShortName");
        }
        private SelectList FillEmployeeSelectList()
        {
            IList<EmployeeDto> EmployeesList = EmployeeService.GetAll();
            List<EmployeeSelectListItem> EmployeeSelectList = new List<EmployeeSelectListItem>();
            foreach (var item in EmployeesList)
            {
                EmployeeSelectList.Add(new EmployeeSelectListItem
                {
                    Id = item.Id,
                    FullName = $"{item.LastName} {item.FirstName} {item.Patronymic}"
                });
            }

            return new SelectList(EmployeeSelectList, "Id", "FullName");
        }
    }
}