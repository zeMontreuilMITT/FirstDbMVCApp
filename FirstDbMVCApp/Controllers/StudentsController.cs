using FirstDbMVCApp.Data;
using FirstDbMVCApp.Models;
using FirstDbMVCApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace FirstDbMVCApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly FirstDbMVCAppContext _context;

        public StudentsController(FirstDbMVCAppContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (_context.Student == null)
            {
                return NotFound();
            } else
            {
                return View(_context.Student.Include(s => s.Course).ToHashSet());
            }
        }

        public IActionResult Create()
        {
            // in create method, add a dropdown list of all course names
            HashSet<Course> courses = _context.Course.ToHashSet();

            CUStudentVM vm = new CUStudentVM(courses);
            vm.Student = new Student();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(CUStudentVM vm)
        {

            Student student = vm.Student;
            student.CourseId = vm.SelectedCourseId;

            if(ModelState.IsValid)
            {
                _context.Student.Add(student);
                _context.SaveChanges();

                return RedirectToAction("Index");
            } else
            {
                return View(student);
            }

        }

        // UPDATE

        public IActionResult Edit(Guid? id)
        {
            if(id == null || _context.Student == null || _context.Course == null)
            {
                return NotFound();
            }

            Student student = _context.Student.Find(id);

            if(student == null)
            {
                return NotFound();
            }


            CUStudentVM vm = new CUStudentVM(_context.Course.ToHashSet());
            vm.SelectedCourseId = student.CourseId;
            vm.Student = student;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(CUStudentVM vm)
        {
            Student student = vm.Student;
            student.CourseId = vm.SelectedCourseId;

            if (ModelState.IsValid)
            {
                _context.Student.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                return Problem("Error handling Student entity changes.");
            }
        }

    }
}
