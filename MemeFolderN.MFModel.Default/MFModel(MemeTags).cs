using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeFolderN.MFModelBase.Wpf
{
    public partial class MFModelWpf : MFModelBase
    {
        protected override async Task<List<MemeTagDTO>> GetAllMemeTags()
        {
            List<MemeTagDTO> memeTagsDTO = await memeTagDataService.GetTags();
            return memeTagsDTO;
        }

        protected override async Task<List<Guid>> GetAllMemeIdByMemeTagId(Guid id)
        {
            return await memeTagNodeDataService.GetAllMemeIdByMemeTagId(id);
        }

        protected override async Task<List<MemeTagDTO>> GetMemeTagsByMemeId(Guid id)
        {
            List<MemeTagDTO> memeTagsDTO = await memeTagDataService.GetTagsByMemeId(id);
            return memeTagsDTO;
        }

        protected override async Task AddMemeTag(MemeTagDTO memeTagDTO)
        {
            MemeTagDTO createdMemeTag = await memeTagDataService.Add(memeTagDTO);
            if (createdMemeTag != null)
            {
                OnAddMemeTagsEvent(new List<MemeTagDTO>() { createdMemeTag });
            }
            else
                throw new MFModelException($"Экзмпляр {memeTagDTO.Title} не удалось сохранить.", MFModelExceptionEnum.NotSaved);
        }
        protected override async Task ChangeMemeTag(MemeTagDTO memeTagDTO)
        {
            MemeTagDTO updatedMemeTag = await memeTagDataService.Update(memeTagDTO.Id, memeTagDTO);
            if (updatedMemeTag != null)
            {
                OnChangedMemeTagsEvent(new List<MemeTagDTO>() { updatedMemeTag });
            }
            else
                throw new MFModelException($"Экзмпляр {memeTagDTO.Title} не удалось обновить.", MFModelExceptionEnum.NotUpdated);
        }

        protected override async Task DeleteMemeTag(MemeTagDTO memeTagDTO)
        {
            bool result = await memeTagDataService.Delete(memeTagDTO.Id);
            if (result)
            {
                OnRemoveMemeTagsEvent(new List<MemeTagDTO>() { memeTagDTO });
            }
            else
                throw new MFModelException($"Экзмпляр {memeTagDTO.Title} не удалось удалить.", MFModelExceptionEnum.NotDeleted);
        }
    }
}
