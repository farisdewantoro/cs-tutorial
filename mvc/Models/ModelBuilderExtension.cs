using System;
using Microsoft.EntityFrameworkCore;

namespace testing1.Models
{
    public static class ModelBuilderExt
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // SEED DATA :
            //KALO MAU UPDATE MISALNYA MARK JADI MARRY TINGGAL DIGANTI DISINI NANTI AUTO OVERRIDE

        modelBuilder.Entity<Employee>().HasData(
           new Employee
           {
               Id = 1,
               Name = "Mark",
               Department = Dept.IT,
               Email = "mark@gmail.com"
           },
                 new Employee
                 {
                     Id = 2,
                     Name = "faris",
                     Department = Dept.IT,
                     Email = "faris@gmail.com"
                 }
       );
        }
    }
}
