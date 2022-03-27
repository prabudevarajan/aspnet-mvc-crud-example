using ASP.net_Core_MVC_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.net_Core_MVC_CRUD.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<StudentModel> studentModels;
            studentModels = _context.studentModels.ToList();
            return View(studentModels);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(studentModel);
        }
        public async Task<IActionResult>Edit(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("Index");
            }
            var getstudentdetails = await _context.studentModels.FindAsync(Id);
            return View(getstudentdetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Update(studentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(studentModel);

        }
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }
            var getstudentdetails = await _context.studentModels.FindAsync(Id);
            return View(getstudentdetails);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getstudentdetails = await _context.studentModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (getstudentdetails == null)
            {
                return NotFound();
            }

            return View(getstudentdetails);
        }
        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var getstudentdetails = await _context.studentModels.FindAsync(id);
            _context.studentModels.Remove(getstudentdetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
