using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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

            // MENGGUNAKAN VIEW MODEL
            HomeDetailViewModel homeDetailViewModel = new HomeDetailViewModel()
            {
                Employee = _employeeRepository.GetEmployee(1),
                PageTitle = "Employee Details AHAY"
            };
            return View(homeDetailViewModel);
        }

        // ATTRIBUTES UNTUK MEMBUAT ROUTE NAME URL
        [Route("")] //tidak menggunakan /home
        [Route("~/")]
        [Route("[action]")]
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

        [Route("[action]")]
        [HttpPost] //KARENA ADA 2 METHOD CREATE :
        public IActionResult Create(Employee employee) //KARENA OUTPUTNYA BISA RedirectToAction dan View maka menggunakan Interface IActionResult KARENA TERDAPAT 2 ITU DIDALAM INTERFACE
        {
            if(ModelState.IsValid){ //CEK PARAMETER MODEL VALID 
                Employee newEmployee = _employeeRepository.Add(employee);
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
