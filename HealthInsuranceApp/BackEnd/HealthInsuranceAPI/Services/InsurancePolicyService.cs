using HealthInsuranceAPI.Models.DTOs.Policy;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceAPI.Services.Interfaces;

namespace HealthInsuranceAPI.Services
{
    public class InsurancePolicyService : IInsurancePolicy
    {
        private readonly IInsurancePolicyRepository _policyRepository;

        public InsurancePolicyService(IInsurancePolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public void AddPolicy(CreatePolicyDTO policyDto)
        {
            var policy = new InsurancePolicy
            {
                PolicyID = Guid.NewGuid(),
                PolicyName = policyDto.PolicyName,
                PolicyNumber = policyDto.PolicyNumber,
                PolicyType = policyDto.PolicyType,
                CoverageAmount = policyDto.CoverageAmount,
                PremiumAmount = policyDto.PremiumAmount,
                RenewalAmount = policyDto.RenewalAmount,
                StartDate = policyDto.StartDate,
                EndDate = policyDto.EndDate
            };

            _policyRepository.AddPolicy(policy);
        }

        public PolicyDTO GetPolicyById(Guid policyId)
        {
            var policy = _policyRepository.GetPolicyById(policyId);
            if (policy == null) throw new Exception("Policy not found.");

            return new PolicyDTO
            {
                PolicyID = policy.PolicyID,
                PolicyName = policy.PolicyName,
                PolicyNumber = policy.PolicyNumber,
                PolicyType = policy.PolicyType,
                CoverageAmount = policy.CoverageAmount,
                PremiumAmount = policy.PremiumAmount,
                RenewalAmount = policy.RenewalAmount,
                StartDate = policy.StartDate,
                EndDate = policy.EndDate
            };
        }

        public void UpdatePolicy(UpdatePolicyDTO policyDto)
        {
            var policy = _policyRepository.GetPolicyById(policyDto.PolicyID);
            if (policy == null) throw new Exception("Policy not found.");

            policy.PolicyName = policyDto.PolicyName;
            policy.PolicyNumber = policyDto.PolicyNumber;
            policy.PolicyType = policyDto.PolicyType;
            policy.CoverageAmount = policyDto.CoverageAmount;
            policy.PremiumAmount = policyDto.PremiumAmount;
            policy.RenewalAmount = policyDto.RenewalAmount;
            policy.StartDate = policyDto.StartDate;
            policy.EndDate = policyDto.EndDate;

            _policyRepository.UpdatePolicy(policy);
        }

        public void DeletePolicy(Guid policyId)
        {
            var policy = _policyRepository.GetPolicyById(policyId);
            if (policy == null) throw new Exception("Policy not found.");

            _policyRepository.DeletePolicy(policyId);
        }

        public IEnumerable<PolicyDTO> GetAllPolicies()
        {
            var policies = _policyRepository.GetAllPolicies();
            return policies.Select(policy => new PolicyDTO
            {
                PolicyID = policy.PolicyID,
                PolicyName = policy.PolicyName,
                PolicyNumber = policy.PolicyNumber,
                PolicyType = policy.PolicyType,
                CoverageAmount = policy.CoverageAmount,
                PremiumAmount = policy.PremiumAmount,
                RenewalAmount = policy.RenewalAmount,
                StartDate = policy.StartDate,
                EndDate = policy.EndDate
            }).ToList();
        }
    }
}
