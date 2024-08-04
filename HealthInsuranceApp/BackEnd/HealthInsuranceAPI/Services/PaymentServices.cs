using System;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.Payment;
using HealthInsuranceAPI.Models.DTOs.Payments;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceAPI.Services.Interfaces;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IRenewalRepository _renewalRepository;
    private readonly ICustomerPolicyRepository _customerPolicyRepository;
    private readonly IRenewalService _renewalService;

    public PaymentService(
        IPaymentRepository paymentRepository,
        IRenewalRepository renewalRepository,
        ICustomerPolicyRepository customerPolicyRepository,
        IRenewalService renewalService)
    {
        _paymentRepository = paymentRepository;
        _renewalRepository = renewalRepository;
        _customerPolicyRepository = customerPolicyRepository;
        _renewalService = renewalService;
    }

    public void ProcessRenewalPayment(Guid renewalId, RenewalPaymentDTO paymentDto)
    {
        try
        {
            var renewal = _renewalRepository.GetRenewal(renewalId);
            if (renewal == null)
                throw new Exception("Renewal not found.");

            var customerPolicy = _customerPolicyRepository.GetCustomerPolicy(renewal.CustomerPolicyID);
            if (customerPolicy == null)
                throw new Exception("Customer policy not found.");

            var calculatedAmount = CalculateRenewalAmount(customerPolicy);

            if (paymentDto.Amount != calculatedAmount)
                throw new Exception("Payment amount does not match the renewal amount.");

            var payment = new Payment
            {
                PaymentID = Guid.NewGuid(),
                CustomerPolicyID = renewal.CustomerPolicyID,
                PaymentAmount = paymentDto.Amount,
                PaymentDate = DateTime.UtcNow,
                PaymentType = PaymentType.Renewal
            };

            _paymentRepository.AddPayment(payment);

            renewal.IsRenewed = true;
            _renewalRepository.UpdateRenewal(renewal);

             
            _renewalService.UpdateCustomerPolicyDates(customerPolicy.CustomerPolicyID);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while processing the renewal payment.", ex);
        }
    }


    public void ProcessNewApplicationPayment(Guid customerPolicyId, PaymentDTO paymentDto)
    {
        try
        {
            var payment = new Payment
            {
                PaymentID = Guid.NewGuid(),
                CustomerPolicyID = customerPolicyId,
                PaymentAmount = paymentDto.Amount,
                PaymentDate = DateTime.UtcNow,
                PaymentType = paymentDto.PaymentType
            };

            _paymentRepository.AddPayment(payment);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while processing the payment.", ex);
        }
    }

    private decimal CalculateRenewalAmount(CustomerPolicy customerPolicy)
    {
        var renewalAmount = customerPolicy.InsurancePolicy.PremiumAmount * 0.1M;

        if (CheckDiscountEligibility(customerPolicy))
        {
            renewalAmount *= 0.9M;
        }

        return renewalAmount;
    }

    private bool CheckDiscountEligibility(CustomerPolicy customerPolicy)
    {
        var lastYear = DateTime.Now.Year - 1;
        return customerPolicy.Claims.All(c => c.ClaimDate.Year != lastYear);
    }
}
