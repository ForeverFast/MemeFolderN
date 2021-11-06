using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;

namespace MemeFolderN.MFViewModels.Default
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
