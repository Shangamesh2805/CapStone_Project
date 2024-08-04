using HealthInsuranceAPI.Models;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface IRenewalRepository
    {
        Renewal GetRenewal(Guid renewalId);
        void AddRenewal(Renewal renewal);
        void UpdateRenewal(Renewal renewal);
        void DeleteRenewal(Guid renewalId);

        IEnumerable<Renewal> GetAllRenewals();
    }
}
