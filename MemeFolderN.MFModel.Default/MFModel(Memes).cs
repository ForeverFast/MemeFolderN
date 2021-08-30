using MemeFolderN.Core.DTOClasses;
using MemeFolderN.Extentions;
using MemeFolderN.MFModelBase.Extentions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;

namespace MemeFolderN.MFModelBase.Default
{
    public partial class MFModel : MFModelBase
    {
        protected override List<MemeDTO> GetMemesByFolderId(Guid id)
        {
            IEnumerable<MemeDTO> memesDTO = memeDataService.GetMemesByFolderId(id).Result;
            return memesDTO.ToList();
        }
        protected override List<MemeDTO> GetMemesByTitle(string title)
        {
            IEnumerable<MemeDTO> memesDTO = memeDataService.GetMemesByTitle(title).Result;
            return memesDTO.ToList();
        }

        protected override void AddMeme(MemeDTO memeDTO)
        {
            memeDTO = MemeAddInit(memeDTO); 

            // Отделение тегов от исходной сущности meme
            List<MemeTagDTO> memeTags = new List<MemeTagDTO>();
            foreach (MemeTagDTO mtn in memeDTO.Tags.ToArray())
                memeTags.Add(mtn);
                
            MemeDTO proccesedMemeDTO = memeDTO with { Tags = null };

            MemeDTO createdMeme = memeDataService.Add(proccesedMemeDTO).Result;
            if (createdMeme != null)
            {
                // Добавление отделённых тегов и сохранение memeTagNodes в БД
                memeTags.ForEach(async (newMemeTag) =>
                {
                    MemeTagNodeDTO proccesedMemeDTO = new MemeTagNodeDTO 
                    {
                        MemeTagId = newMemeTag.Id,
                        MemeId = createdMeme.Id 
                    };
                    MemeTagNodeDTO dbCreatedMemeTagNode = await memeTagNodeDataService.Add(proccesedMemeDTO);
                    createdMeme.Tags.Append(newMemeTag);
                });

                OnAddMemesEvent(new List<MemeDTO>() { createdMeme });
            }
            else
                throw new MFModelException($"Экзмпляр {memeDTO.Title} не удалось сохранить.", MFModelExceptionEnum.NotSaved);
        }

        protected override void AddRangeMemes(List<MemeDTO> memesDTO)
        {
            List<MemeDTO> processedMemesDTO = new List<MemeDTO>();
            foreach (MemeDTO memeDTO in memesDTO)
            {
                try
                {
                    MemeDTO processedMemeDTO = MemeAddInit(memeDTO);
                    processedMemesDTO.Add(processedMemeDTO);
                }
                catch (Exception)
                {
                    
                }
            }

            OnAddMemesEvent(processedMemesDTO);
        }

        protected override void ChangeMeme(MemeDTO memeDTO)
        {
            MemeDTO oldMemeData = memeDataService.GetById(memeDTO.Id).Result;

            List<MemeTagDTO> oldMemeTagForRemove = oldMemeData.Tags.Except(memeDTO.Tags).ToList();
            foreach(MemeTagDTO memeTagDTO in oldMemeTagForRemove)
            {
                MemeTagNodeDTO memeTagNodeDTO = memeTagNodeDataService.GetByMemeIdAndMemeTagId(memeDTO.Id, memeTagDTO.Id).Result;
                memeTagNodeDataService.Delete(memeTagNodeDTO.Id);
            }

            List<MemeTagDTO> newMemeTagForAdd = memeDTO.Tags.Except(oldMemeData.Tags).ToList();
            foreach (MemeTagDTO memeTagDTO in newMemeTagForAdd)
            {
                MemeTagNodeDTO memeTagNodeDTO = new MemeTagNodeDTO
                {
                    MemeTagId = memeTagDTO.Id,
                    MemeId = memeDTO.Id
                };
                memeTagNodeDataService.Add(memeTagNodeDTO);
            }

            MemeDTO updatedMeme = memeDataService.Update(memeDTO.Id, memeDTO).Result;
            if (updatedMeme != null)
            {
                OnChangedMemesEvent(new List<MemeDTO>() { updatedMeme });
            }
            else
                throw new MFModelException($"Экзмпляр {memeDTO.Title} не удалось обновить.", MFModelExceptionEnum.NotUpdated);
        }
        
