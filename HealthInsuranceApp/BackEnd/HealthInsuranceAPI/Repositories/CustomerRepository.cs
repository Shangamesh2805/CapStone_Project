
using System.Linq;
using HealthInsuranceAPI.Exceptions;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;

namespace HealthInsuranceApp.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public CustomerRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public Customer GetCustomerById(Guid customerId)
        {
            var customer = _context.Customers.Find(customerId);
            if (customer == null)
            {
                throw new NotFoundException($"Customer with ID {customerId} not found.");
            }
            return customer;
        }

        public void AddCustomer(Customer customer)
        {
            if (_context.Customers.Any(c => c.UserID == customer.UserID))
            {
                throw new AlreadyExistsException($"Customer with UserID {customer.UserID} already exists.");
            }
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = GetCustomerById(customer.CustomerID);
            if (existingCustomer == null)
            {
                throw new NotFoundException($"Customer with ID {customer.CustomerID} not found.");
            }
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Guid customerId)
        {
            var customer = GetCustomerById(customerId);
            if (customer == null)
            {
                throw new NotFoundException($"Customer with ID {customerId} not found.");
            }
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
        public Customer GetCustomerByUserId(Guid userId)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.UserID == userId);
            if (customer == null)
            {
                throw new NotFoundException($"Customer with UserID {userId} not found.");
            }
            return customer;
        }

      

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }
    }
}
