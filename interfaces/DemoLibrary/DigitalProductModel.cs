using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    //UNTUK INHERITANCE CLASS HARUS PALING DEPAN, SEDANGKAN INTERFACE BISA DISEBELAHNYA DAN BISA MENGINHERIT BANYAK
    public class DigitalProductModel : IDigitalProductModel
    {
        public string Title { get; set; }

        public bool HasOrderBeenCompleted { get; private set; }
        public int TotalDownloadsLeft { get;private set; } = 5;
        public void ShipItem(CustomerModel customer)
        {
            if(HasOrderBeenCompleted == false)
            {
                Console.WriteLine($"Simulating emaling  to { customer.FirstName } to { customer.EmailAddress }");
                TotalDownloadsLeft -= 1;
                if (TotalDownloadsLeft < 1)
                {
                    HasOrderBeenCompleted = true;
                    TotalDownloadsLeft = 0;
                }
            }

        }
    }
}