        protected override void DeleteMeme(MemeDTO memeDTO)
        {
            if (memeDataService.Delete(memeDTO.Id).Result)
            {
                OnRemoveMemesEvent(new List<MemeDTO>() { memeDTO });
            }
            else
                throw new MFModelException($"Экзмпляр {memeDTO.Title} не удалось удалить.", MFModelExceptionEnum.NotDeleted);
        }

        protected override void DeleteRangeMemes(List<MemeDTO> memesDTO)
        {
            if (memeDataService.DeleteRangeMemes(memesDTO).Result)
            {
                OnRemoveMemesEvent(memesDTO);
            }
            else
            {
                string errorMessage = "Экзмпляры:\r\n";
                memesDTO.ForEach(m => errorMessage += $"{m.Title}\r\n");
                throw new MFModelException($"{errorMessage} не удалось удалить.", MFModelExceptionEnum.NotDeleted);
            }
                
        }

        #region Вспомогательные методы

        /// <summary>
        /// Метод для обработки новой записи Meme
        /// </summary>
        /// <param name="memeDTO"></param>
        /// <returns></returns>
        protected MemeDTO MemeAddInit(MemeDTO memeDTO)
        {
            FolderDTO folder = memeDTO.ParentFolder;

            string newMemePath = @$"{folder.FolderPath}\{memeDTO.Title}{Path.GetExtension(memeDTO.ImagePath)}";
            if (File.Exists(newMemePath))
            {
                newMemePath = GetMemeAnotherName(folder.FolderPath, memeDTO.Title, memeDTO.ImagePath);
            }

            File.Copy(memeDTO.ImagePath, newMemePath);
            string newTitle = memeDTO.Title;
            if (string.IsNullOrEmpty(memeDTO.Title))
            {
                newTitle = Path.GetFileNameWithoutExtension(newMemePath);
            }

            // Создание миниатюры
            string newMiniImageMemePath = @$"{folder.FolderPath}\Mini{newTitle}{Path.GetExtension(memeDTO.ImagePath)}";
            Image result = this.ResizeOrigImg(Image.FromFile(newMemePath), 120, 72);
            result.Save(newMiniImageMemePath);
            result.Dispose();

            MemeDTO proccesedMemeDTO = memeDTO with
            {
                Title = newTitle,
                ImagePath = newMemePath,
                MiniImagePath = newMiniImageMemePath
            };

            return proccesedMemeDTO;
        }

        protected string GetMemeAnotherName(string rootPath, string title, string imagePath)
        {
            string newMemePath = string.Empty;
            int num = 1;
            while (true)
            {
                newMemePath = @$"{rootPath}\{title} ({num++}){Path.GetExtension(imagePath)}";
                if (!File.Exists(newMemePath))
                {
                    File.Copy(imagePath, newMemePath);
                    break;
                }
            }

            return newMemePath;
        }

        protected Image ResizeOrigImg(Image image, int nWidth, int nHeight)
        {
            int newWidth, newHeight;
            var coefH = (double)nHeight / (double)image.Height;
            var coefW = (double)nWidth / (double)image.Width;
            if (coefW >= coefH)
            {
                newHeight = (int)(image.Height * coefH);
                newWidth = (int)(image.Width * coefH);
            }
            else
            {
                newHeight = (int)(image.Height * coefW);
                newWidth = (int)(image.Width * coefW);
            }

            Image result = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(result))
            {
                g.CompositingQuality = CompositingQuality.Default;
                g.SmoothingMode = SmoothingMode.Default;
                g.InterpolationMode = InterpolationMode.Default;

                g.DrawImage(image, 0, 0, newWidth, newHeight);
                g.Dispose();
            }
            return result;
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