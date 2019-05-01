using Deployer.Tasks;

namespace Deployer
{
    public class DeploymentContext: IDeploymentContext
    {
        public IDiskLayoutPreparer DiskLayoutPreparer { get; set; } = new NullDiskPreparer();
        public IDevice Device { get; set; } = new NullDevice();
        public SlimWindowsDeploymentOptions DeploymentOptions { get; set; } = new SlimWindowsDeploymentOptions();
    }
}