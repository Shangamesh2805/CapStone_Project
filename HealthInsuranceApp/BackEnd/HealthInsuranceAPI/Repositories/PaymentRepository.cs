using System.Linq;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;
using HealthInsuranceAPI.Exceptions;

namespace HealthInsuranceApp.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public PaymentRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public Payment GetPayment(Guid paymentId)
        {
            var payment = _context.Payments.Find(paymentId);
            if (payment == null)
            {
                throw new NotFoundException($"Payment with ID {paymentId} not found.");
            }
            return payment;
        }

        public void AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void UpdatePayment(Payment payment)
        {
            try
            {
                var existingPayment = GetPayment(payment.PaymentID);
                if (existingPayment == null)
                {
                    throw new NotFoundException($"Payment with ID {payment.PaymentID} not found.");
                }
                _context.Payments.Update(payment);
                _context.SaveChanges();
            }
            catch(Exception) 
            {
                throw new InvalidOperationException();
            }
        }

        public void DeletePayment(Guid paymentId)
        {
            try
            {
                var payment = GetPayment(paymentId);
                if (payment == null)
                {
                    throw new NotFoundException($"Payment with ID {paymentId} not found.");
                }
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured While Deleting");
            }
        }
    }
}
