using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.ViewModels;
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
        //readonly DBManipulatorFacade DbManipulator = new DBManipulatorFacade();
        readonly ProjectDBManipulator ProjectDbManipulator = new ProjectDBManipulator();
        readonly ProjectTaskDBManipulator ProjectTaskDbManipulator = new ProjectTaskDBManipulator();
        readonly EmployeeDBManipulator EmployeeDbManipulator = new EmployeeDBManipulator();

        public ActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");

            List<ProjectViewModel> Projects = ProjectConverter.DTOtoViewModelList(ProjectDbManipulator.GetProjectsList());
            //var ProjectTasks = ProjectTaskConverter.DTOtoViewModel(ProjectTaskDbManipulator.GetAllProjectTasksList());

            //var ProjectAndTaskViewModel = GetProjectViewModel(Projects, ProjectTasks);

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
                var ProjectViewModel = ProjectConverter.DTOtoViewModelList(ProjectDbManipulator.GetProjectById(id));
                var ProjectTasksViewModel = ProjectTaskConverter.DTOtoViewModel(ProjectTaskDbManipulator.GetProjectTasksByProjectId(id));
                ProjectAndTaskViewModel model = GetProjectViewModel(ProjectViewModel, ProjectTasksViewModel);

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(ProjectViewModel project, bool IsCreateNotEdit = false)
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
                    if (IsCreateNotEdit)
                    {
                        ProjectDbManipulator.CreateProject(projectDTO);
                    }
                    else
                    {
                        ProjectDbManipulator.EditProject(project.Id, projectDTO);
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
                ProjectDbManipulator.DeleteProject(id);
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

        private ProjectAndTaskViewModel GetProjectViewModel(List<ProjectViewModel> ProjectsViewModel, List<ProjectTaskViewModel> ProjectTasksViewModel)
        {
            ProjectAndTaskViewModel ProjectAndTask = new ProjectAndTaskViewModel()
            {
                Projects = ProjectsViewModel,
                ProjectTasks = ProjectTasksViewModel
            };

            return ProjectAndTask;
        }

        //private ProjectViewModelWithRelations GetProjectViewModelWithRelations(int projectId)
        //{
        //    ProjectViewModelWithRelations model = new ProjectViewModelWithRelations {
        //        Project = ProjectConverter.DTOtoViewModelList(ProjectDbManipulator.GetProjectById(projectId))[0],
        //        ProjectTasks = new List<ProjectTaskViewModelWithRelations>().Add(
        //            ProjectTaskConverter.DTOtoViewModel(ProjectTaskDbManipulator.GetProjectTasksByProjectId(projectId)))


        //    }
        //    return model; // TODO: change
        //}
    }
}