using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModel.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFModel.MFModel
{
    public partial class MFModel
    {
        protected override List<MemeTagDTO> GetMemeTags()
        {
            IEnumerable<MemeTagDTO> memeTagsDTO = memeTagDataService.GetTags().Result;
            return memeTagsDTO.ToList();
        }

        protected override void AddMemeTag(MemeTagDTO memeTagDTO)
        {
            MemeTagDTO createdMemeTag = memeTagDataService.Add(memeTagDTO).Result;
            if (createdMemeTag != null)
            {
                OnAddMemeTagsEvent(new List<MemeTagDTO>() { createdMemeTag });
            }
            else
                throw new MFModelException($"Экзмпляр {memeTagDTO.Title} не удалось сохранить.", MFModelExceptionEnum.NotSaved);
        }
        protected override void ChangeMemeTag(MemeTagDTO memeTagDTO)
        {
            MemeTagDTO updatedMemeTag = memeTagDataService.Update(memeTagDTO.Id, memeTagDTO).Result;
            if (updatedMemeTag != null)
            {
                OnChangedMemeTagsEvent(new List<MemeTagDTO>() { updatedMemeTag });
            }
            else
                throw new MFModelException($"Экзмпляр {memeTagDTO.Title} не удалось обновить.", MFModelExceptionEnum.NotUpdated);
        }

        protected override void DeleteMemeTag(MemeTagDTO memeTagDTO)
        {
            if (memeTagDataService.Delete(memeTagDTO.Id).Result)
            {
                OnRemoveMemeTagsEvent(new List<MemeTagDTO>() { memeTagDTO });
            }
            else
                throw new MFModelException($"Экзмпляр {memeTagDTO.Title} не удалось удалить.", MFModelExceptionEnum.NotDeleted);
        }
    }
}
