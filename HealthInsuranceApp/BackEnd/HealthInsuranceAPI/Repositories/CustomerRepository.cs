using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;
using HealthInsuranceApp.Models;
using System.Linq;

namespace HealthInsuranceApp.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public CustomerRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public Customer GetCustomer(int customerId)
        {
            return _context.Customers.Find(customerId);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = GetCustomer(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}
