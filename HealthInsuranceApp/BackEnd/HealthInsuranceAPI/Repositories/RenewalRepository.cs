using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;
using System;
using System.Linq;

namespace HealthInsuranceAPI.Repositories
{
    public class RenewalRepository : IRenewalRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public RenewalRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public Renewal GetRenewal(Guid renewalId)
        {
            try
            {
                return _context.Renewals.Find(renewalId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching the renewal", ex);
            }
        }

        public void AddRenewal(Renewal renewal)
        {
            try
            {
                _context.Renewals.Add(renewal);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding the renewal", ex);
            }
        }

        public void UpdateRenewal(Renewal renewal)
        {
            try
            {
                _context.Entry(renewal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating the renewal", ex);
            }
        }

        public void DeleteRenewal(Guid renewalId)
        {
            try
            {
                var renewal = _context.Renewals.Find(renewalId);
                if (renewal != null)
                {
                    _context.Renewals.Remove(renewal);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting the renewal", ex);
            }
        }

        public IEnumerable<Renewal> GetAllRenewals()
        {
            return _context.Renewals.ToList();
        }
    }
}
