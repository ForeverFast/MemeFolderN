using MemeFolderN.Common.DTOClasses;
using MemeFolderN.MFViewModels.Common;

namespace MemeFolderN.MFViewModels.Wpf
{
    public class MemeTagVM : MemeTagVMBase
    {
        public MemeTagVM(MemeTagDTO memeTagDTO) : base()
        {
            this.CopyFromDTO(memeTagDTO);
        }

        protected override void CleanUp(bool clean)
        {
            if (!this._disposed)
            {
                if (clean)
                {
                    this.Memes.Clear();
                }
            }
            this._disposed = true;
        }

        ~MemeTagVM()
        {
            CleanUp(false);
        }
    }
}
