using System.Threading.Tasks;
using Deployer.Execution;
using Serilog;

namespace Deployer.Tasks
{
    [TaskDescription("Deploying Windows")]
    public class DeployWindows : IDeploymentTask
    {
        private readonly IDeploymentContext context;
        private readonly IOperationProgress progressObserver;

        public DeployWindows(IDeploymentContext context, IOperationProgress progressObserver)
        {
            this.context = context;
            this.progressObserver = progressObserver;
        }

        public async Task Execute()
        {
            Log.Information("Deploying Windows...");
            await context.WindowsDeployer.Deploy(context.Device, progressObserver);
        }
    }
}