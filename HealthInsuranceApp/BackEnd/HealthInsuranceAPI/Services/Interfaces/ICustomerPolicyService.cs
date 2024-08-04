using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.CustomerPolicy;

namespace HealthInsuranceAPI.Services.Interfaces
{
    public interface ICustomerPolicyService
    {
        IEnumerable<CustomerPolicyDTO> GetCustomerPolicyByCustomerId(Guid customerId);
        CustomerPolicyDTO GetCustomerPolicyById(Guid customerPolicyId);
        void CreateCustomerPolicy(CreateCustomerPolicyDTO customerPolicyDto, Guid userId);

        void UpdateCustomerPolicy(UpdateCustomerPolicyDTO customerPolicyDto);

        void DeleteCustomerPolicy(Guid customerPolicyId);
        void CheckRenewalEligibility(Guid customerPolicyId);
        IEnumerable<CustomerPolicyDTO> GetAllCustomerPolicies();
        Customer GetCustomerByUserId(Guid userId);
        CustomerPolicyDTO GetCustomerPolicyDetails(Guid customerPolicyId);

    }
}
