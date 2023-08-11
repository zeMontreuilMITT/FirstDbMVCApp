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

            CreateStudentVM vm = new CreateStudentVM(courses);
            vm.NewStudent = new Student();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(CreateStudentVM vm)
        {

            Student student = vm.NewStudent;
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
    }
}
