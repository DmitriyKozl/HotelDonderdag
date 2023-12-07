using System.Runtime.Serialization;

namespace HotelProject.BL.Model
{
    [Serializable]
    internal class RegistrationException : Exception
    {


        public RegistrationException(string? message) : base(message)
        {
        }

        public RegistrationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }


    }
}