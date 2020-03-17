using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.Models;
using TrainingTask.ApplicationCore.DBManipulators;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Web.Functional;
using TrainingTask.ApplicationCore.Validators;
using TrainingTask.Web.Converters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace TrainingTask.Controllers
{
    public class ProjectController : Controller
    {
        private ILogger Logger;
        public ProjectController(ILogger fileLogger)
        {
            Logger = fileLogger;
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
        }
        readonly ProjectDBManipulator ProjectsDbManipulator = new ProjectDBManipulator();
        readonly ProjectTaskDBManipulator ProjectTaskDbManipulator = new ProjectTaskDBManipulator();
        readonly EmployeeDBManipulator EmployeeDbManipulator = new EmployeeDBManipulator();
        public ActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");


            var ProjectAndTaskViewModel = GetAllProjectsAndTasksViewModels();

            return View(ProjectAndTaskViewModel);
        }

        public ActionResult CreateOrEdit(int id = -1)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            if (id < 0)
            {
                ViewBag.IsCreateNotEdit = true;
                return View();
            }
            else
            {
                ViewBag.IsCreateNotEdit = false;
                var ProjectViewModel = ProjectConverter.DTOtoViewModelList(GetProjectById(id));
                var ProjectTasksViewModel = ProjectTaskConverter.DTOtoViewModel(GetProjectTasksById(id));
                ProjectAndTaskViewModel model = BuildProjectAndTaskViewModel(ProjectViewModel, ProjectTasksViewModel);
                    
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(ProjectViewModel project)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                if (project == null)
                    throw new NullReferenceException();
                ProjectValidate(project);
                if (ModelState.IsValid)
                {
                    ProjectDTO projectDTO = ProjectConverter.ViewModelToDTO(project);
                    if (project.IsCreateNotEdit)
                    {
                        ProjectsDbManipulator.CreateProject(projectDTO);
                    }
                    else
                    {
                        ProjectsDbManipulator.EditProject(project.Id, projectDTO);
                    }
                }
                else
                {
                    return View(project);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
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
                ProjectsDbManipulator.DeleteProject(id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        private void ProjectValidate(ProjectViewModel project)
        {
            const string TooLongString = "Too long";
            const string InvalidValue = "Invalid value";
            const int MaxLength = 50;

            if (!Validator.NameIsValid(project.Name))
            {
                ModelState.AddModelError("Name", InvalidValue);
            }
            if (!Validator.LengthIsValid(project.Name, MaxLength))
            {
                ModelState.AddModelError("Name", TooLongString);
            }
            if (!Validator.NameIsValid(project.ShortName))
            {
                ModelState.AddModelError("ShortName", InvalidValue);
            }
        }

        private List<ProjectDTO> GetProjectById(int projectId)
        {
            return ProjectsDbManipulator.GetProjectById(projectId);
        }

        private List<ProjectTaskDTO> GetProjectTasksById(int projectId)
        {
            return ProjectTaskDbManipulator.GetProjectTasksByProjectId(projectId);
        }

        private ProjectAndTaskViewModel GetAllProjectsAndTasksViewModels()
        {
            var Projects = ProjectConverter.DTOtoViewModelList(ProjectsDbManipulator.GetProjectsList());
            var ProjectTasks = ProjectTaskConverter.DTOtoViewModel(ProjectTaskDbManipulator.GetAllProjectTasksList());

            return BuildProjectAndTaskViewModel(Projects, ProjectTasks);
        }

        private ProjectAndTaskViewModel GetByProjectIdProjectAndTasksViewModel(int projectId)
        {
            var Projects = ProjectConverter.DTOtoViewModelList(ProjectsDbManipulator.GetProjectById(projectId));
            var ProjectTasks = ProjectTaskConverter.DTOtoViewModel(ProjectTaskDbManipulator.GetProjectTasksByProjectId(projectId));

            return BuildProjectAndTaskViewModel(Projects, ProjectTasks);
        }

        private ProjectAndTaskViewModel BuildProjectAndTaskViewModel(List<ProjectViewModel> ProjectsViewModel, List<ProjectTaskViewModel> ProjectTaskssViewModel)
        {
            ProjectAndTaskViewModel ProjectAndTask = new ProjectAndTaskViewModel()
            {
                Projects = ProjectsViewModel,
                ProjectTasks = ProjectTaskssViewModel
            };

            return ProjectAndTask;
        }
    }
}