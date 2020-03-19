using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.ViewModels;
using TrainingTask.ApplicationCore.DBManipulators;
using TrainingTask.ApplicationCore.DTO;
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

            List<ProjectVM> Projects = VMConverter.DTOtoVM(ProjectDbManipulator.GetProjectsList());
            //var ProjectTasks = VMConverter.DTOtoVM(ProjectTaskDbManipulator.GetAllProjectTasksList());

            //var ProjectAndTaskVM = GetProjectVM(Projects, ProjectTasks);

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
                var ProjectVM = VMConverter.DTOtoVM(ProjectDbManipulator.GetProjectById(id));
                var ProjectTasksVM = VMConverter.DTOtoVM(ProjectTaskDbManipulator.GetProjectTasksByProjectId(id));
                ProjectAllVM model = GetProjectVM(ProjectVM, ProjectTasksVM);

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(ProjectVM project, bool IsCreateNotEdit = false)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                if (project == null)
                    throw new NullReferenceException();
                ProjectValidate(project);
                if (ModelState.IsValid)
                {
                    ProjectDTO projectDTO = VMConverter.VMToDTO(project);
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

        private void ProjectValidate(ProjectVM project)
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

        private ProjectAllVM GetProjectVM(List<ProjectVM> projectsVM, List<ProjectTaskVM> projectTasksVM)
        {
            ProjectAllVM ProjectAndTask = new ProjectAllVM()
            {
                Projects = projectsVM,
                ProjectTasks = projectTasksVM
            };

            return ProjectAndTask;
        }

        //private ProjectVMWithRelations GetProjectVMWithRelations(int projectId)
        //{
        //    ProjectVMWithRelations model = new ProjectVMWithRelations {
        //        Project = VMConverter.DTOtoVMList(ProjectDbManipulator.GetProjectById(projectId))[0],
        //        ProjectTasks = new List<ProjectTaskVMWithRelations>().Add(
        //            VMConverter.DTOtoVM(ProjectTaskDbManipulator.GetProjectTasksByProjectId(projectId)))


        //    }
        //    return model; // TODO: change
        //}
    }
}