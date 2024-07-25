using HealthInsuranceAPI.Models;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface IAgentRepository
    {
        Agent GetAgent(int agentId);
        void AddAgent(Agent agent);
        void UpdateAgent(Agent agent);
        void DeleteAgent(int agentId);

    }
}
