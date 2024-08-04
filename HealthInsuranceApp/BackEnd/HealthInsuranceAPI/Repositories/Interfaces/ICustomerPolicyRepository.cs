using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.CustomerPolicy;
using System;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface ICustomerPolicyRepository
    {
        CustomerPolicy GetCustomerPolicy(Guid customerPolicyId);
        void AddCustomerPolicy(CustomerPolicy customerPolicy);
        void UpdateCustomerPolicy(CustomerPolicy customerPolicy);
        void DeleteCustomerPolicy(Guid customerPolicyId);
        void AddRenewal(Renewal renewal);
        Renewal GetRenewalById(Guid renewalId);
        Renewal GetLastRenewal(Guid customerPolicyId);
        IEnumerable<CustomerPolicy> GetAllCustomerPolicies();
        CustomerPolicyDTO GetCustomerDetails(Guid customerPolicyId);
        CustomerPolicy GetCustomerPolicyDetails(Guid customerPolicyId);
        IEnumerable<CustomerPolicy> GetCustomerPoliciesByCustomerId(Guid customerId);
    }
}

