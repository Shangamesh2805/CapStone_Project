using HealthInsuranceAPI.Models;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface IInsurancePolicyRepository
    {
        InsurancePolicy GetPolicy(int policyId);
        void AddPolicy(InsurancePolicy policy);
        void UpdatePolicy(InsurancePolicy policy);
        void DeletePolicy(int policyId);
    }
}
