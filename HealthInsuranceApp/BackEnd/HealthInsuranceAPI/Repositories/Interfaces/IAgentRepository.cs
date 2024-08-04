using HealthInsuranceAPI.Models;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface IAgentRepository
    {
        Agent GetAgent(Guid agentId);
        void AddAgent(Agent agent);
        void UpdateAgent(Agent agent);
        void DeleteAgent(Guid agentId);

        IEnumerable<Agent> GetAllAgents();

    }
}
