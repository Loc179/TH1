using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TH1.Models;
using TH1.Services.Interfaces;

namespace TH1.Controllers
{
    public class StudentController : Controller
    {
        private List<Student> listStudents = new List<Student>();
        readonly IBufferedFileUploadService _bufferedFileUploadService;
        public StudentController()
        {
            listStudents = new List<Student>()
            {
                new Student() { Id = 101, Name="Hải Nam", Branch=Branch.IT,Gender=Gender.Male,IsRegular=true,Address="A1-2018",Email="nam@g.com"},
                new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE, Gender = Gender.Female, IsRegular = true, Address = "A1-2019", Email = "tu@g.com" },
                new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE, Gender = Gender.Male, IsRegular = false, Address = "A1-2020", Email = "phong@g.com" },
                new Student() { Id = 104, Name = "Minh Tú", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "A1-2021", Email = "mai@g.com" }

            };
        }

        [HttpGet("/Admin/Student/List")]
        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet("/Admin/Student/Add")]
        public IActionResult Create()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };
            return View();
        }

        [HttpPost("/Admin/Student/Add")]
        public async Task<IActionResult> Create(Student s, IFormFile avatarfile)
        {
            //
            // Handling Upload avatar file:
            // - Avatar files will store in ~/wwwroot/UploadFiles/
            // 
            if (avatarfile != null)
                try
                {
                    if (await _bufferedFileUploadService.UploadFile(avatarfile))
                    {
                        Debug.WriteLine("File Upload Successful");
                        ViewBag.Message = "File Upload Successful";
                    }
                    else
                    {
                        Debug.WriteLine("File Upload Failed");
                        ViewBag.Message = "File Upload Failed";
                    }
                }
                catch
                {
                    Debug.WriteLine("File Upload Failed");
                    //Log ex
                    ViewBag.Message = "File Upload Failed";
                }

            //
            // Handling store The new student
            //
            s.Id = listStudents.Last<Student>().Id + 1;

            // Handle Avatar Url. 
            if (s.Avatar != null)
                s.Avatar = Path.Combine("UploadedFiles", s.Avatar);

            listStudents.Add(s);
            return View("Index", listStudents);
        }
        /*public IActionResult Create(Student s)
        {
            s.Id=listStudents.Last<Student>().Id + 1;
            listStudents.Add(s);
            return View("Index", listStudents);
        }*/
    }
}
