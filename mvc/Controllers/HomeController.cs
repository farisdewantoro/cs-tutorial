using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using testing1.Models;
using testing1.ViewModels;

namespace testing1.Controllers
{
    // [Route("Home")] //SEMUA ROUTE MENGGUNAKAN /home
    [Route("[controller]")] // sama kayak atas
    //[Route("[controller][action]")] // bisa juga 
    public class HomeController :Controller
    {
       
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ILogger logger;

        public HomeController(IEmployeeRepository employeeRepository,
        IWebHostEnvironment hostingEnvironment,
        ILogger<HomeController> logger
        )
        {
            
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }
        // public ObjectResult Details()
        // {

        //     Employee model = _employeeRepository.GetEmployee(1);
        //     return new ObjectResult(model); 
        // }
        
        [Route("[action]")] // MEMANGGIL METHODNYA
        [Route("details/{id?}")] //==> ATTRIBUTES UNTUK MEMBUAT ROUTE NAME URL
        public ViewResult Details(int? id) // int? <== BERARTI PARAMETERNYA BISA NULL
        //BISA JUGA PAKE QUERY STRING UNTUK PASS PARAMETER ID
        // public ViewResult DetailEdited(int? id) //=>MASIH JALAN ASAL PAS "RETURN" => return View("~/Views/Home/Details.cshtml")
        {
             logger.LogTrace("trace Log");
             logger.LogWarning("warning log");
         
            //custom view render : jadi urlnya home/details tetapi yang dirender adalah Testing.cshtml
            // return View("Testing");

            //custom view dari folder berbeda
            // return View("/CustomView/testing2.cshtml");

            // custom view dari folder "tanpa extension .cshtml" yang berbeda 1 level mengunakan ..
            // return View("../Test/Update");

            // MENGGUNAKAN DATA

            // Menggunakan ViewData - string keys
            // Kekurangannya kalo typo tidak terdeteksi di pas compile tpi di runtime terlihat 
            Employee model = _employeeRepository.GetEmployee(id??1); // id?? <== KALO IDnya NULL maka ID=1
            ViewData["Employee"] = model;
            ViewData["PageTitle"] = "Employee Details";

            // Menggunakan ViewBag - dynamic propeties
          
            ViewBag.Employee = model;
            ViewBag.PageTitle = "Employee Details";



            //return View(); //bukan strong type view;

            // Menggunakan Strong Type View
            // Complie Time Error Checking 
            // lebih cepat
            // return View(model); //tidak harus namanya model

            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            // MENGGUNAKAN VIEW MODEL
            HomeDetailViewModel homeDetailViewModel = new HomeDetailViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details AHAY"
            };
            return View(homeDetailViewModel);
        }
        
      

        // ATTRIBUTES UNTUK MEMBUAT ROUTE NAME URL
        [Route("")] //tidak menggunakan /home
        [Route("~/")]
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

        [Route("[action]")]
        [HttpGet] //KARENA ADA 2 METHOD CREATE : 
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel(){
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }
        [Route("[action]/{id}")]
        [HttpPost] //KARENA ADA 2 METHOD CREATE :
        public IActionResult Edit(EmployeeEditViewModel model) //KARENA OUTPUTNYA BISA RedirectToAction dan View maka menggunakan Interface IActionResult KARENA TERDAPAT 2 ITU DIDALAM INTERFACE
        {
            if (ModelState.IsValid)
            { //CEK PARAMETER MODEL VALID 
                Employee employee = _employeeRepository.GetEmployee(model.Id); // => ID BERASAL DARI HIDDEN "INPUT FIELD ID"
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if(model.Photo != null)
                {
                    // ERROR : The process cannot access the file 'file path' because it is being used by another process 
                    //https://stackoverflow.com/questions/26741191/ioexception-the-process-cannot-access-the-file-file-path-because-it-is-being

                    // if (model.ExistingPhotoPath != null)
                    // {
                    //     string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                    //     System.IO.File.Delete(filePath);
                    // }


                    string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                    if(System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath  = ProcessUploadedFile(model);
                }

            

                _employeeRepository.Update(employee);
                return RedirectToAction("index");
            }
            return View();

        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFilename = null;
            if (model.Photo != null && model.Photo.Count > 0)
            {
                foreach (IFormFile p in model.Photo)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFilename = Guid.NewGuid().ToString() + "_" + p.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFilename);

                    //karena filestream not properly disposed
                    using(var fileStream = new FileStream(filePath, FileMode.Create)){
                        p.CopyTo(fileStream);
                    }
                 

                }

            }

            return uniqueFilename;
        }

        [Route("[action]")]
        [HttpPost] //KARENA ADA 2 METHOD CREATE :
        public IActionResult Create(EmployeeCreateViewModel model) //KARENA OUTPUTNYA BISA RedirectToAction dan View maka menggunakan Interface IActionResult KARENA TERDAPAT 2 ITU DIDALAM INTERFACE
        {
            if(ModelState.IsValid){ //CEK PARAMETER MODEL VALID 
                string uniqueFilename = ProcessUploadedFile(model);

                Employee newEmployee = new Employee(){
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFilename
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
           
        }
        // public JsonResult Index()
        // {

        //     return Json(new { id = 1, name = "ahay" });
        // }
    }
}
