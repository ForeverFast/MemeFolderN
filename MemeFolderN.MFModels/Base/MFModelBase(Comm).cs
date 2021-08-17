using MemeFolderN.MFModel.Abstractions;
using System;

namespace MemeFolderN.MFModel.Base
{
    public partial class MFModelBase
    {
        public bool IsDisposable { get; protected set; } = true;
        public bool IsLoaded { get; protected set; } = false;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing) => IsLoaded = !(IsDisposable = true);

        ~MFModelBase()
        {
            Dispose(false);
        }

    }
}
