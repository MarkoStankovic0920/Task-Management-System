using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApplication.Db;
using User_Roles_Application.Models;
using Microsoft.AspNetCore.Authorization;

namespace User_Roles_Application.Controllers
{
    [Authorize(Roles = "Admin,Supervisor")]
    public class SupervisorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SupervisorController(ApplicationDbContext db)
        { _db = db; }

        public IActionResult SupervisorPanel()
        {
            return View();
        }

        public IActionResult TaskList()
        {
            IEnumerable<Tasks> objTasksList = _db.Tasks.ToList();
            if (objTasksList == null)
            {
                return NotFound();
            }
            return View(objTasksList);
        }

        public IActionResult TaskReportList()
        {
            IEnumerable<TaskReport> objTasksReportList = _db.TaskReport.Include(tr => tr.Task).ToList();
            if (objTasksReportList == null)
            {
                return NotFound();
            }
            return View(objTasksReportList);
        }

        //GET
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TaskReport task = new TaskReport();
            task.Task = _db.Tasks.Find(id);

            if (task.Task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskReport obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            obj.Task = _db.Tasks.Find(obj.TaskId);

            if (obj.Task == null)
            {
                return NotFound("Associated Task not found");
            }
            _db.TaskReport.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("SupervisorPanel");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var taskReport = _db.TaskReport.Find(id);
            if (taskReport == null)
            {
                return NotFound();
            }
            return View(taskReport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskReport obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            _db.TaskReport.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("TaskReportList");
        }

        [HttpGet]
        public IActionResult DeleteView(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var taskReport = _db.TaskReport.Find(id);
            if (taskReport == null)
            {
                return NotFound();
            }
            return View(taskReport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            var taskReport = _db.TaskReport.Find(id);
            if (taskReport == null)
            {
                return NotFound();
            }

            _db.TaskReport.Remove(taskReport);
            _db.SaveChanges();

            return RedirectToAction("TaskReportList");
        }
    }
}