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
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrainingTask.Controllers
{
    public class ProjectTaskController : Controller
    {
        private ILogger Logger;
        private readonly string[] ProjectTaskStatus = { "NotStarted", "InProcess", "Completed", "Delayed" };
        public ProjectTaskController(ILogger fileLogger)
        {
            Logger = fileLogger;
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
        }
        readonly ProjectTaskDBManipulator dbManipulator = new ProjectTaskDBManipulator();
        public ActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            return View(ProjectTaskConverter.DTOtoVM(dbManipulator.GetAllProjectTasksList()));
        }

        public ActionResult CreateOrEdit(int id = -1)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            ViewBag.ProjectTaskStatus = new SelectList(ProjectTaskStatus, "Value", "Name");
            if (id < 0)
            {
                ViewBag.IsCreateNotEdit = "true";
                return View();
            }
            else
            {
                ViewBag.IsCreateNotEdit = "false";
                ProjectTaskVM model = ProjectTaskConverter.DTOtoVM(dbManipulator.GetProjectTasksById(id))[0];
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(ProjectTaskVM projectTask, bool IsCreateNotEdit = false)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            //ModelState.MarkFieldValid("IsCreateNotEdit"); // A small crutch for saving my nerves without any consequences
            try
            {
                if (projectTask == null)
                    throw new NullReferenceException();
                ViewBag.ProjectTaskStatus = new SelectList(ProjectTaskStatus, "Value", "Name");
                ProjectTaskValidate(projectTask);
                if (ModelState.IsValid)
                {
                    ProjectTaskDTO projectTaskDTO = ProjectTaskConverter.VMToDTO(projectTask);
                    if (IsCreateNotEdit)
                    {
                        ProjectTaskDBManipulator.CreateProjectTask(projectTaskDTO);
                    }
                    else
                    {
                        ProjectTaskDBManipulator.EditProjectTask(projectTask.Id, projectTaskDTO);
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
                ProjectTaskDBManipulator.DeleteProjectTask(id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        private void ProjectTaskValidate(ProjectTaskVM projectTask)
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