namespace Hotel.DL.Exceptions; 

public class ActivityRepositoryException : Exception {
    
    public ActivityRepositoryException(string message) : base(message)
    {

    }

    public ActivityRepositoryException(string message, Exception innerException) : base(message, innerException)
    {

    }
    
}