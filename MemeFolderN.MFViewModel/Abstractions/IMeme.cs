using MemeFolderN.Common.DTOClasses;
using System;
using System.Collections.ObjectModel;

namespace MemeFolderN.MFViewModels.Common.Abstractions
{
    public interface IMeme : IFolderObject, ICopyDTO<MemeDTO>
    {
        DateTime AddingDate { get; set; }

        string ImagePath { get; set; }

        string MiniImagePath { get; set; }

        ObservableCollection<IMemeTag> MemeTags { get; }
    }
}
