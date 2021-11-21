using MemeFolderN.Common.DTOClasses;
using MemeFolderN.MFViewModels.Common;

namespace MemeFolderN.MFViewModels.Wpf
{
    public partial class FolderVM : FolderVMBase
    {
        public FolderVM(FolderDTO folderDTO) : base()
        {
            this.CopyFromDTO(folderDTO);
        }

        //public override void CopyFromDTO(FolderDTO dto)
        //{
        //    base.CopyFromDTO(dto);

        //    if (dto.Folders?.Count > 0)
        //    {
        //        foreach (FolderDTO folder in dto.Folders)
        //            Folders.Add(new FolderVM(folder));
        //    }

        //    if (dto.Memes?.Count > 0)
        //    {
        //        foreach (MemeDTO meme in dto.Memes)
        //            Memes.Add(new MemeVM(meme));
        //    }
        //}

        protected override void CleanUp(bool clean)
        {
            if (!this._disposed)
            {
                if (clean)
                {
                    Folders.Clear();
                    Memes.Clear();
                    SelectedFolder = null;
                    SelectedMeme = null;
                }
            }
            this._disposed = true;
        }

        ~FolderVM()
        {
            CleanUp(false);
        }
    }
}
