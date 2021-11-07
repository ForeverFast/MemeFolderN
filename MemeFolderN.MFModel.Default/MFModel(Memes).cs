using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Extentions;
using MemeFolderN.MFModelBase.Extentions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MemeFolderN.MFModelBase.Wpf
{
    public partial class MFModelWpf : MFModelBase
    {
        protected override async Task<List<MemeDTO>> GetMemesByFolderId(Guid id)
        {
            List<MemeDTO> memesDTO = await memeDataService.GetMemesByFolderId(id);
            return memesDTO.ToList();
        }

        protected override async Task<List<MemeDTO>> GetMemesByTitle(string title)
        {
            List<MemeDTO> memesDTO = await memeDataService.GetMemesByTitle(title);
            return memesDTO;
        }

        protected override async Task<List<MemeDTO>> GetAllMemes()
        {
            List<MemeDTO> memesDTO = await memeDataService.GetAllMemes();
            return memesDTO;
        }

        protected override async Task AddMeme(MemeDTO memeDTO)
        {
            Guid? parentGuid = memeDTO.ParentFolderId;
            string parentFolderPath = parentGuid != null
                ? await GetParentFolderPath(parentGuid)
                : throw new MFModelException($"Для сохранения требуется guid родительского каталога.");

            memeDTO = InitNewMeme(memeDTO, parentFolderPath);

            List<Guid> memeTags = memeDTO.TagGuids != null ? memeDTO.TagGuids : new List<Guid>();
            
            MemeDTO proccesedMemeDTO = memeDTO with { Tags = null };

            MemeDTO createdMeme = await memeDataService.Add(proccesedMemeDTO);
            if (createdMeme != null)
            {

                await memeTagNodeDataService.AddRange(createdMeme.Id, memeTags);
                
                OnAddMemesEvent(new List<MemeDTO>() { createdMeme });
            }
            else
                throw new MFModelException($"Экзмпляр {memeDTO.Title} не удалось сохранить.", MFModelExceptionEnum.NotSaved);
        }

        protected override async Task AddRangeMemes(List<MemeDTO> memesDTO)
        {
            //List<MemeDTO> processedMemesDTO = new List<MemeDTO>();
            //List<Task<MemeDTO>> InitTasks = new List<Task<MemeDTO>>();

            //memesDTO.ForEach(m =>
            //{
            //    Task<MemeDTO> InitTask = InitNewMeme(m);
            //    InitTasks.Add(InitTask);
            //    InitTask.Start();
            //});

            //await Task.WhenAll(InitTasks);

            //await memeDataService.AddRangeMemes(processedMemesDTO);

            //OnAddMemesEvent(processedMemesDTO);

            await Task.Delay(1000);
        }

        protected override async Task ChangeMeme(MemeDTO memeDTO)
        {
            MemeDTO oldMemeData = await memeDataService.GetById(memeDTO.Id);

            List<Guid> oldMemeTagForRemove = oldMemeData.TagGuids.Except(memeDTO.TagGuids).ToList();
            await memeTagNodeDataService.DeleteRange(oldMemeData.Id, oldMemeTagForRemove);
           
            List<Guid> newMemeTagForAdd = memeDTO.TagGuids.Except(oldMemeData.TagGuids).ToList();
            await memeTagNodeDataService.AddRange(oldMemeData.Id, newMemeTagForAdd);

            MemeDTO updatedMeme = await memeDataService.Update(memeDTO.Id, memeDTO);
            if (updatedMeme != null)
            {
                OnChangedMemesEvent(new List<MemeDTO>() { updatedMeme });
            }
            else
                throw new MFModelException($"Экзмпляр {memeDTO.Title} не удалось обновить.", MFModelExceptionEnum.NotUpdated);
        }

        protected override async Task DeleteMeme(MemeDTO memeDTO)
        {
            bool result = await memeDataService.Delete(memeDTO.Id);
            if (result)
            {
                OnRemoveMemesEvent(new List<MemeDTO>() { memeDTO });
            }
            else
                throw new MFModelException($"Экзмпляр {memeDTO.Title} не удалось удалить.", MFModelExceptionEnum.NotDeleted);
        }

        protected override async Task DeleteRangeMemes(List<MemeDTO> memesDTO)
        {
            List<MemeDTO> deletedMemes = await memeDataService.DeleteRangeMemes(memesDTO);
            List<MemeDTO> notDeletedMemes = memesDTO.Except(deletedMemes).ToList();

            if (deletedMemes.Count > 0)
            {
                OnRemoveMemesEvent(memesDTO);
            }
            
            if (notDeletedMemes.Count > 0)
            {
                string errorMessage = "Экзмпляры:\r\n";
                memesDTO.ForEach(m => errorMessage += $"{m.Title}\r\n");
                throw new MFModelException($"{errorMessage} не удалось удалить.", MFModelExceptionEnum.NotDeleted);
            }

        }

        protected override async Task DeleteMemeTagFromMeme(Guid memeGuid, Guid tagGuid)
        {
            MemeDTO memeDTO = await memeTagNodeDataService.Delete(memeGuid, tagGuid);
            if (memeDTO != null)
            {
                OnChangedMemesEvent(new List<MemeDTO> { memeDTO });
            }
            else
                throw new MFModelException($"Не удалось удалить тег.", MFModelExceptionEnum.NotDeleted);
        }

        #region Вспомогательные методы

        /// <summary>
        /// Метод для обработки новой записи Meme
        /// </summary>
        /// <param name="memeDTO"></param>
        /// <returns></returns>
        protected MemeDTO InitNewMeme(MemeDTO memeDTO, string parentFolderPath)
        {
            string newImagePath = string.Empty;
            if (!string.IsNullOrEmpty(memeDTO.Title))
                newImagePath = ExplorerHelper.CreateNewImage(parentFolderPath, memeDTO.ImagePath, memeDTO.Title);
            else
                newImagePath = ExplorerHelper.CreateNewImage(parentFolderPath, memeDTO.ImagePath);

            string newMiniImagePath= ExplorerHelper.CreateNewMiniImageForNewImage(parentFolderPath, newImagePath);
            
            memeDTO = memeDTO with
            {
                Title = Path.GetFileNameWithoutExtension(newImagePath),
                ImagePath = newImagePath,
                MiniImagePath = newMiniImagePath
            };

            return memeDTO;
        }

        #endregion
    }
}


// OLD DATA (DO NOT REMOVE) 

//foreach (MemeTagDTO memeTag in oldMemeData.Tags.ToArray())
//{
//    bool flag = true;
//    foreach (MemeTagDTO newMemeTag in memeDTO.Tags)
//    {
//        if (memeTag.Id == newMemeTag.Id)
//        {
//            flag = false;
//            break;
//        }
//    }

//    if (flag)
//    {
//        memeTagNodeDataService.Delete(memeTag.Id);
//        oldMemeData.Tags.Remove(memeTag);
//    }
//}


//foreach (MemeTagNodeDTO newMemeTagNode in memeDTO.Tags.ToArray())
//{
//    bool flag = true;
//    foreach (MemeTagNodeDTO memeTagNode in oldMemeData.Tags)
//    {
//        if (memeTagNode.MemeTag.Id == newMemeTagNode.MemeTag.Id)
//        {
//            flag = false;
//            break;
//        }
//    }

//    if (flag)
//    {
//        MemeTagNodeDTO dbCreatedMemeTagNode = memeTagNodeDataService.Add(newMemeTagNode).Result;
//        DataExtentions.ReplaceReference(memeDTO.Tags, dbCreatedMemeTagNode, mtn => mtn == newMemeTagNode);
//    }

//}