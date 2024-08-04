using HealthInsuranceAPI.DTOs;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.Claims;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInsuranceAPI.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;
        private readonly ICustomerPolicyRepository _customerPolicyRepository;

        public ClaimService(IClaimRepository claimRepository, ICustomerPolicyRepository customerPolicyRepository)
        {
            _claimRepository = claimRepository;
            _customerPolicyRepository = customerPolicyRepository;
        }

        public CreateClaimDTO GetClaimById(Guid claimId)
        {
            var claim = _claimRepository.GetClaim(claimId);
            if (claim == null) throw new Exception("Claim not found");

            return new ClaimDTO
            {
                ClaimID = claim.ClaimID,
                CustomerPolicyID = claim.CustomerPolicyID,
                ClaimAmount = claim.ClaimAmount,
                ClaimDate = claim.ClaimDate,
                Reason = claim.Reason,
                ClaimStatus = claim.ClaimStatus.ToString()
            };
        }

        public void RaiseClaim(CreateClaimDTO claimDto)
        {
            try
            {
                if (claimDto == null)
                {
                    throw new ArgumentNullException(nameof(claimDto), "Claim data is required");
                }

                var customerPolicy = _customerPolicyRepository.GetCustomerPolicy(claimDto.CustomerPolicyID);
                if (customerPolicy == null)
                {
                    throw new Exception("Customer policy not found");
                }

                var claim = new Claim
                {
                    ClaimID = Guid.NewGuid(),
                    CustomerPolicyID = claimDto.CustomerPolicyID,
                    ClaimAmount = claimDto.ClaimAmount,
                    ClaimDate = DateTime.Now,
                    ClaimStatus = ClaimStatus.Pending,
                    Reason = claimDto.Reason
                };

                _claimRepository.RaiseClaim(claim);
            }
            catch (Exception ex)
            {
                // Log detailed exception
                Console.WriteLine($"Error while raising claim: {ex.Message}");
                Console.WriteLine(ex.StackTrace); // Log stack trace for detailed debugging
                throw new Exception("An error occurred while processing your request.");
            }
        }


        public void UpdateClaim(UpdateClaimDTO claimDto)
        {
            var claim = _claimRepository.GetClaim(claimDto.ClaimID);
            if (claim == null) throw new Exception("Claim not found");

            claim.ClaimAmount = claimDto.ClaimAmount;
            claim.Reason = claimDto.Reason;

            _claimRepository.UpdateClaim(claim);
        }

        public void DeleteClaim(Guid claimId)
        {
            _claimRepository.DeleteClaim(claimId);
        }

        public void UpdateClaimStatus(Guid claimId, string status)
        {
            var claim = _claimRepository.GetClaim(claimId);
            if (claim == null) throw new Exception("Claim not found");

            if (Enum.TryParse<ClaimStatus>(status, out var claimStatus))
            {
                claim.ClaimStatus = claimStatus;
                _claimRepository.UpdateClaimStatus(claimId, claimStatus);
            }
            else
            {
                throw new Exception("Invalid claim status");
            }
        }

        public IEnumerable<CreateClaimDTO> GetAllClaims()
        {
            return _claimRepository.GetAllClaims().Select(c => new ClaimDTO
            {
                ClaimID = c.ClaimID,
                CustomerPolicyID = c.CustomerPolicyID,
                ClaimAmount = c.ClaimAmount,
                ClaimDate = c.ClaimDate,
                Reason = c.Reason,
                ClaimStatus = c.ClaimStatus.ToString()
            }).ToList();
        }

        public IEnumerable<ClaimDTO> GetClaimsByCustomerPolicy(Guid customerPolicyId)
        {
            return _claimRepository.GetClaimsByCustomerPolicy(customerPolicyId).Select(c => new ClaimDTO
            {
                ClaimID = c.ClaimID,
                CustomerPolicyID = c.CustomerPolicyID,
                ClaimAmount = c.ClaimAmount,
                ClaimDate = c.ClaimDate,
                Reason = c.Reason,
                ClaimStatus = c.ClaimStatus.ToString()
            }).ToList();
        }
    }
}
