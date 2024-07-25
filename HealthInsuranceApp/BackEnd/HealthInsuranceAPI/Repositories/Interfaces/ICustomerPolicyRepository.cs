using HealthInsuranceAPI.Models;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface ICustomerPolicyRepository
    {
        CustomerPolicy GetCustomerPolicy(int customerPolicyId);
        void AddCustomerPolicy(CustomerPolicy customerPolicy);
        void UpdateCustomerPolicy(CustomerPolicy customerPolicy);
        void DeleteCustomerPolicy(int customerPolicyId);
    }
}
