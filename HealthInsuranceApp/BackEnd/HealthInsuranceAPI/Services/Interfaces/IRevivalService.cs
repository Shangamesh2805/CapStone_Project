using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.Revival;

namespace HealthInsuranceAPI.Services.Interfaces
{
    public interface IRevivalService
    {
        void AddRevival(CreateRevivalDTO revivalDto);
        RevivalDTO GetRevivalById(Guid revivalId);

        IEnumerable<RevivalDTO> GetAllRevivals();
        void ApproveRevival(Guid revivalId);
        void RejectRevival(Guid revivalId);
    }
}
