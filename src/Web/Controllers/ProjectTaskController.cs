using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.ViewModels;
using TrainingTask.ApplicationCore.Dto;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingTask.Web.Converters;
using ApplicationCore.Repository;

namespace TrainingTask.Controllers
{
    public class ProjectTaskController : Controller
    {
        private ILogger Logger;
        IRepositoryService<ProjectTaskDto> ProjectTaskRepositoryService;
        IRepositoryService<ProjectDto> ProjectRepositoryService;
        IRepositoryService<EmployeeDto> EmployeeRepositoryService;

        IConvertWeb<ProjectTaskVm, ProjectTaskDto> ProjectTaskConverter;

        string[] ProjectTaskStatuses = { "NotStarted", "InProcess", "Completed", "Delayed" };

        public ProjectTaskController(ILogger logger, IConvertWeb<ProjectTaskVm, ProjectTaskDto> projectTaskConverter)
        {
            Logger = logger;
            ProjectTaskConverter = projectTaskConverter;

            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");

            ProjectTaskRepositoryService = new ProjectTaskRepositoryService();
            ProjectRepositoryService = new ProjectRepositoryService();
            EmployeeRepositoryService = new EmployeeRepositoryService();
        }

        public ActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            List<ProjectTaskVm> ProjectTasksVm = ProjectTaskConverter.ConvertAll(ProjectTaskRepositoryService.GetAll());
            return View(ProjectTasksVm);
        }

        public ActionResult CreateOrEdit(int id = -1)
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
                ProjectTaskVm model = ProjectTaskConverter.Convert(ProjectTaskRepositoryService.GetSingle(id));
                return View(model);
            }
        }
        private void FillProjectSelectList()
        {
            List<ProjectDto> ProjectsList = ProjectRepositoryService.GetAll();
            List<ProjectSelectListItem> ProjectSelectList = new List<ProjectSelectListItem>();
            ProjectsList.ForEach(project =>
            {
                ProjectSelectList.Add(new ProjectSelectListItem
                {
                    Id = project.Id,
                    ShortName = project.ShortName
                });
            });
            ViewBag.ProjectSelectList = new SelectList(ProjectSelectList, "Id", "ShortName");
        }
        private void FillEmployeeSelectList()
        {
            List<EmployeeDto> EmployeesList = EmployeeRepositoryService.GetAll();
            List<EmployeeSelectListItem> EmployeeSelectList = new List<EmployeeSelectListItem>();
            EmployeesList.ForEach(employee =>
            {
                EmployeeSelectList.Add(new EmployeeSelectListItem
                {
                    Id = employee.Id,
                    FullName = $"{employee.LastName} {employee.FirstName} {employee.Patronymic}"
                });
            });
            ViewBag.EmployeeSelectList = new SelectList(EmployeeSelectList, "Id", "FullName");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(ProjectTaskVm projectTask, bool IsCreateNotEdit = false)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                if (projectTask == null)
                    throw new NullReferenceException();
                ViewBag.ProjectTaskStatuses = ProjectTaskStatuses;
                if (ModelState.IsValid)
                {
                    ProjectTaskDto projectTaskDto = ProjectTaskConverter.Convert(projectTask);
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
        public ActionResult Delete(int id)
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