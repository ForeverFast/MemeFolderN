
using System;

namespace MemeFolderN.MFModel.Abstractions
{
    public interface IMFModel : IFolderModel, IMemeModel, IMemeTagModel, IDisposable
    {
        /// <summary>Модель недоступна</summary>
        bool IsDisposable { get; }

        /// <summary>Данные загружены</summary>
        bool IsLoaded { get; }
    }
}
