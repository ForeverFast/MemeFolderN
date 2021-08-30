using MemeFolderN.Core.DTOClasses;

namespace MemeFolderN.MFViewModels.Default.MethodCommands
{
    public interface IMemeTagMethodCommandsClass
    {
        void MemeTagAddMethodAsync();
        void MemeTagChangeMethodAsync(MemeTagDTO memeTagDTO);
        void MemeTagDeleteMethodAsync(MemeTagDTO memeTagDTO);
    }
}
