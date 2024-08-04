using System;

namespace HealthInsuranceApp.Exceptions
{
    public class InvalidOperationException : Exception
    {
        private string msg;
        public InvalidOperationException()
        {
            msg = "An Invalid Operation Occured While excecuting ";
        }
        public override string Message => msg;
    }
}
