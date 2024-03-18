using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using MyApplication.Db;
using System.Reflection.Metadata;
using User_Roles_Application.Models;

namespace User_Roles_Application.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TasksController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles = "Member, Admin, Supervisor")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TaskList()
        {
            IEnumerable<Tasks> objTasksList = _db.Tasks.ToList();
            if (objTasksList == null)
            {
                return View();
            }
            return View(objTasksList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tasks obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tasks.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("TaskList");
            }
            return View("TaskList");
        }

        [HttpGet]
        public IActionResult EditView(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var taskFromDb = _db.Tasks.Find(id);
            if (taskFromDb == null)
            {
                return NotFound();
            }
            return View(taskFromDb);
        }

        //Patch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tasks model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditView");
            }
            _db.Tasks.Update(model);
            _db.SaveChanges();
            return RedirectToAction("TaskList", "Tasks");
        }

        public IActionResult DeleteView(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var taskFromDb = _db.Tasks.Find(id);
            if (taskFromDb == null)
            {
                return NotFound();
            }
            return View(taskFromDb);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            var taskFromDb = _db.Tasks.Find(id);
            if (taskFromDb == null)
            {
                return NotFound();
            }
            _db.Tasks.Remove(taskFromDb);
            _db.SaveChanges();
            return RedirectToAction("TaskList", "Admin");
        }
    }
}