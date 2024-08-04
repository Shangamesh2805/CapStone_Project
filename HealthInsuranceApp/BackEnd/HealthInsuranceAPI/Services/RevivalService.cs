using HealthInsuranceAPI.Models.DTOs.Revival;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceAPI.Services.Interfaces;
using System;

public class RevivalService : IRevivalService
{
    private readonly IRevivalRepository _revivalRepository;
    private readonly ICustomerPolicyRepository _customerPolicyRepository;

    public RevivalService(IRevivalRepository revivalRepository, ICustomerPolicyRepository customerPolicyRepository)
    {
        _revivalRepository = revivalRepository;
        _customerPolicyRepository = customerPolicyRepository;
    }

    public void AddRevival(CreateRevivalDTO revivalDto)
    {
        var customerPolicy = _customerPolicyRepository.GetCustomerPolicy(revivalDto.CustomerPolicyID);
        if (customerPolicy == null) throw new Exception("Customer policy not found");

        var revival = new Revival
        {
            RevivalID = Guid.NewGuid(),
            CustomerPolicyID = revivalDto.CustomerPolicyID,
            RevivalDate = revivalDto.RevivalDate,
            Reason = revivalDto.Reason,
            IsApproved = false // Default to false, approval required
        };

        _revivalRepository.AddRevival(revival);
    }

    public void ApproveRevival(Guid revivalId)
    {
        var revival = _revivalRepository.GetRevival(revivalId);
        if (revival == null) throw new Exception("Revival not found");

        revival.IsApproved = true;
        _revivalRepository.UpdateRevival(revival);

        var customerPolicy = _customerPolicyRepository.GetCustomerPolicy(revival.CustomerPolicyID);
        if (customerPolicy != null)
        {
            customerPolicy.Status = PolicyStatus.Active;
            _customerPolicyRepository.UpdateCustomerPolicy(customerPolicy);
        }
    }

    public void RejectRevival(Guid revivalId)
    {
        var revival = _revivalRepository.GetRevival(revivalId);
        if (revival == null) throw new Exception("Revival not found");

        _revivalRepository.DeleteRevival(revivalId);
    }

    public RevivalDTO GetRevivalById(Guid revivalId)
    {
        var revival = _revivalRepository.GetRevival(revivalId);
        if (revival == null) throw new Exception("Revival not found");

        return new RevivalDTO
        {
            RevivalID = revival.RevivalID,
            CustomerPolicyID = revival.CustomerPolicyID,
            RevivalDate = revival.RevivalDate,
            Reason = revival.Reason,
            IsApproved = revival.IsApproved
        };
    }
    public IEnumerable<RevivalDTO> GetAllRevivals()
    {
        return _revivalRepository.GetAllRevivals().Select(revival => new RevivalDTO
        {
            RevivalID = revival.RevivalID,
            CustomerPolicyID = revival.CustomerPolicyID,
            RevivalDate = revival.RevivalDate,
            Reason = revival.Reason,
            IsApproved = revival.IsApproved
        }).ToList();
    }

}
