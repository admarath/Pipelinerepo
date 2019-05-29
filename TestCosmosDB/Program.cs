
using CosmosDB.Model;
using CosmosDB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCosmosDB
{
    class Program
    {
        public static void Main(string[] args)
        {
            var employee = new Employee
            {
                Id = "505684",
                Name = "Subasri",
                City = "Chennai"

            };
            var employeeService = new EmployeeAccess();
            var emp = employeeService.CreateEmployeeAsync(employee).Result;
            var emplist = employeeService.GetAllEmployeesAsync().Result;
            foreach(var ee in emplist)
            {
                Console.WriteLine("Employee ID - " + ee.Id);
                Console.WriteLine("employee Name - "+ ee.Name);
                Console.WriteLine("City - "+ ee.City);
                Console.WriteLine("**********************************************");
            }
            Console.WriteLine("Finished");
            Console.ReadLine();

            
        }
    }
}
