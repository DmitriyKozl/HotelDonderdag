namespace Hotel.DL.Exceptions
{
    public class RegistrationRepositoryException : Exception
    {
        public RegistrationRepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RegistrationRepositoryException(string message) : base(message)
        {
        }
    }

}
