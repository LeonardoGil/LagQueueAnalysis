using LagEnvironmentApplication.Models;

namespace LagEnvironmentApplication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Guid> Authenticate(AuthenticateModel authenticate);
    }
}
