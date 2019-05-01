using System;
using System.Threading.Tasks;
using Deployer.FileSystem;
using ManagedWimLib;

namespace Deployer.NetFx
{
    public class WimlibImageService : ImageServiceBase
    {
        public WimlibImageService(IFileSystemOperations fileSystemOperations) : base(fileSystemOperations)
        {
        }

        public override async Task ApplyImage(Volume volume, string imagePath, int imageIndex = 1,
            bool useCompact = false, IOperationProgress progressObserver = null)
        {
            EnsureValidParameters(volume, imagePath, imageIndex);

            await Task.Run(() =>
            {
                using (var wim = Wim.OpenWim(imagePath, OpenFlags.DEFAULT,
                    (msg, info, callback) => UpdatedStatusCallback(msg, info, callback, progressObserver)))
                {
                    wim.ExtractImage(imageIndex, volume.Root, ExtractFlags.DEFAULT);
                }
            });
        }

        public override Task CaptureImage(Volume windowsVolume, string destination,
            IOperationProgress progressObserver = null)
        {
            throw new NotImplementedException();
        }

        private static CallbackStatus UpdatedStatusCallback(ProgressMsg msg, object info, object progctx,
            IOperationProgress progressObserver)
        {
            if (info is ProgressInfo_Extract m)
            {
                ulong percentComplete = 0;

                switch (msg)
                {
                    case ProgressMsg.EXTRACT_FILE_STRUCTURE:

                        if (0 < m.EndFileCount) percentComplete = m.CurrentFileCount * 10 / m.EndFileCount;

                        break;
                    case ProgressMsg.EXTRACT_STREAMS:

                        if (0 < m.TotalBytes) percentComplete = 10 + m.CompletedBytes * 80 / m.TotalBytes;

                        break;
                    case ProgressMsg.EXTRACT_METADATA:

                        if (0 < m.EndFileCount) percentComplete = 90 + m.CurrentFileCount * 10 / m.EndFileCount;

                        break;
                }

                progressObserver.Percentage.OnNext((double) percentComplete / 100);
            }


            return CallbackStatus.CONTINUE;
        }
    }
}