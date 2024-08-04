using HealthInsuranceAPI.Models.DTOs.Agents;

namespace HealthInsuranceAPI.Services.Interfaces
{
    public interface IAgentServices
    {
        void ApproveClaim(Guid claimId);
        void RejectClaim(Guid claimId);

        void AddAgent(CreateAgentDTO agentDto);
        AgentDTO GetAgentById(Guid agentId);
        void UpdateAgent(UpdateAgentDTO agentDto);
        void DeleteAgent(Guid agentId);
        IEnumerable<AgentDTO> GetAllAgents();
    }
}
