using System;

namespace MazeLearner
{
    public interface IAsset : IDisposable
    {
        AssetState State { get; }
        // IContentSource Source { get; }

        string Name {get;}
        bool IsLoaded {get;}
        bool IsDisposed {get;}
    }
}