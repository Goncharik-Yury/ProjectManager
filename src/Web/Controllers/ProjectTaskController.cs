using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.ViewModels;
using TrainingTask.ApplicationCore.DBManipulators;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.ApplicationCore.Validators;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingTask.Web.Converters;

namespace TrainingTask.Controllers
{
    public class ProjectTaskController : Controller
    {
        private ILogger Logger;
        readonly EmployeeDBManipulator EmployeeDbManipulator = new EmployeeDBManipulator();
        readonly ProjectDBManipulator ProjectDbManipulator = new ProjectDBManipulator();
        readonly ProjectTaskDBManipulator dbManipulator = new ProjectTaskDBManipulator();
        string[] ProjectTaskStatuses = { "NotStarted", "InProcess", "Completed", "Delayed" };

        public ProjectTaskController(ILogger fileLogger)
        {
            Logger = fileLogger;
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
        }
        public ActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            return View(VMConverter.DTOtoVM(dbManipulator.GetAllProjectTasksList()));
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
                ProjectTaskVM model = VMConverter.DTOtoVM(dbManipulator.GetProjectTasksById(id))[0];
                return View(model);
            }
        }
        private void FillProjectSelectList()
        {
            List<ProjectDTO> ProjectsList = ProjectDbManipulator.GetProjectsList();
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
            List<EmployeeDTO> EmployeesList = EmployeeDbManipulator.GetEmployeesList();
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
        public ActionResult CreateOrEdit(ProjectTaskVM projectTask, bool IsCreateNotEdit = false)
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            //ModelState.ClearValidationState("IsCreateNotEdit"); // A small crutch for saving my nerves without any consequences
            //ModelState.MarkFieldValid("IsCreateNotEdit"); // A small crutch for saving my nerves without any consequences
            //ModelState.ClearValidationState("EmployeeFullName"); // A small crutch for saving my nerves without any consequences
            //ModelState.MarkFieldValid("EmployeeFullName"); // A small crutch for saving my nerves without any consequences
            try
            {
                if (projectTask == null)
                    throw new NullReferenceException();
                ViewBag.ProjectTaskStatuses = ProjectTaskStatuses;
                ProjectTaskValidate(projectTask);
                if (ModelState.IsValid)
                {
                    ProjectTaskDTO projectTaskDTO = VMConverter.VMToDTO(projectTask);
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
                ProjectTaskDBManipulator.DeleteProjectTask(id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return View("Error");
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