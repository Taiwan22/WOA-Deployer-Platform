namespace Deployer.Tasks
{
    public interface IDeploymentContext
    {
        IDiskLayoutPreparer DiskLayoutPreparer { get; set; }
        IDevice Device { get; set; }
        SlimWindowsDeploymentOptions DeploymentOptions { get; set; }
    }
}