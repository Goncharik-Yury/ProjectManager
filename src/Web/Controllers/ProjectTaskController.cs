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
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace TrainingTask.Controllers
{
    public class ProjectTaskController : Controller
    {
        private ILogger Logger;
        public ProjectTaskController(ILogger fileLogger)
        {
            Logger = fileLogger;
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
        }
        readonly DBManipulatorProjectTask dbManipulator = new DBManipulatorProjectTask();
        public ActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            return View(ProjectTaskConverter.DTOtoViewModel(dbManipulator.GetAllProjectTasksList()));
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
                ProjectTaskViewModel model = ProjectTaskConverter.DTOtoViewModel(dbManipulator.GetProjectTasksbyId(id))[0];
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(ProjectTaskViewModel projectTask)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            try
            {
                if (projectTask == null)
                    throw new NullReferenceException();
                ProjectTaskValidate(projectTask);
                if (ModelState.IsValid)
                {
                    ProjectTaskDTO projectTaskDTO = ProjectTaskConverter.ViewModelToDTO(projectTask);
                    if (projectTask.IsCreateNotEdit)
                    {
                        DBManipulatorProjectTask.CreateProjectTask(projectTaskDTO);
                    }
                    else
                    {
                        DBManipulatorProjectTask.EditProjectTask(projectTask.Id, projectTaskDTO);
                    }
                }
                else
                {
                    return View(projectTask);
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
                DBManipulatorProjectTask.DeleteProjectTask(id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        private void ProjectTaskValidate(ProjectTaskViewModel projectTask)
        {
            const string TooLongString = "Too long";
            const string InvalidValue = "Invalid value";
            const int MaxLength = 50;

            if (!Validator.NameIsValid(projectTask.Name))
            {
                ModelState.AddModelError("Name", InvalidValue);
            }
            if (!Validator.LengthIsValid(projectTask.Name, MaxLength))
            {
                ModelState.AddModelError("Name", TooLongString);
            }
            if (!Validator.NameIsValid(projectTask.Status))
            {
                ModelState.AddModelError("Status", InvalidValue);
            }
            if (!Validator.LengthIsValid(projectTask.Status, MaxLength))
            {
                ModelState.AddModelError("Status", TooLongString);
            }
            if (!Validator.DateIsValid(projectTask.BeginDate))
            {
                ModelState.AddModelError("BeginDate", InvalidValue);
            }
            if (!Validator.DateIsValid(projectTask.EndDate))
            {
                ModelState.AddModelError("EndDate", InvalidValue);
            }

        }
    }
}