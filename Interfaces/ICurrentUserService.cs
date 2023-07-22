namespace Interfaces
{
    public interface ICurrentUserService
    {
        public Guid Id { get;}
        public bool IsAdmin { get; }
    }
}
