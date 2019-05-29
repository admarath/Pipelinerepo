using CosmosDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDB.Service
{
    interface IEmployeeAccess
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    }
}
