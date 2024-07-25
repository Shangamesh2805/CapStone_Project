using HealthInsuranceAPI.Models;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface IClaimRepository
    {
        Claim GetClaim(int claimId);
        void AddClaim(Claim claim);
        void UpdateClaim(Claim claim);
        void DeleteClaim(int claimId);
    }
}
