using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using University_System.Models;

namespace University_Management_System.Controllers
{
    public class StudentController : Controller
    {
        private UniversitySystemContext DBContext;


        public StudentController()
        {
            DBContext = new UniversitySystemContext();
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Add()
        {
            List<Department> Departments = DBContext.Departments.ToList();
            return View(Departments);
        }


        public IActionResult SaveAdd(Student std)
        {

            DBContext.Students.Add(std);
            DBContext.SaveChanges();

            HttpContext.Session.SetInt32("stdId", std.Sid);
            HttpContext.Session.SetInt32("DeptNo", std.Dno);

            return RedirectToAction("ShowDepartmentCourses");
        }

        public IActionResult DeleteStudent(int sid)
        {
            // Deleting related courses redrence this student
            foreach (var crs in DBContext.StudentCourses)
            {
                if (crs.Sid == sid)
                {
                    DBContext.StudentCourses.Remove(crs);
                    DBContext.SaveChanges();
                }
            }

            var student = DBContext.Students.FirstOrDefault(s => s.Sid == sid);

            if (student != null)
            {
                DBContext.Students.Remove(student);
                DBContext.SaveChanges();

            }


            return RedirectToAction("ShowStudents");
        }

        public IActionResult ShowDepartmentCourses()
        {

            var deptNo = HttpContext.Session.GetInt32("DeptNo");

            var courses = DBContext.DepartmentCourses
                    .Where(dc => dc.Dno == deptNo)
                    .Include(dc => dc.CnoNavigation)
                    .Select(dc => dc.CnoNavigation)
                    .ToList();

            return View(courses);
        }


        public IActionResult AddCourse(int CNO)
        {

            var stdid = HttpContext.Session.GetInt32("stdId");


            var existingRelation = DBContext.StudentCourses.FirstOrDefault(sc => sc.Sid == stdid && sc.Cno == CNO);

            if (existingRelation == null)
            {
                var student = DBContext.Students.FirstOrDefault(s => s.Sid == stdid);
                var course = DBContext.Courses.FirstOrDefault(c => c.Cno == CNO);

                if (student != null && course != null)
                {
                    StudentCourse stdCrs = new StudentCourse
                    {
                        Cno = CNO,
                        Sid = (int)stdid,
                        Grade = null,
                        SidNavigation = student,
                        CnoNavigation = course
                    }; 


                    DBContext.StudentCourses.Add(stdCrs);
                    var entriesWritten = DBContext.SaveChanges();

                    if (entriesWritten == 0)
                    {
                        int i = 0;
                    }
                }
            }
        
            return RedirectToAction("ShowDepartmentCourses");

        }



        //public IActionResult DeleteCourse(int CNO)
        //{
        //    var sid = HttpContext.Session.GetInt32("stdId");

        //    foreach (StudentCourse stdCrs in DBContext.StudentCourses)
        //    {
        //        if (stdCrs.Sid == sid && stdCrs.Cno == CNO)
        //        {
        //            DBContext.StudentCourses.Remove(stdCrs);
        //            DBContext.SaveChanges();
        //        }
        //    }

        //    return RedirectToAction("ShowDepartmentCourses");
        //}

        public IActionResult ShowStudents()
        {
            List<Student> students = DBContext.Students.ToList();
            return View(students);
        }


    

        public IActionResult EditStudent(int sid)
        {

            Student student = DBContext.Students.FirstOrDefault(s => s.Sid == sid);

            if (student == null)
            {

                int i = 0;
            }

            return View(student);
        }

        public IActionResult UpdateStudent(int Sid , Student std)
        {

            Student student = DBContext.Students.SingleOrDefault(s => s.Sid == Sid);

            if(student == null)
                return NotFound();

            student.Fname = std.Fname;
            student.Lname = std.Lname;
            student.Address= std.Address;
            student.Email = std.Email;
            student.Age = std.Age;
            student.PhoneNumebr = std.PhoneNumebr;

            DBContext.SaveChanges();          

            return RedirectToAction("ShowStudents");
        }


    }


}

