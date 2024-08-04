
using System.Linq;
using HealthInsuranceAPI.Exceptions;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.CustomerPolicy;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;
using Microsoft.EntityFrameworkCore;
namespace HealthInsuranceApp.Repositories
{
    public class CustomerPolicyRepository : ICustomerPolicyRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public CustomerPolicyRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }


        public CustomerPolicy GetCustomerPolicy(Guid customerPolicyId)
        {
            try
            {
                var customerPolicy = _context.CustomerPolicies
                                             .Include(cp => cp.InsurancePolicy)
                                             .FirstOrDefault(cp => cp.CustomerPolicyID == customerPolicyId);

                if (customerPolicy == null)
                {
                    throw new NotFoundException($"Customer Policy with ID {customerPolicyId} not found.");
                }
 
                if (customerPolicy.InsurancePolicy == null)
                {
                    throw new NotFoundException($"Associated insurance policy not found for Customer Policy ID {customerPolicyId}.");
                }

                return customerPolicy;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database update exception: {dbEx.Message}");
                throw new Exception("A database update error occurred while fetching the customer policy.", dbEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unknown error: {ex.Message}");
                throw new Exception("An unknown error occurred while fetching the customer policy.", ex);
            }
        }


        public void AddCustomerPolicy(CustomerPolicy customerPolicy)
        {
            if (_context.CustomerPolicies.Any(cp => cp.CustomerID == customerPolicy.CustomerID && cp.PolicyID == customerPolicy.PolicyID))
            {
                throw new AlreadyExistsException($"Customer Policy for CustomerID {customerPolicy.CustomerID} and PolicyID {customerPolicy.PolicyID} already exists.");
            }
            _context.CustomerPolicies.Add(customerPolicy);
            _context.SaveChanges();
        }

        public void UpdateCustomerPolicy(CustomerPolicy customerPolicy)

        {
            try
            {
                var existingCustomerPolicy = GetCustomerPolicy(customerPolicy.CustomerPolicyID);
                if (existingCustomerPolicy == null)
                {
                    throw new NotFoundException($"Customer Policy with ID {customerPolicy.CustomerPolicyID} not found.");
                }
                _context.CustomerPolicies.Update(customerPolicy);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new UserNotFoundException();
            }
        }

        public void DeleteCustomerPolicy(Guid customerPolicyId)
        {
            var customerPolicy = GetCustomerPolicy(customerPolicyId);
            try
            {
                if (customerPolicy == null)
                {
                    throw new NotFoundException($"Customer Policy with ID {customerPolicyId} not found.");
                }
                _context.CustomerPolicies.Remove(customerPolicy);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }

        }
        public void AddRenewal(Renewal renewal)
        {
            _context.Renewals.Add(renewal);
            _context.SaveChanges();
        }

        public Renewal GetRenewalById(Guid renewalId)
        {
            return _context.Renewals.Find(renewalId);
        }

        public Renewal GetLastRenewal(Guid customerPolicyId)
        {
            return _context.Renewals
                .Where(r => r.CustomerPolicyID == customerPolicyId)
                .OrderByDescending(r => r.RenewalDate)
                .FirstOrDefault();
        }
        public IEnumerable<CustomerPolicy> GetAllCustomerPolicies() 
        {
            return _context.CustomerPolicies.ToList();
        }
        public CustomerPolicy GetCustomerPolicyDetails(Guid customerPolicyId)
        {
            return _context.CustomerPolicies
                           .Include(cp => cp.InsurancePolicy)
                           .Include(cp => cp.Claims)
                           .Include(cp => cp.Payments)
                           .Include(cp => cp.Renewals)
                           .Include(cp => cp.Revivals)
                           .FirstOrDefault(cp => cp.CustomerPolicyID == customerPolicyId);
        }
        public IEnumerable<CustomerPolicy> GetCustomerPoliciesByCustomerId(Guid customerId)
        {
            return _context.CustomerPolicies                
                .Include(cp => cp.InsurancePolicy)
                .Include(cp => cp.Claims)
                .Include(cp => cp.Payments)
                .Include(cp => cp.Renewals)
                .Include(cp => cp.Revivals)
                .Where(cp => cp.CustomerID == customerId)
                .ToList();
        }

        public CustomerPolicyDTO GetCustomerDetails(Guid customerPolicyId)
        {
            throw new NotImplementedException();
        }
    }
}
