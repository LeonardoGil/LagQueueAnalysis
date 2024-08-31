using System;

namespace LagQueueDomain.Entities
{
    public class AnalysisEnvironment
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Database { get; set; }
    }
}
