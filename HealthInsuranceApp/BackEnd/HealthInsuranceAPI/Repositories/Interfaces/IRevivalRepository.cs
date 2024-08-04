using HealthInsuranceAPI.Models;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface IRevivalRepository
    {
        Revival GetRevival(Guid revivalId);
        void AddRevival(Revival revival);
        void UpdateRevival(Revival revival);
        void DeleteRevival(Guid revivalId);
        IEnumerable<Revival> GetAllRevivals();
    }
}
