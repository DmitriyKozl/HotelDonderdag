namespace Hotel.DL.Exceptions
{
    public class MemberRepositoryException : Exception
    {
        public MemberRepositoryException(string message) : base(message)
        {

        }
        public MemberRepositoryException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
