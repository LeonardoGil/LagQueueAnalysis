using System;

namespace LagEnvironmentDomain.Entities
{
    public class Token
    {
        public Guid Id { get; set; }
        
        public DateTime Expiration { get; set; }

        public AnalysisEnvironment AnalysisEnvironment { get; set; }

        public static Token Create(AnalysisEnvironment environment)
        {
            return new Token
            {
                Id = Guid.NewGuid(),
                Expiration = DateTime.UtcNow.AddHours(3),
                AnalysisEnvironment = environment
            };
        }
    }
}
