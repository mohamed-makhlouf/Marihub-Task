using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Marihub_Task.Models;

namespace Marihub_Task.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly CompanyContext db;

        public DepartmentController(CompanyContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public  IActionResult Index()
        {
            var depts = db.Departments.ToList();
            return View(depts);
        }

        public IActionResult Details(int? id)
        {
            

            var depts =  db.Departments
                .FirstOrDefault(m => m.DeptId == id);
            

            return View(depts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DeptId,DeptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Add(department);
                 db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var department =  db.Departments.Find(id);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("DeptId,DeptName")] Department department)
        {
            

            if (ModelState.IsValid)
            {
                
                    db.Update(department);
                     db.SaveChanges();
                
             
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Delete(int? id)
        {
            

            var department = db.Departments
                .FirstOrDefault(m => m.DeptId == id);
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
