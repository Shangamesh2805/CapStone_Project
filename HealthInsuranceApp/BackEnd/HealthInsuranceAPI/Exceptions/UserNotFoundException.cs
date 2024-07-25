namespace HealthInsuranceAPI.Exceptions
{

    [Serializable]
    public class UserNotFoundException : Exception
    {
        private string msg;
        public UserNotFoundException()
        {
            msg = "No User Found with given UserID ";
        }
        public override string Message => msg;
    }
}
