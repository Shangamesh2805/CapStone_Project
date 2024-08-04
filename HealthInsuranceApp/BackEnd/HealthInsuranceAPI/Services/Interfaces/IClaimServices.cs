using HealthInsuranceAPI.DTOs;
using HealthInsuranceAPI.Models.DTOs.Claims;
using System;
using System.Collections.Generic;

namespace HealthInsuranceAPI.Services.Interfaces
{
    public interface IClaimService
    {
        void RaiseClaim(CreateClaimDTO claimDto);
        CreateClaimDTO GetClaimById(Guid claimId);
        void UpdateClaim(UpdateClaimDTO claimDto);
        void UpdateClaimStatus(Guid claimId, string status);
        void DeleteClaim(Guid claimId);
        IEnumerable<CreateClaimDTO> GetAllClaims();
        IEnumerable<ClaimDTO> GetClaimsByCustomerPolicy(Guid customerPolicyId);
    }
}
