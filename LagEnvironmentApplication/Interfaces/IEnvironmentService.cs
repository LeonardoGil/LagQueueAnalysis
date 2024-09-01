using LagEnvironmentApplication.Models;
using LagEnvironmentDomain.Entities;

namespace LagEnvironmentApplication.Interfaces
{
    public interface IEnvironmentService
    {
        Task<AnalysisEnvironment> ToGenerate(GenerateEnvironmentModel generateEnvironment);
    }
}
