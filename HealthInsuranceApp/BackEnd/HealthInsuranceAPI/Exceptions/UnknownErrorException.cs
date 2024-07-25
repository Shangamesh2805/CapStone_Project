using System.Runtime.Serialization;

namespace HealthInsuranceAPI.Exceptions
{
    [Serializable]
    internal class UnknownErrorException : Exception
    {
        private string msg;

        public UnknownErrorException()
        {
            msg = "An Error Occured while Adding the User";
        }
        public override string Message => msg;
    }
}