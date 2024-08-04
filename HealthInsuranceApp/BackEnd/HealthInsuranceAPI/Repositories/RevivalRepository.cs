using HealthInsuranceAPI.Exceptions;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;
using System;
using System.Linq;

namespace HealthInsuranceAPI.Repositories
{
    public class RevivalRepository : IRevivalRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public RevivalRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public Revival GetRevival(Guid revivalId)
        {
            try
            {
                return _context.Revivals.Find(revivalId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching the revival", ex);
            }
        }

        public void AddRevival(Revival revival)
        {
            try
            {
                _context.Revivals.Add(revival);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding the revival", ex);
            }
        }

        public void UpdateRevival(Revival revival)
        {
            try
            {
                _context.Entry(revival).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating the revival", ex);
            }
        }

        public void DeleteRevival(Guid revivalId)
        {
            try
            {
                var revival = _context.Revivals.Find(revivalId);
                if (revival != null)
                {
                    _context.Revivals.Remove(revival);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting the revival", ex);
            }
        }

        public IEnumerable<Revival> GetAllRevivals()
        {
            try
            {
                return _context.Revivals.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error",ex);
            }
        }
    }
}
