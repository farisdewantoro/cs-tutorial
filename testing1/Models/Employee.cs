using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace testing1.Models
{

    public class Employee
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public string Email {get;set;}
        public Dept Department{get;set;}
    }
}