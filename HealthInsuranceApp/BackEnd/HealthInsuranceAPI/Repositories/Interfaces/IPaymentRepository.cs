using HealthInsuranceAPI.Models;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Payment GetPayment(int paymentId);
        void AddPayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(int paymentId);
    }
}
