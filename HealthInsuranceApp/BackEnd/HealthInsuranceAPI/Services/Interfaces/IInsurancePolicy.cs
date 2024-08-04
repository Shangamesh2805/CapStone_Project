using HealthInsuranceAPI.Models.DTOs.Policy;

namespace HealthInsuranceAPI.Services.Interfaces
{
    public interface IInsurancePolicy
    {
        void AddPolicy(CreatePolicyDTO policyDto);
        PolicyDTO GetPolicyById(Guid policyId);
        void UpdatePolicy(UpdatePolicyDTO policyDto);
        void DeletePolicy(Guid policyId);
        IEnumerable<PolicyDTO> GetAllPolicies();
    }
}
