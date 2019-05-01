using System.Threading.Tasks;
using Deployer.Tasks;
using Grace.DependencyInjection.Attributes;

namespace Deployer
{
    [Metadata("IsNull", true)] 
    [Metadata("Name", nameof(NullWindowsDeployer))] 
    public class NullWindowsDeployer : IHighLevelWindowsDeployer
    {
        public Task Deploy(IDevice device, IOperationProgress progressObserver)
        {
            return Task.CompletedTask;
        }
    }
}