using MemeFolderN.Core.DTOClasses;
using System;

namespace MemeFolderN.MFViewModels.Default.MethodCommands
{
    public interface IMemeMethodCommandsClass
    {
        void MemeAddMethodAsync(Guid? parentFolderId);
        void MemeChangeMethodAsync(MemeDTO memeDTO);
        void MemeDeleteMethodAsync(MemeDTO memeDTO);
        void MemeOpenMethod(string imagePath);
        void MemeCopyMethod(string imagePath);
    }
}
