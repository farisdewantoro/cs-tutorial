using System;

namespace polymorphism
{
   public class Program{
        static void Main(string[] args)
        {
            // Employee[] employees =new Employee[2];
            // employees[0] = new PartTimeEmployee();
            // employees[1] = new FullTimeEmployee();

            // foreach (var e in employees)
            // {
            //     e.PrintFullName();
            // }
            PartTimeEmployee part = new PartTimeEmployee(){
                gg = "halo",
                FirstName="haha",
                LastName="gg"
            };
         
            part.PrintFullName();
            Console.ReadLine();
        }
    }
   public  class Employee
    {
        public string FirstName = "FN";
        public string LastName = "LN";
        public Employee()
        {
            
        }
        public Employee(string FirstName,string LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
      public virtual void PrintFullName()
      {
          Console.WriteLine(this.FirstName + "" + this.LastName);
      }
    }

    public class PartTimeEmployee : Employee
    {
        public string gg {get;set;}
        public PartTimeEmployee(string FirstName,string LastName,string gg):base(FirstName,LastName)
        {
            this.gg = gg;
        }

        public PartTimeEmployee()
        {
        }

        public override void PrintFullName()
        {
            Console.WriteLine(this.FirstName + "" + this.LastName + " Part");
        }
    }

    // public class FullTimeEmployee :Employee
    // {
    //     public  void PrintFullName()
    //     {
    //         Console.WriteLine(this.FirstName + "" + this.LastName + " Full");
    //     }
    // }

}
