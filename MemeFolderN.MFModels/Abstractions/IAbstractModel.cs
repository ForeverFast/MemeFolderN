using System;

namespace MemeFolderN.MFModels.Abstractions
{
    public interface IAbstractModel : IDisposable
    {
        /// <summary>Модель недоступна</summary>
        bool IsDisposable { get; }

        /// <summary>Данные загружены</summary>
        bool IsLoaded { get; }
    }
}
