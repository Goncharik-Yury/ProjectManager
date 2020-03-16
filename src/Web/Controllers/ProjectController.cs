using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        readonly DBProjectManipulator dbManipulator = new DBProjectManipulator();
        public ActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            return View(ProjectConverter.DTOtoViewModel(dbManipulator.GetProjectsList()));
        }

        public ActionResult Create()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel project)
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
                    dbManipulator.CreateProject(projectDTO);
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

        public ActionResult Edit(int id)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            ProjectViewModel model = ProjectConverter.DTOtoViewModel(dbManipulator.GetProjectById(id))[0];
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProjectViewModel project)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                if (project == null) throw new NullReferenceException();
                ProjectValidate(project);
                if (ModelState.IsValid)
                {
                    ProjectDTO projectDTO = ProjectConverter.ViewModelToDTO(project);
                    dbManipulator.EditProject(project.Id, projectDTO);
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
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                dbManipulator.DeleteProject(id);
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
    }
}