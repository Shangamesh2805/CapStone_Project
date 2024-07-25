using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;
using HealthInsuranceApp.Models;
using System.Linq;

namespace HealthInsuranceApp.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public PaymentRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public Payment GetPayment(int paymentId)
        {
            return _context.Payments.Find(paymentId);
        }

        public void AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void UpdatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }

        public void DeletePayment(int paymentId)
        {
            var payment = GetPayment(paymentId);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
        }
    }
}
