using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.Customers;

namespace HealthInsuranceAPI.Services.Interfaces
{
    public interface ICustomerServices
    {
        void AddCustomer(CreateCustomerDTO customerDto);
        CustomerDTO GetCustomerById(Guid customerId);
        void UpdateCustomer(UpdateCustomerDTO customerDto);
        void DeleteCustomer(Guid customerId);
        IEnumerable<CustomerDTO> GetAllCustomers();
    }
}
