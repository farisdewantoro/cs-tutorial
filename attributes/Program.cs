using System;
using System.Linq;
using System.Reflection;
using System.Text;
namespace attributes
{
    class Program
    {
        static void Main(string[] args)
        {
            // MENGGUNAKAN REFLECTION
            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.GetCustomAttributes<TestAtt>().Count() > 0
                        select t;

            foreach(var t in types)
            {
                Console.WriteLine(t.Name);
                foreach(var p in t.GetProperties()){
                    Console.WriteLine(p.Name);
                
                }
            }
            Console.ReadLine();
           
        }
    }

    // untuk memasang attributenya bisa digunakan dimana aja
    //contoh karena menggunakan | jadi bisa di class, property, dan method
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
    public class TestAtt : Attribute
    {
        public string Name { get; set; }
        public int Version { get; set; }
    }

    [TestAtt(Name="John",Version = 1)]
    public class Test
    {
        // [TestAtt]
        public int IntValue { get; set; }
        // [TestAtt]
        public void Method(){

        }
    }
    public class NoAttribute{

    }
}
