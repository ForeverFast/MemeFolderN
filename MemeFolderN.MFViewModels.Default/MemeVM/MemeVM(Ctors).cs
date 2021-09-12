using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFViewModelsBase;
using System.Linq;

namespace MemeFolderN.MFViewModels.Default
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
    }
}
