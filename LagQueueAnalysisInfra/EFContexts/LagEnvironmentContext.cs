﻿using LagQueueDomain.Entities;

using Microsoft.EntityFrameworkCore;

namespace LagQueueAnalysisInfra.EFContexts
{
    public class LagEnvironmentContext : DbContext
    {
        public LagEnvironmentContext(DbContextOptions<LagEnvironmentContext> options) : base(options)
        {
        }

        public DbSet<AnalysisEnvironment> Environments { get; set; }
    }
}
