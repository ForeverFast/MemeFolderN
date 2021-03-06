using MemeFolderN.Core.DTOClasses;
using System;
using System.Collections.ObjectModel;

namespace MemeFolderN.MFViewModelsBase.Abstractions
{
    public interface IFolder : IFolderObject, ICopyDTO<FolderDTO>
    {
        string FolderPath { get; set; }

        DateTime CreatingDate { get; set; }

        ObservableCollection<IFolder> Folders { get; }

        ObservableCollection<IMeme> Memes { get; }
    }
}
