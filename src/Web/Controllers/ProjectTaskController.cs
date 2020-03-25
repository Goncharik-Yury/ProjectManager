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
        private ILogger Logger;
        IProjectTaskRepositoryService<ProjectTaskDto> ProjectTaskRepositoryService;
        IRepositoryService<ProjectDto> ProjectRepositoryService;
        IRepositoryService<EmployeeDto> EmployeeRepositoryService;

        readonly IConvert<ProjectTaskVm, ProjectTaskDto> ConvertToProjectTaskDto;
        readonly IConvert<ProjectTaskDto, ProjectTaskVm> ConvertToProjectTaskVm;

        string[] ProjectTaskStatuses = { "NotStarted", "InProcess", "Completed", "Delayed" };

        public ProjectTaskController(
            ILogger logger,
            IProjectTaskRepositoryService<ProjectTaskDto> projectTaskRepositoryService,
            IRepositoryService<ProjectDto> projectRepositoryService,
            IRepositoryService<EmployeeDto> employeeRepositoryService,
        IConvert<ProjectTaskVm, ProjectTaskDto> convertToProjectTaskDto,
            IConvert<ProjectTaskDto, ProjectTaskVm> convertToProjectTaskVm
            )
        {
            Logger = logger;

            ProjectTaskRepositoryService = projectTaskRepositoryService;
            ProjectRepositoryService = projectRepositoryService;
            EmployeeRepositoryService = employeeRepositoryService;

            ConvertToProjectTaskDto = convertToProjectTaskDto;
            ConvertToProjectTaskVm = convertToProjectTaskVm;
        }

        public IActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            IList<ProjectTaskVm> ProjectTasksVm = ConvertToProjectTaskVm.ConvertAll(ProjectTaskRepositoryService.GetAll());
            return View(ProjectTasksVm);
        }

        public IActionResult CreateOrEdit(int id = -1)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");

            FillEmployeeSelectList();
            FillProjectSelectList();

            ViewBag.ProjectTaskStatuses = ProjectTaskStatuses;

            if (id < 0)
            {
                ViewBag.IsCreateNotEdit = "true";
                return View();
            }
            else
            {
                ViewBag.IsCreateNotEdit = "false";
                ProjectTaskVm model = ConvertToProjectTaskVm.Convert(ProjectTaskRepositoryService.GetSingle(id));
                return View(model);
            }
        }
        private void FillProjectSelectList()
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
            ViewBag.ProjectSelectList = new SelectList(ProjectSelectList, "Id", "ShortName");
        }
        private void FillEmployeeSelectList()
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
            ViewBag.EmployeeSelectList = new SelectList(EmployeeSelectList, "Id", "FullName");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(ProjectTaskVm projectTask, bool IsCreateNotEdit = false)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                if (projectTask == null)
                    throw new NullReferenceException();
                ViewBag.ProjectTaskStatuses = ProjectTaskStatuses;
                if (ModelState.IsValid)
                {
                    ProjectTaskDto projectTaskDto = ConvertToProjectTaskDto.Convert(projectTask);
                    if (IsCreateNotEdit)
                    {
                        ProjectTaskRepositoryService.Create(projectTaskDto);
                    }
                    else
                    {
                        ProjectTaskRepositoryService.Update(projectTaskDto);
                    }
                }
                else
                {
                    FillEmployeeSelectList();
                    FillProjectSelectList();
                    return View(projectTask);
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
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                ProjectTaskRepositoryService.Delete(id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
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