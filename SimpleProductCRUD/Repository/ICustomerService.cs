using SimpleProductCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SimpleProductCRUD.Repository
{
    public interface ICustomerService
    {
        Task<IList<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(Guid? id);
        Task InsertCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(Guid? id);
    }
}