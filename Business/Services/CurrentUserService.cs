namespace business.Services
{
    using Interfaces;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public virtual Guid Id
        {
            get
            {
                var id = _accessor.HttpContext?.User?
                    .FindFirst(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(id?.ToString()) ? Guid.Empty : Guid.Parse(id.ToString());
            }
        }
        public bool IsAdmin
        {
            get
            {
                var role =  _accessor.HttpContext?.User
                        .FindFirst(ClaimTypes.Role);
                return role!.ToString() == "Admin" ? true : false;
            }
        }
    }
}
