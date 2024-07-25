using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;
using HealthInsuranceApp.Models;
using System.Linq;

namespace HealthInsuranceApp.Repositories
{
    public class InsurancePolicyRepository : IInsurancePolicyRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public InsurancePolicyRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public InsurancePolicy GetPolicy(int policyId)
        {
            return _context.InsurancePolicies.Find(policyId);
        }

        public void AddPolicy(InsurancePolicy policy)
        {
            _context.InsurancePolicies.Add(policy);
            _context.SaveChanges();
        }

        public void UpdatePolicy(InsurancePolicy policy)
        {
            _context.InsurancePolicies.Update(policy);
            _context.SaveChanges();
        }

        public void DeletePolicy(int policyId)
        {
            var policy = GetPolicy(policyId);
            if (policy != null)
            {
                _context.InsurancePolicies.Remove(policy);
                _context.SaveChanges();
            }
        }
    }
}
