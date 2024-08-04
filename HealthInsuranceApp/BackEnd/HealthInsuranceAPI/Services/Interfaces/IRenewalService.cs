using HealthInsuranceAPI.Models.DTOs.Renewal;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.DTOs;
using HealthInsuranceAPI.Models.DTOs.Payments;

namespace HealthInsuranceAPI.Services.Interfaces
{
    public interface IRenewalService
    {
        
        RenewalDTO GetRenewalById(Guid renewalId);
        void UpdateRenewal(UpdateRenewalDTO renewalDto);
        void DeleteRenewal(Guid renewalId);
        IEnumerable<RenewalDTO> GetAllRenewals();
        void UpdateCustomerPolicyDates(Guid customerPolicyId);
        
        RenewalResponseDTO AddRenewal(Guid customerPolicyId);
    }
}
