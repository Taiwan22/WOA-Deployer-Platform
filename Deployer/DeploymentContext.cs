using Deployer.Tasks;

namespace Deployer
{
    public class DeploymentContext: IDeploymentContext
    {
        public IHighLevelWindowsDeployer WindowsDeployer { get; set; } = new NullWindowsDeployer();
        public IDevice Device { get; set; } = new NullDevice();
        public SlimWindowsDeploymentOptions DeploymentOptions { get; set; } = new SlimWindowsDeploymentOptions();
    }
}