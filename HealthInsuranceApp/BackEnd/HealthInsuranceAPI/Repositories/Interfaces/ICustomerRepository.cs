using HealthInsuranceAPI.Models;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        Customer GetCustomerById(Guid customerId);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Guid customerId);
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerByUserId(Guid userId);

    }
}
