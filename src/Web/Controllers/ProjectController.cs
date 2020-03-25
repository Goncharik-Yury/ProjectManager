using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.ViewModels;
using TrainingTask.ApplicationCore.Dto;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TrainingTask.ApplicationCore.Repository;
using TrainingTask.Common;

namespace TrainingTask.Controllers
{
    public class ProjectController : Controller
    {
        private ILogger Logger;
        readonly IRepositoryService<ProjectDto> ProjectRepositoryService;
        readonly IProjectTaskRepositoryService<ProjectTaskDto> ProjectTaskRepositoryService;
        readonly IConvert<ProjectVm, ProjectDto> ConvertToProjectDto;
        readonly IConvert<ProjectDto, ProjectVm> ConvertToProjectVm;
        readonly IConvert<ProjectTaskVm, ProjectTaskDto> ConvertToProjectTaskDto;
        readonly IConvert<ProjectTaskDto, ProjectTaskVm> ConvertToProjectTaskVm;

        public ProjectController(ILogger logger, IRepositoryService<ProjectDto> projectRepositoryService,
            IProjectTaskRepositoryService<ProjectTaskDto> projectTaskRepositoryService,
            IConvert<ProjectVm, ProjectDto> convertToProjectDto,
            IConvert<ProjectDto, ProjectVm> convertToProjectVm,
            IConvert<ProjectTaskVm, ProjectTaskDto> convertToProjectTaskDto,
            IConvert<ProjectTaskDto, ProjectTaskVm> convertToProjectTaskVm
            )
        {
            Logger = logger;

            ProjectRepositoryService = projectRepositoryService;
            ProjectTaskRepositoryService = projectTaskRepositoryService;

            ConvertToProjectDto = convertToProjectDto;
            ConvertToProjectVm = convertToProjectVm;
            ConvertToProjectTaskDto = convertToProjectTaskDto;
            ConvertToProjectTaskVm = convertToProjectTaskVm;
        }

        public IActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");

            IList<ProjectVm> Projects = ConvertToProjectVm.ConvertAll(ProjectRepositoryService.GetAll());

            return View(Projects);
        }

        public IActionResult CreateOrEdit(int id = -1)
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
                ProjectVm ProjectVm = ConvertToProjectVm.Convert(ProjectRepositoryService.GetSingle(id));
                IList<ProjectTaskVm> ProjectTasksVm = ConvertToProjectTaskVm.ConvertAll(ProjectTaskRepositoryService.GetAllByProjectId(ProjectVm.Id));
                ProjectAllVm model = ComposeProjectVm(ProjectVm, ProjectTasksVm);

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(ProjectAllVm projectAllVm, bool IsCreateNotEdit = false)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                if (projectAllVm.Projects == null)
                    throw new NullReferenceException();
                if (ModelState.IsValid)
                {
                    ProjectDto projectDto = ConvertToProjectDto.Convert(projectAllVm.Projects);
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
        public IActionResult Delete(int id)
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

        private ProjectAllVm ComposeProjectVm(ProjectVm projectsVm, IList<ProjectTaskVm> projectTasksVm)
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