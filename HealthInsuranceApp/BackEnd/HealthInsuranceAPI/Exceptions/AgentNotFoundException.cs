using System.Runtime.Serialization;

namespace HealthInsuranceAPI.Exceptions
{
    [Serializable]
    internal class AgentNotFoundException : Exception
    {
        public AgentNotFoundException()
        {

        }

    }
}