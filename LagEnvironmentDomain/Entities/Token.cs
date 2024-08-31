using System;

namespace LagEnvironmentDomain.Entities
{
    public class Token
    {
        public Guid Id { get; set; }
        
        public string Key { get; set; }

        public DateTime Expiration { get; set; }


        public Guid AnalysisEnvironmentId { get; set; }
        public virtual AnalysisEnvironment AnalysisEnvironment { get; set; }
    }
}
