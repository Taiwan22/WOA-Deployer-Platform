namespace Deployer.Tasks
{
    public interface IDeploymentContext
    {
        IHighLevelWindowsDeployer WindowsDeployer { get; set; }
        IDevice Device { get; set; }
        SlimWindowsDeploymentOptions DeploymentOptions { get; set; }
    }
}