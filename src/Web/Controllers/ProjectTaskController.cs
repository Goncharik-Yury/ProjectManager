using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.ViewModels;
using TrainingTask.ApplicationCore.Dto;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingTask.ApplicationCore.Repository;
using TrainingTask.Common;

namespace TrainingTask.Controllers
{
    public class ProjectTaskController : Controller
    {
        private readonly ILogger logger;
        IProjectTaskRepositoryService<ProjectTaskDto> ProjectTaskRepositoryService;
        IRepositoryService<ProjectDto> ProjectRepositoryService;
        IRepositoryService<EmployeeDto> EmployeeRepositoryService;

        private readonly IConvert<ProjectTaskVm, ProjectTaskDto> ConvertToProjectTaskDto;
        private readonly IConvert<ProjectTaskDto, ProjectTaskVm> ConvertToProjectTaskVm;

        private readonly string[] ProjectTaskStatuses = { "NotStarted", "InProcess", "Completed", "Delayed" };

        public ProjectTaskController(
            ILogger logger,
            IProjectTaskRepositoryService<ProjectTaskDto> projectTaskRepositoryService,
            IRepositoryService<ProjectDto> projectRepositoryService,
            IRepositoryService<EmployeeDto> employeeRepositoryService,
        IConvert<ProjectTaskVm, ProjectTaskDto> convertToProjectTaskDto,
            IConvert<ProjectTaskDto, ProjectTaskVm> convertToProjectTaskVm
            )
        {
            this.logger = logger;

            ProjectTaskRepositoryService = projectTaskRepositoryService;
            ProjectRepositoryService = projectRepositoryService;
            EmployeeRepositoryService = employeeRepositoryService;

            ConvertToProjectTaskDto = convertToProjectTaskDto;
            ConvertToProjectTaskVm = convertToProjectTaskVm;
        }

        [HttpGet]
        public IActionResult Index()
        {
            logger.LogDebug($"ProjectTask.Index [post] is called");
            IList<ProjectTaskVm> ProjectTasksVm = ConvertToProjectTaskVm.ConvertAll(ProjectTaskRepositoryService.GetAll());
            return View(ProjectTasksVm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            logger.LogDebug($"ProjectTask.Create [get] is called");

            ViewBag.EmployeeSelectList = FillEmployeeSelectList();
            ViewBag.ProjectSelectList = GetProjectSelectList();

            ViewBag.ProjectTaskStatuses = ProjectTaskStatuses;

            ViewBag.AspAction = "Create";
            return View("CreateOrEdit");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            logger.LogDebug($"ProjectTask.Edit [get] is called");

            ViewBag.EmployeeSelectList = FillEmployeeSelectList();
            ViewBag.ProjectSelectList = GetProjectSelectList();

            ViewBag.ProjectTaskStatuses = ProjectTaskStatuses;

            ViewBag.AspAction = "Edit";
            ProjectTaskVm model = ConvertToProjectTaskVm.Convert(ProjectTaskRepositoryService.Get(id));
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
                    throw new NullReferenceException();
                ViewBag.ProjectTaskStatuses = ProjectTaskStatuses;
                if (ModelState.IsValid)
                {
                    ProjectTaskDto projectTaskDto = ConvertToProjectTaskDto.Convert(projectTask);
                    ProjectTaskRepositoryService.Create(projectTaskDto);
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
                    throw new NullReferenceException();
                ViewBag.ProjectTaskStatuses = ProjectTaskStatuses;
                if (ModelState.IsValid)
                {
                    ProjectTaskDto projectTaskDto = ConvertToProjectTaskDto.Convert(projectTask);
                    ProjectTaskRepositoryService.Update(projectTaskDto);
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
                ProjectTaskRepositoryService.Delete(id);
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
            IList<ProjectDto> ProjectsList = ProjectRepositoryService.GetAll();
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
            IList<EmployeeDto> EmployeesList = EmployeeRepositoryService.GetAll();
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

        private class EmployeeSelectListItem
        {
            public int Id { get; set; }
            public string FullName { get; set; }
        }
        private class ProjectSelectListItem
        {
            public int Id { get; set; }
            public string ShortName { get; set; }
        }
    }
}