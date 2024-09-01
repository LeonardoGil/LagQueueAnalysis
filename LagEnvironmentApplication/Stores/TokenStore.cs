using LagEnvironmentApplication.Interfaces;
using LagEnvironmentDomain.Entities;

namespace LagEnvironmentApplication.Stores
{
    public class TokenStore : ITokenStore
    {
        private readonly Dictionary<Guid, Token> _tokens = new();

        public AnalysisEnvironment GetEnvironment(Guid key)
        {
            _tokens.TryGetValue(key, out var token);
            
            return token?.AnalysisEnvironment;
        }

        public Token GetValidTokenByEnvironmentId(Guid environmentId)
        {
            Func<KeyValuePair<Guid, Token>, bool> query = dic => dic.Value.Id == environmentId && 
                                                                 dic.Value.Expiration >= DateTime.UtcNow;

            if (_tokens.Any(query))
                return _tokens.First(query).Value;

            return default;
        }

        public bool HasToken(Guid token)
        {
            return _tokens.ContainsKey(token);
        }

        public void SetToken(Guid token, Token environment)
        {
            _tokens[token] = environment;
        }
    }
}
