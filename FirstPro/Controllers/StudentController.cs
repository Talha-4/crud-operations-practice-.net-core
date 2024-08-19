using FirstPro.Data;
using FirstPro.Models;
using FirstPro.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace FirstPro.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        public StudentController(ApplicationDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentViewModel studentview)
        {
            var student = new Student
            {
                Name = studentview.Name,
                Email = studentview.Email,
                Phone = studentview.Phone,
                Subscribed = studentview.Subscribed,
            };
            await DbContext.Students.AddAsync(student);
            await DbContext.SaveChangesAsync();
            ModelState.Clear();
            return RedirectToAction("AllStudents");

        }
        [HttpGet]
        public async Task<IActionResult> AllStudents()
        {
            var students = await DbContext.Students.ToListAsync();
            return View(students);
        }
       [ HttpGet ]
        public async Task<IActionResult> EditStudentInfo(Guid id)
        {
            var student = await DbContext.Students.FindAsync(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatedInfo(Student studentview)
        {
            var student = await DbContext.Students.FindAsync(studentview.Id);

            if (student == null)
            {
                return NotFound(); 
            }    
            student.Name = studentview.Name;
            student.Email = studentview.Email;
            student.Phone = studentview.Phone;
            student.Subscribed = studentview.Subscribed;
            DbContext.Students.Update(student);
            await DbContext.SaveChangesAsync();

            return RedirectToAction("AllStudents");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var student = await DbContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            DbContext.Students.Remove(student);
            await DbContext.SaveChangesAsync();

            return RedirectToAction("AllStudents");

        }
    }
}
