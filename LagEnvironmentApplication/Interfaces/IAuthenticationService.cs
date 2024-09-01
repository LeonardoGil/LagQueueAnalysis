using LagEnvironmentApplication.Models;

namespace LagEnvironmentApplication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(AuthenticateModel authenticate);
    }
}
