namespace Hotel.DL.Exceptions
{
    public class OrganizerRepositoryException : Exception
    {
        public OrganizerRepositoryException(string message) : base(message)
        {

        }
        public OrganizerRepositoryException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
