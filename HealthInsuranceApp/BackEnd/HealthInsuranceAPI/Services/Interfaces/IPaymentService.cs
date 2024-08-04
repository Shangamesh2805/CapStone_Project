using System;
using HealthInsuranceAPI.Models.DTOs.Payment;
using HealthInsuranceAPI.Models.DTOs.Payments;

namespace HealthInsuranceAPI.Services.Interfaces
{
    public interface IPaymentService
    {
        /// <summary>
        /// Processes the payment for a policy renewal.
        /// </summary>
        /// <param name="renewalId">The ID of the renewal.</param>
        /// <param name="paymentDto">The payment details.</param>
        void ProcessRenewalPayment(Guid renewalId, RenewalPaymentDTO paymentDto);

        /// <summary>
        /// Processes the payment for a new insurance application.
        /// </summary>
        /// <param name="customerPolicyId">The ID of the customer policy.</param>
        /// <param name="paymentDto">The payment details.</param>
        void ProcessNewApplicationPayment(Guid customerPolicyId, PaymentDTO paymentDto);
    }
}
