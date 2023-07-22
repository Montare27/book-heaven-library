namespace AuthApi.ViewModels
{
    public class ClaimsModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
