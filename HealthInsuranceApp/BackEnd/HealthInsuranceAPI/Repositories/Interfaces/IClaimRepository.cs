using HealthInsuranceAPI.Models;
using System;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface IClaimRepository
    {
        Claim GetClaim(Guid claimId);
        void RaiseClaim(Claim claim);
        void UpdateClaim(Claim claim);
        void DeleteClaim(Guid claimId);
        void UpdateClaimStatus(Guid claimId, ClaimStatus status);
        IEnumerable<Claim> GetAllClaims();
        IEnumerable<Claim> GetClaimsByCustomerPolicy(Guid customerPolicyId);

    }
}
