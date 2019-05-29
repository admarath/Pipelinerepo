using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmosDB.Model;

namespace CosmosDB.Service
{
    public class EmployeeAccess : IEmployeeAccess

    {
        private IDbRepository _dbRepository;
        private string _collectionName;
        public EmployeeAccess()
        {
            var endpoint = "https://sjcosmosdbtest.documents.azure.com:443/";
            var authKey = "9vPyUtbdyEbhvcpq81fScsqoGt2pQ0qB3XauXJh7FdAR3B7qEZNktdKL1cVgEdIosJpZ7BcEndYW7T2tYkPBkA==";
            var databaseid = "CosmosDBDemo";
            _collectionName = "EmployeeAditi";

            _dbRepository = new AzureCosmosDbRepository(endpoint,authKey,databaseid);
        }
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            var emp = await _dbRepository.CreateEntityAsync<Employee>(_collectionName,employee);
            return (emp);

        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var emplist = await _dbRepository.GetAllEntitiesAsync<Employee>(_collectionName);
            return emplist;
        }
    }
}
