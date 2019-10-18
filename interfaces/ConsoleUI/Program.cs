using DemoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        //INTERFACE ADALAH SEBUAH CONTRACT DIMANA SETIAP CLASSNYA APABILA MENGGUNAKAN INTERFACE MAKA HANYA BISA MENGAKSES YANG ADA PADA INTERFACENYA
        static void Main(string[] args)
        {
            List<IProductModel> cart = AddSampleData();
            CustomerModel customer = GetCustomer();
            
            foreach (IProductModel prod in cart)
            {
                prod.ShipItem(customer);

                if(prod is IDigitalProductModel digital)
                {
                    Console.WriteLine($"Jadi didalam DigitalProductModel Ada IproductModel Contoh seperti ini TotalDownlaod: {digital.TotalDownloadsLeft}");
                }
                if(prod is DigitalProductModel a)
                {
                    Console.WriteLine($"nyoba: {a.TotalDownloadsLeft}");
                }
            
            }

            Console.ReadLine();
        }

        private static CustomerModel GetCustomer()
        {
            return new CustomerModel
            {
                FirstName = "Tim",
                LastName = "Corey",
                City = "Scranton",
                EmailAddress = "tim@IAmTimCorey.com",
                PhoneNumber = "555-1212"
            };
        }

        private static List<IProductModel> AddSampleData()
        {
            List<IProductModel> output = new List<IProductModel>();

            output.Add(new PhysicalProductModel { Title = "Nerf Football" });
            output.Add(new PhysicalProductModel { Title = "IAmTimCorey T-Shirt" });
            output.Add(new PhysicalProductModel { Title = "Hard Drive" });
            output.Add(new DigitalProductModel { Title = "Digital" });
            output.Add(new CourseProductModel { Title = "ahahahahaha" });
            
            return output;
        }
    }


}
