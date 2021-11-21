using MemeFolderN.Common.DTOClasses;
using MemeFolderN.MFViewModels.Common;
using System.Linq;

namespace MemeFolderN.MFViewModels.Wpf
{
    public partial class MemeVM : MemeVMBase
    {
        public MemeVM(MemeDTO memeDTO) : base()
        {
            this.CopyFromDTO(memeDTO);
        }

        //public override void CopyFromDTO(MemeDTO dto)
        //{
        //    base.CopyFromDTO(dto);
        //    if (dto.Tags?.Count > 0)
        //    {

        //        foreach (MemeTagDTO memeTag in dto.Tags)
        //            MemeTags.Add(new MemeTagVM(memeTag));

        //    }
        //}

        public override MemeDTO CopyDTO()
        {
            MemeDTO baseDTO = base.CopyDTO();

            baseDTO = baseDTO with
            {
                Tags = this.MemeTags.Select(mt => mt.CopyDTO()).ToList()
            };

            return baseDTO;
        }

        protected override void CleanUp(bool clean)
        {
            if (!this._disposed)
            {
                if (clean)
                {
                    this.MemeTags.Clear();
                }
            }
            this._disposed = true;
        }

        ~MemeVM()
        {
            CleanUp(false);
        }
    }
}
