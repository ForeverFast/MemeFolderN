using MemeFolderN.Core.DTOClasses;
using System;
using System.Collections.ObjectModel;

namespace MemeFolderN.MFViewModelsBase.Abstractions
{
    public interface IMeme : IFolderObject, ICopyDTO<MemeDTO>
    {
        DateTime AddingDate { get; set; }

        string ImagePath { get; set; }

        string MiniImagePath { get; set; }

        ObservableCollection<IMemeTag> Tags { get; }
    }
}
