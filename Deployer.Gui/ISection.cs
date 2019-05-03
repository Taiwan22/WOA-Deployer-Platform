using System;

namespace Deployer.Gui
{
    public interface ISection
    {
        IObservable<bool> IsBusyObservable { get; }
        string Name { get; }
    }
}