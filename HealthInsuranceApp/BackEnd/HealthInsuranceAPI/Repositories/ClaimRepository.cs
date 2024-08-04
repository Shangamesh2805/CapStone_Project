using System;
using System.Collections.Generic;
using System.Linq;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthInsuranceAPI.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public ClaimRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public Claim GetClaim(Guid claimId)
        {
            try
            {
                return _context.Claims.Find(claimId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching the claim", ex);
            }
        }

        public void RaiseClaim(Claim claim)
        {
            try
            {
                _context.Claims.Add(claim);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding the claim", ex);
            }
        }

        public void UpdateClaim(Claim claim)
        {
            try
            {
                _context.Entry(claim).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating the claim", ex);
            }
        }

        public void DeleteClaim(Guid claimId)
        {
            try
            {
                var claim = _context.Claims.Find(claimId);
                if (claim != null)
                {
                    _context.Claims.Remove(claim);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting the claim", ex);
            }
        }

        public void UpdateClaimStatus(Guid claimId, ClaimStatus status)
        {
            try
            {
                var claim = _context.Claims.Find(claimId);
                if (claim != null)
                {
                    claim.ClaimStatus = status;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating the claim status", ex);
            }
        }
        public IEnumerable<Claim> GetAllClaims()
        {
            return _context.Claims.ToList();
        }
        public IEnumerable<Claim> GetClaimsByCustomerPolicy(Guid customerPolicyId) 
        {
            return _context.Claims
                .Where(c => c.CustomerPolicyID == customerPolicyId)
                .ToList();
        }
    }
}
