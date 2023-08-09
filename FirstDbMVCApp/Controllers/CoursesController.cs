using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstDbMVCApp.Data;
using FirstDbMVCApp.Models;

namespace FirstDbMVCApp.Controllers
{
    public class CoursesController : Controller
    {
        private FirstDbMVCAppContext _db;

        // Dependency Injection will provide a new instance of this class whenever we construct a new controller
        public CoursesController(FirstDbMVCAppContext db)
        {
            _db = db;
        }
        
        // CREATE READ UPDATE DELETE

        public IActionResult Index()
        {
            // index will show all instances of a model (all records on a table)
            HashSet<Course> allCourses = _db.Course
                .Include(c => c.EnrolledStudents).ToHashSet();

            return View(allCourses);
        }
    }
}
