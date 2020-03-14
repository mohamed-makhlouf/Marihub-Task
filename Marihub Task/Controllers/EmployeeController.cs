using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Marihub_Task.Models;
using Microsoft.AspNetCore.Authorization;

namespace Marihub_Task.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly CompanyContext db;

        public EmployeeController(CompanyContext _db)
        {
            db = _db;
        }

        // GET: Employee
        [HttpGet]
        public  IActionResult Index()
        {
            var emps = db.Employees.Include(e => e.Department).ToList();
            return View(emps);
        }

        // GET: Employee/Details/5
        [HttpGet]
        [Authorize]
        public IActionResult Details(int? id)
        {

            var emp = db.Employees
                .Include(e => e.Department)
                .FirstOrDefault(m => m.Id == id);
            return View(emp);
        }

        // GET: Employee/Create
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var depts = db.Departments.ToList();
            ViewBag.depts = new SelectList(depts, "DeptId", "DeptName", 0);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Id,Name,Email,Age,DeptId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["DeptId"] = new SelectList(db.Departments, "DeptId", "DeptId", employee.DeptId);
            return View(employee);
        }

        // GET: Employee/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {
            var employee = db.Employees.Find(id);
            var depts = db.Departments.ToList();
            ViewBag.depts = new SelectList(depts, "DeptId", "DeptName", 0);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Age,DeptId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    db.Update(employee);
                    await db.SaveChangesAsync();
                
             
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(db.Departments, "DeptId", "DeptId", employee.DeptId);
            return View(employee);
        }

        //Employee/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await db.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
