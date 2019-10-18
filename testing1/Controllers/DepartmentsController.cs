using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testing1.Models;
using testing1.ViewModels;

namespace testing1.Controllers
{
    public class DepartmentsController : Controller
    {
        public string List(){
            return "List of departments";
        }
        public string Detail()
        {
            return "Detail of departments";
        }
    }
}
