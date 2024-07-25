using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;
using HealthInsuranceApp.Models;
using System.Linq;

namespace HealthInsuranceApp.Repositories
{
    public class CustomerPolicyRepository : ICustomerPolicyRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public CustomerPolicyRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public CustomerPolicy GetCustomerPolicy(int customerPolicyId)
        {
            return _context.CustomerPolicies.Find(customerPolicyId);
        }

        public void AddCustomerPolicy(CustomerPolicy customerPolicy)
        {
            _context.CustomerPolicies.Add(customerPolicy);
            _context.SaveChanges();
        }

        public void UpdateCustomerPolicy(CustomerPolicy customerPolicy)
        {
            _context.CustomerPolicies.Update(customerPolicy);
            _context.SaveChanges();
        }

        public void DeleteCustomerPolicy(int customerPolicyId)
        {
            var customerPolicy = GetCustomerPolicy(customerPolicyId);
            if (customerPolicy != null)
            {
                _context.CustomerPolicies.Remove(customerPolicy);
                _context.SaveChanges();
            }
        }
    }
}
