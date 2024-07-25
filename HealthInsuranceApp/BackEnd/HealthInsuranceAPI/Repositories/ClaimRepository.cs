using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;
using HealthInsuranceApp.Models;
using System.Linq;

namespace HealthInsuranceApp.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public ClaimRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public Claim GetClaim(int claimId)
        {
            return _context.Claims.Find(claimId);
        }

        public void AddClaim(Claim claim)
        {
            _context.Claims.Add(claim);
            _context.SaveChanges();
        }

        public void UpdateClaim(Claim claim)
        {
            _context.Claims.Update(claim);
            _context.SaveChanges();
        }

        public void DeleteClaim(int claimId)
        {
            var claim = GetClaim(claimId);
            if (claim != null)
            {
                _context.Claims.Remove(claim);
                _context.SaveChanges();
            }
        }
    }
}
