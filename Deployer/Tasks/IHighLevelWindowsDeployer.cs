using System.Threading.Tasks;

namespace Deployer.Tasks
{
    public interface IHighLevelWindowsDeployer
    {
        Task Deploy(IDevice device, IOperationProgress progressObserver);
    }
}