
namespace HotelProject.BL.Exceptions
{

    public class ActivityException : Exception
    {
        public ActivityException(string? message) : base(message)
        {
        }

        public ActivityException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}