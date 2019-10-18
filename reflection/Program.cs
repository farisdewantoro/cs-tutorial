using System;
using System.Linq;
using System.Reflection;

namespace reflection
{
    class Program
    {
        static void Main(string[] args)
        {
        
        // MENGGUNAKAN ATTRIBUTE :

        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes().Where(t=>t.GetCustomAttributes<MyClassAtt>().Count() > 0 );
        foreach (var type in types)
        {
            System.Console.WriteLine(type.Name);

            var methods = type.GetMethods().Where(m=>m.GetCustomAttributes<MyMethodAttribute>().Count()>0);
            foreach (var method in methods)
            {
                System.Console.WriteLine(method.Name);
            }
        }


        //   TANPA ATTRIBUTE :
        
        //    var assembly = Assembly.GetExecutingAssembly();
        //    System.Console.WriteLine(assembly.FullName);

        //    var types = assembly.GetTypes();
        //    foreach(var type in types)
        //    {
        //        System.Console.WriteLine("Type : "+type.Name);
        //        var props = type.GetProperties();
        //        foreach (var prop in props)
        //        {
        //            System.Console.WriteLine("\t Property : " +prop.Name +" Property Type : "+prop.PropertyType);
        //        }
        //        var fields = type.GetFields();
        //        foreach(var field in fields){
        //            System.Console.WriteLine("\tField : "+ field.Name);
        //        }
        //        var methods = type.GetMethods();
        //        foreach (var method in methods)
        //        {
        //             System.Console.WriteLine("\tMethod : " + method.Name);
        //        }
        //    }
        //    var sample = new Sample(){
        //        Name="John",
        //        Age=23,
        //        GG=2
        //    };
        //    var sampleType = typeof(Sample);
        //    var nameProperty = sampleType.GetProperty("Name");
        //    System.Console.WriteLine("Property : " + nameProperty.GetValue(sample));
            
        //    var myMethod = sampleType.GetMethod("MyMethod");
        //    myMethod.Invoke(sample,null);


            Console.ReadLine();
        }

    }

    [MyClassAtt]
    public class Sample
    {
        public string Name { get; set; }
        public int GG { get; set; }
        public int Age;
        [MyMethod]
        public void MyMethod()
        {
            Console.WriteLine("Hello from my method");
        }

        public void NoAttributeMethod(){}
    }
    [AttributeUsage(AttributeTargets.Class)]
    public class MyClassAtt :Attribute{

    }
    [AttributeUsage(AttributeTargets.Method)]
    public class MyMethodAttribute :Attribute{

    }
}
