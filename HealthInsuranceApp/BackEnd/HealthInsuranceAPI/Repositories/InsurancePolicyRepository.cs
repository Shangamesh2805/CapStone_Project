
using System.Linq;
using HealthInsuranceAPI.Exceptions;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;

namespace HealthInsuranceApp.Repositories
{
    public class InsurancePolicyRepository : IInsurancePolicyRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public InsurancePolicyRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public InsurancePolicy GetPolicyById(Guid policyId)
        {
            var policy = _context.InsurancePolicies.Find(policyId);
            if (policy == null)
            {
                throw new NotFoundException($"Policy with ID {policyId} not found.");
            }
            return policy;
        }

        public void AddPolicy(InsurancePolicy policy)
        {
            if (_context.InsurancePolicies.Any(p => p.PolicyNumber == policy.PolicyNumber))
            {
                throw new AlreadyExistsException($"Policy with number {policy.PolicyNumber} already exists.");
            }
            _context.InsurancePolicies.Add(policy);
            _context.SaveChanges();
        }

        public void UpdatePolicy(InsurancePolicy policy)

        {
            try
            {
                var existingPolicy = GetPolicyById(policy.PolicyID);
                if (existingPolicy == null)
                {
                    throw new NotFoundException($"Policy with ID {policy.PolicyID} not found.");
                }
                _context.InsurancePolicies.Update(policy);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException();
            }
        }

        public void DeletePolicy(Guid policyId)
        {
            try
            {
                var policy = GetPolicyById(policyId);
                if (policy == null)
                {
                    throw new NotFoundException($"Policy with ID {policyId} not found.");
                }
                _context.InsurancePolicies.Remove(policy);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException();
            }
        }


        public IEnumerable<InsurancePolicy> GetAllPolicies()
        {
            return _context.InsurancePolicies.ToList();
        }
    }
}
