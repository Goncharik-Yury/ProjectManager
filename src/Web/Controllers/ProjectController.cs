using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.ViewModels;
using TrainingTask.ApplicationCore.Dto;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using ApplicationCore.Repository;
using TrainingTask.Web.Converters;

namespace TrainingTask.Controllers
{
    public class ProjectController : Controller
    {
        private ILogger Logger;
        readonly IRepositoryService<ProjectDto> ProjectRepositoryService;
        readonly IProjectTaskRepositoryService<ProjectTaskDto> ProjectTaskRepositoryService;
        readonly IConvertWeb<ProjectVm, ProjectDto> ProjectConverter;
        readonly IConvertWeb<ProjectTaskVm, ProjectTaskDto> ProjectTaskConverter;

        public ProjectController(ILogger logger, IConvertWeb<ProjectVm, ProjectDto> projectConverter, IConvertWeb<ProjectTaskVm, ProjectTaskDto> projectTaskConverter)
        {
            Logger = logger;
            ProjectConverter = projectConverter;
            ProjectTaskConverter = projectTaskConverter;

            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");

            ProjectRepositoryService = new ProjectRepositoryService();
            ProjectTaskRepositoryService = new ProjectTaskRepositoryService();
        }

        public ActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");

            List<ProjectVm> Projects = ProjectConverter.ConvertAll(ProjectRepositoryService.GetAll());

            return View(Projects);
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
                ProjectVm ProjectVm = ProjectConverter.Convert(ProjectRepositoryService.GetSingle(id));
                List<ProjectTaskVm> ProjectTasksVm = ProjectTaskConverter.ConvertAll(ProjectTaskRepositoryService.GetAllByProjectId(ProjectVm.Id));// Получать нужные таски
                ProjectAllVm model = ComposeProjectVm(ProjectVm, ProjectTasksVm);

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(ProjectAllVm projectAllVm, bool IsCreateNotEdit = false)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                if (projectAllVm.Projects == null)
                    throw new NullReferenceException();
                if (ModelState.IsValid)
                {
                    ProjectDto projectDto = ProjectConverter.Convert(projectAllVm.Projects);
                    if (IsCreateNotEdit)
                    {
                        ProjectRepositoryService.Create(projectDto);
                    }
                    else
                    {
                        ProjectRepositoryService.Update(projectDto);
                    }
                }
                else
                {
                    return View(projectAllVm);
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
                ProjectRepositoryService.Delete(id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        private ProjectAllVm ComposeProjectVm(ProjectVm projectsVm, List<ProjectTaskVm> projectTasksVm)
        {
            ProjectAllVm ProjectAndTask = new ProjectAllVm()
            {
                Projects = projectsVm,
                ProjectTasks = projectTasksVm
            };

            return ProjectAndTask;
        }
    }
}