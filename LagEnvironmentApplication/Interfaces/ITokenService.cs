using LagEnvironmentDomain.Entities;

namespace LagEnvironmentApplication.Interfaces
{
    public interface ITokenService
    {
        Token Register(AnalysisEnvironment environment);
    }
}
