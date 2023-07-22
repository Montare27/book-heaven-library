namespace business.Exceptions
{
    public class NotAccessedActionException : Exception
    {
        public NotAccessedActionException(Type type, Guid id) : base($"Unauthorized action to resource {type} by user id {id}")
        {
            
        }
    }
}
