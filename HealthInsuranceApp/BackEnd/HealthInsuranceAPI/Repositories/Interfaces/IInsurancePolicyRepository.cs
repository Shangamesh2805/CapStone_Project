using HealthInsuranceAPI.Models;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface IInsurancePolicyRepository
    {
        void AddPolicy(InsurancePolicy policy);
        InsurancePolicy GetPolicyById(Guid policyId);
        void UpdatePolicy(InsurancePolicy policy);
        void DeletePolicy(Guid policyId);
        IEnumerable<InsurancePolicy> GetAllPolicies();
    }
}
