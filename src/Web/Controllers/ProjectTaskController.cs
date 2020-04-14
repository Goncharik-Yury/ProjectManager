using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Web.ViewModels;
using ProjectManager.ApplicationCore.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManager.ApplicationCore.Repository;
using ProjectManager.Common;
using ProjectManager.Web.Common;
using System.Text.RegularExpressions;

namespace ProjectManager.Controllers
{
    public class ProjectTaskController : Controller
    {
        private readonly ILogger logger;
        IProjectTaskService<ProjectTaskDto> ProjectTaskService;
        IService<ProjectDto> ProjectService;
        IService<EmployeeDto> EmployeeService;

        private readonly IConvert<ProjectTaskVm, ProjectTaskDto> ConvertToProjectTaskDto;
        private readonly IConvert<ProjectTaskDto, ProjectTaskVm> ConvertToProjectTaskVm;

        private readonly string[] ProjectTaskStatuses = { "NotStarted", "InProcess", "Completed", "Delayed" };


        public ProjectTaskController(ILogger logger, IProjectTaskService<ProjectTaskDto> projectTaskService, IService<ProjectDto> projectService, IService<EmployeeDto> employeeService, IConvert<ProjectTaskVm, ProjectTaskDto> convertToProjectTaskDto,
            IConvert<ProjectTaskDto, ProjectTaskVm> convertToProjectTaskVm)
        {
            this.logger = logger;

            ProjectTaskService = projectTaskService;
            ProjectService = projectService;
            EmployeeService = employeeService;

            ConvertToProjectTaskDto = convertToProjectTaskDto;
            ConvertToProjectTaskVm = convertToProjectTaskVm;
        }

        [HttpGet]
        public IActionResult Index()
        {
            logger.LogDebug($"ProjectTask.Index is called");
            IList<ProjectTaskVm> ProjectTasksVm = ConvertToProjectTaskVm.Convert(ProjectTaskService.GetAll());
            FillProjectTasksVm(ProjectTasksVm);
            return View(ProjectTasksVm);
        }

        //[HttpGet]
        //public IActionResult CreateWithRedirect(string RedirectAction, string RedirectController, string RedirectId)
        //{

        //}

        [HttpGet]
        public IActionResult Create(string RedirectAction, string RedirectController, string RedirectId)
        {
            //var RequestPath = Request.Path;
            //var RouteDataRoute = RouteData.Values;

            //referer
            //var w = HttpContext.Request.QueryString;
            //var e = HttpContext.Request.RouteValues;
            //var r = HttpContext.Request.PathBase;
            logger.LogDebug($"ProjectTask.Create [get] is called");
            IList<EmployeeDto> employeesDto = EmployeeService.GetAll();
            IList<ProjectDto> projectesDto = ProjectService.GetAll();
            ProjectTaskFilledVm model = ComposeProjectTaskVm(null, GetEmployeeSelectList(employeesDto), GetProjectSelectList(projectesDto), ProjectTaskStatuses);

            //string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            //string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            //var q = RouteData.Values["controller"];

            var Redirection = View("CreateOrEdit", model);
            string referer = HttpContext.Request.Headers["referer"];
            Regex regex = new Regex(@"/Project/");
            MatchCollection matches = regex.Matches(referer);
            if (matches.Count > 0)
            {
                return RedirectToAction("Edit", "Project", (object)"2");
            }
            else
            {
                return View("CreateOrEdit", model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            logger.LogDebug($"ProjectTask.Edit [get] is called");
            IList<EmployeeDto> employeesDto = EmployeeService.GetAll();
            IList<ProjectDto> projectesDto = ProjectService.GetAll();
            ProjectTaskVm ProjectTaskVm = ConvertToProjectTaskVm.Convert(ProjectTaskService.Get(id));
            FillProjectTaskVm(ProjectTaskVm);
            ProjectTaskFilledVm model = ComposeProjectTaskVm(ProjectTaskVm, GetEmployeeSelectList(employeesDto), GetProjectSelectList(projectesDto), ProjectTaskStatuses);
            return View("CreateOrEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProjectTaskFilledVm projectTaskFilledVm)
        {
            logger.LogDebug($"ProjectTask.Create [post] is called");
            try
            {
                if (projectTaskFilledVm == null)
                    throw new ArgumentException();
                if (ModelState.IsValid)
                {
                    ProjectTaskDto projectTaskDto = ConvertToProjectTaskDto.Convert(projectTaskFilledVm.ProjectTasks);
                    ProjectTaskService.Create(projectTaskDto);
                }
                else
                {
                    return View(projectTaskFilledVm);
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
        public IActionResult Edit(ProjectTaskFilledVm projectTaskFilledVm)
        {
            logger.LogDebug($"ProjectTask.Edit [post] is called");
            try
            {
                if (projectTaskFilledVm == null)
                    throw new ArgumentException();
                if (ModelState.IsValid)
                {
                    ProjectTaskDto projectTaskDto = ConvertToProjectTaskDto.Convert(projectTaskFilledVm.ProjectTasks);
                    ProjectTaskService.Update(projectTaskDto);
                }
                else
                {
                    return View(projectTaskFilledVm);
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
            logger.LogDebug($"ProjectTask.Delete is called");
            try
            {
                ProjectTaskService.Delete(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

        private SelectList GetProjectSelectList(IList<ProjectDto> projectesDto)
        {
            List<ProjectSelectListItem> ProjectSelectList = new List<ProjectSelectListItem>();
            foreach (var item in projectesDto)
            {
                ProjectSelectList.Add(new ProjectSelectListItem
                {
                    Id = item.Id,
                    ShortName = item.ShortName
                });
            }

            return new SelectList(ProjectSelectList, "Id", "ShortName");
        }

        private SelectList GetEmployeeSelectList(IList<EmployeeDto> employeesDto)
        {
            List<EmployeeSelectListItem> EmployeeSelectList = new List<EmployeeSelectListItem>();
            foreach (var item in employeesDto)
            {
                EmployeeSelectList.Add(new EmployeeSelectListItem
                {
                    Id = item.Id,
                    FullName = $"{item.LastName} {item.FirstName} {item.Patronymic}"
                });
            }

            return new SelectList(EmployeeSelectList, "Id", "FullName");
        }

        private ProjectTaskFilledVm ComposeProjectTaskVm(ProjectTaskVm projectTasksVm, SelectList employeeSelectList, SelectList projectSelectList, string[] projectTaskStatuses)
        {
            ProjectTaskFilledVm projectTaskFilledVm = new ProjectTaskFilledVm()
            {
                ProjectTasks = projectTasksVm,
                EmployeeSelectList = employeeSelectList,
                ProjectSelectList = projectSelectList,
                ProjectTaskStatuses = projectTaskStatuses,
            };
            return projectTaskFilledVm;
        }

        private void FillProjectTasksVm(IList<ProjectTaskVm> projectTasksVm)
        {
            foreach (var item in projectTasksVm)
            {
                FillProjectTaskVm(item);
            }
        }

        private void FillProjectTaskVm(ProjectTaskVm item)
        {
            var employee = EmployeeService.Get(item.EmployeeId);
            var project = ProjectService.Get(item.ProjectId);
            item.EmployeeFullName = $"{employee.LastName} {employee.FirstName} {employee.Patronymic}";
            item.ProjectShortName = $"{project.ShortName}";
        }
    }
}