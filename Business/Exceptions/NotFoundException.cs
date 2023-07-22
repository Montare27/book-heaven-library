namespace business.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Type type, object id) : base($"Was not found any object of type {type} by id {id}")
        {
            
        }
    }
}
