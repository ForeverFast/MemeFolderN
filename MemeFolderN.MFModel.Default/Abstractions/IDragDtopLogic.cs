using MemeFolderN.Core.DTOClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModel.Wpf.Abstractions
{
    public interface IDragDtopLogic
    {
        Task AddInputDataAsync(FolderDTO folderDTO, List<string> paths);
    }
}
