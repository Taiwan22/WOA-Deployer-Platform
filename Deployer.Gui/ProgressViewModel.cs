﻿using System;
using System.Reactive.Linq;
using ByteSizeLib;
using ReactiveUI;
using Serilog;

namespace Deployer.Gui
{
    public class ProgressViewModel : ReactiveObject
    {
        public IReactiveCommand Command { get; }

        private readonly ObservableAsPropertyHelper<double> progress;
        private readonly ObservableAsPropertyHelper<bool> isProgressVisible;
        private readonly ObservableAsPropertyHelper<bool> isProgressIndeterminate;
        private readonly ObservableAsPropertyHelper<ByteSize> downloaded;
        private readonly ObservableAsPropertyHelper<bool> isExecuting;

        public ProgressViewModel(object owner, IReactiveCommand command, IDialog dialog, IOperationProgress operationProgress)
        {
            Command = command;
            progress = operationProgress.Percentage
                .Where(d => !double.IsNaN(d))
                .ToProperty(this, model => model.Progress);

            isProgressVisible = operationProgress.Percentage
                .Select(d => !double.IsNaN(d))
                .CombineLatest(command.IsExecuting, (isNumber, isExecuting) => isNumber && isExecuting)
                .ToProperty(this, x => x.IsProgressVisible);

            isProgressIndeterminate = operationProgress.Percentage
                .Select(double.IsPositiveInfinity)
                .ToProperty(this, x => x.IsProgressIndeterminate);

            downloaded = operationProgress.Value
                .Select(x => ByteSize.FromBytes(x))
                .Sample(TimeSpan.FromSeconds(1))
                .ToProperty(this, model => model.Downloaded);

            command.ThrownExceptions.Subscribe(async e =>
            {
                Log.Error(e, "An error has occurred");
                Log.Information($"Error: {e.Message}");
                await dialog.ShowAlert(owner, Resources.ErrorTitle, $"{e.Message}");
            });

            isExecuting = command.IsExecuting
                //.StartWith(false)
                .ToProperty(this, x => x.IsExecuting);
        }

        public bool IsExecuting => isExecuting.Value;

        public ByteSize Downloaded => downloaded.Value;

        public bool IsProgressIndeterminate => isProgressIndeterminate.Value;

        public bool IsProgressVisible => isProgressVisible.Value;

        public double Progress => progress.Value;
    }
}