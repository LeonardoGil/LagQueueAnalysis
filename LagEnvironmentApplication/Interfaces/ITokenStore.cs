using LagEnvironmentDomain.Entities;

namespace LagEnvironmentApplication.Interfaces
{
    public interface ITokenStore
    {
        void SetToken(Guid token, Token environment);
        
        AnalysisEnvironment GetEnvironment(Guid token);
        
        bool HasToken(Guid token);

        Token GetValidTokenByEnvironmentId(Guid environmentId);
    }
}
