using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.ViewModels;
using TrainingTask.ApplicationCore.Dto;
using Microsoft.Extensions.Logging;
using TrainingTask.ApplicationCore.Repository;
using TrainingTask.Common;

namespace TrainingTask.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger logger;
        private readonly IService<ProjectDto> ProjectService;
        private readonly IProjectTaskService<ProjectTaskDto> ProjectTaskService;
        private readonly IConvert<ProjectVm, ProjectDto> ConvertToProjectDto;
        private readonly IConvert<ProjectDto, ProjectVm> ConvertToProjectVm;
        private readonly IConvert<ProjectTaskVm, ProjectTaskDto> ConvertToProjectTaskDto;
        private readonly IConvert<ProjectTaskDto, ProjectTaskVm> ConvertToProjectTaskVm;

        public ProjectController(ILogger logger, IService<ProjectDto> projectService,
            IProjectTaskService<ProjectTaskDto> projectTaskService,
            IConvert<ProjectVm, ProjectDto> convertToProjectDto,
            IConvert<ProjectDto, ProjectVm> convertToProjectVm,
            IConvert<ProjectTaskVm, ProjectTaskDto> convertToProjectTaskDto,
            IConvert<ProjectTaskDto, ProjectTaskVm> convertToProjectTaskVm
            )
        {
            this.logger = logger;

            ProjectService = projectService;
            ProjectTaskService = projectTaskService;

            ConvertToProjectDto = convertToProjectDto;
            ConvertToProjectVm = convertToProjectVm;
            ConvertToProjectTaskDto = convertToProjectTaskDto;
            ConvertToProjectTaskVm = convertToProjectTaskVm;
        }

        [HttpGet]
        public IActionResult Index()
        {
            logger.LogDebug($"Project.Index is called");

            IList<ProjectVm> Projects = ConvertToProjectVm.Convert(ProjectService.GetAll());

            return View(Projects);
        }

        [HttpGet]
        public IActionResult Create()
        {
            logger.LogDebug($"Project.Create [get] is called");
            return View("CreateOrEdit");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            logger.LogDebug($"Project.Edit [get] is called");
            ProjectVm ProjectVm = ConvertToProjectVm.Convert(ProjectService.Get(id));
            IList<ProjectTaskVm> ProjectTasksVm = ConvertToProjectTaskVm.Convert(ProjectTaskService.GetAllByProjectId(ProjectVm.Id));
            ProjectsVm model = ComposeProjectVm(ProjectVm, ProjectTasksVm);
            return View("CreateOrEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProjectsVm ProjectsVm)
        {
            logger.LogDebug($"Project.Create [post] is called");
            try
            {
                if (ProjectsVm.Projects == null)
                    throw new ArgumentException();
                if (ModelState.IsValid)
                {
                    ProjectDto projectDto = ConvertToProjectDto.Convert(ProjectsVm.Projects);
                    ProjectService.Create(projectDto);
                }
                else
                {
                    return View(ProjectsVm);
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
        public IActionResult Edit(ProjectsVm ProjectsVm)
        {
            if (ProjectsVm.Projects == null)
                throw new ArgumentException();
            logger.LogDebug($"Project.Edit [post] is called");
            try
            {
                if (ProjectsVm.Projects == null)
                    throw new NullReferenceException();
                if (ModelState.IsValid)
                {
                    ProjectDto projectDto = ConvertToProjectDto.Convert(ProjectsVm.Projects);
                    ProjectService.Update(projectDto);
                }
                else
                {
                    return View(ProjectsVm);
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
            logger.LogDebug($"Project.Delete [post] is called");
            try
            {
                ProjectService.Delete(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        private ProjectsVm ComposeProjectVm(ProjectVm projectsVm, IList<ProjectTaskVm> projectTasksVm)
        {
            ProjectsVm ProjectAndTask = new ProjectsVm()
            {
                Projects = projectsVm,
                ProjectTasks = projectTasksVm
            };

            return ProjectAndTask;
        }
    }
}