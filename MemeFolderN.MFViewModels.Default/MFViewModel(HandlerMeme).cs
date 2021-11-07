using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Extentions;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MFViewModel : MFViewModelBase
    {
        private void Model_ChangedMemesEvent(object sender, ActionType action, List<MemeDTO> memesDTO)
        {
            if (memesDTO.Any())
                switch (action)
                {
                    case ActionType.Add:
                        Task.Factory.StartNew(MemesAdd, memesDTO);
                        break;
                    case ActionType.Changed:
                        Task.Factory.StartNew(MemesChanged, memesDTO);
                        break;
                    case ActionType.Remove:
                        Task.Factory.StartNew(MemesRemove, memesDTO);
                        break;
                    default:
                        return;
                }
        }


        /// <summary>Добавление Мемов</summary>
        /// <param name="state">Добавляемые Мемы</param>
        private void MemesAdd(object state)
        {
            /// Получение коллекции из параметра
            List<MemeDTO> memes = (List<MemeDTO>)state;

            /// Создание коллекции добавляемых Мемов
            List<MemeVM> list = new List<MemeVM>(memes.Count);

            /// Цикл по полученной коллекции
            foreach (MemeDTO meme in memes.ToArray())
            {
                /// Если в имеющейся коллекции нет Мема с таким ID
                if (Memes.All(r => r.Id != meme.Id))
                {
                    /// Создание нового Мема для добавления в коллекцию
                    MemeVM newMemeVM = new MemeVM(meme);

                    list.Add(newMemeVM);
                    /// Удаление Мема созданного из полученной коллекции
                    memes.Remove(meme);
                }
            }


            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
            {
                List<MemeVM> folderMemeVMs = SelectedFolder == null ? null : folderMemeVMs = list
                    .Where(m => m.ParentFolderId == SelectedFolder.Id && !SelectedFolder.Memes.Any(x => x.Id == m.Id))
                    .ToList();

                List<MemeVM> memeTagMemeVMs = SelectedMemeTag == null ? null : folderMemeVMs = list
                    .Where(m => m.MemeTags.Any(mt => mt.Id == SelectedMemeTag.Id) && !SelectedMemeTag.Memes.Any(x => x.Id == m.Id))
                    .ToList();

                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<List<MemeVM>, List<MemeVM>, List<MemeVM>>)MemesAddUI, list, folderMemeVMs, memeTagMemeVMs);
            }
            else
            {
                IsMemesLoadedFlag = true;
                BusyCheck();
            }
        }

        /// <summary>Метод добавляющий Мемы в коллекцию для представления</summary>
        /// <param name="memes">Добавляемые Мемы</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void MemesAddUI(List<MemeVM> memes, List<MemeVM> folderMemeVMs, List<MemeVM> memeTagMemeVMs)
        {
            lock (Memes)
            {
                memes.ForEach(m => Memes.Add(m));

                if (folderMemeVMs != null)
                    folderMemeVMs.ForEach(m => SelectedFolder.Memes.Add(m));
                if (memeTagMemeVMs != null)
                    memeTagMemeVMs.ForEach(m => SelectedMemeTag.Memes.Add(m));

                IsMemesLoadedFlag = true;
                BusyCheck();
            }
        }


        /// <summary>Изменение Мемов</summary>
        /// <param name="state">Изменяемые Мемы</param>
        private void MemesChanged(object state)
        {
            /// Получение коллекции из параметра
            List<MemeDTO> memes = (List<MemeDTO>)state;

            /// Создание коллекции изменяемых Мемов
            Dictionary<MemeDTO, MemeVM> list = new Dictionary<MemeDTO, MemeVM>(memes.Count);

            List<MemeVM> folderMemeVMsForRemove = new List<MemeVM>();
            List<MemeVM> folderMemeVMsForAdd = new List<MemeVM>();

            List<MemeVM> memeTagMemeVMsForRemove = new List<MemeVM>();
            List<MemeVM> memeTagMemeVMsForAdd = new List<MemeVM>();

            /// Цикл по полученной коллекции
            foreach (MemeDTO meme in memes.ToArray())
            {
                /// Если в имеющейся коллекции есть Мем с таким ID
                MemeVM mvm = (MemeVM)Memes.FirstOrDefault(r => r.Id == meme.Id);
                if (mvm != null)
                {
                    /// Создание новой пары Данные и Мем для изменения в коллекции
                    list.Add(meme, mvm);

                    if (SelectedFolder != null)
                        if (mvm.MemeTags.Any(mt => mt.Id == SelectedFolder.Id) && !meme.TagGuids.Any(tg => tg == SelectedFolder.Id))
                        {
                            folderMemeVMsForRemove.Add(mvm);
                        }
                        else if (!mvm.MemeTags.Any(mt => mt.Id == SelectedFolder.Id) && meme.TagGuids.Any(tg => tg == SelectedFolder.Id))
                        {
                            folderMemeVMsForAdd.Add(mvm);
                        }

                    if (SelectedMemeTag != null)
                        if (mvm.MemeTags.Any(mt => mt.Id == SelectedMemeTag.Id) && !meme.TagGuids.Any(tg => tg == SelectedMemeTag.Id))
                        {
                            memeTagMemeVMsForRemove.Add(mvm);
                        }
                        else if (!mvm.MemeTags.Any(mt => mt.Id == SelectedMemeTag.Id) && meme.TagGuids.Any(tg => tg == SelectedMemeTag.Id))
                        {
                            memeTagMemeVMsForAdd.Add(mvm);
                        }

                    /// Удаление Мема из полученной коллекции
                    memes.Remove(meme);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
            {
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<Dictionary<MemeDTO, MemeVM>,
                    List<MemeVM>,
                    List<MemeVM>,
                    List<MemeVM>,
                    List<MemeVM>>)MemesChangedUI,
                    list,
                    folderMemeVMsForRemove,
                    folderMemeVMsForAdd,
                    memeTagMemeVMsForRemove,
                    memeTagMemeVMsForAdd);
            }
            else
            {
                IsMemesLoadedFlag = true;
                BusyCheck();
            }

        }

        /// <summary>Метод изменяющий Мемы в коллекции для представления</summary>
        /// <param name="memes">DTO тип с новыми данными и Мемами</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void MemesChangedUI(Dictionary<MemeDTO, MemeVM> memes,
            List<MemeVM> folderMemeVMsForRemove,
            List<MemeVM> folderMemeVMsForAdd,
            List<MemeVM> memeTagMemeVMsForRemove,
            List<MemeVM> memeTagMemeVMsForAdd)
        {
            lock (Memes)
            {
                foreach (var meme in memes)
                    meme.Value.CopyFromDTO(meme.Key);

                if (SelectedFolder != null)
                {
                    folderMemeVMsForRemove.ForEach(m => SelectedFolder.Memes.Remove(m));
                    folderMemeVMsForAdd.ForEach(m => SelectedFolder.Memes.Add(m));
                }

                if (SelectedMemeTag != null)
                {
                    memeTagMemeVMsForRemove.ForEach(m => SelectedMemeTag.Memes.Remove(m));
                    memeTagMemeVMsForAdd.ForEach(m => SelectedMemeTag.Memes.Add(m));
                }

                IsMemesLoadedFlag = true;
                BusyCheck();
            }   
        }


        /// <summary>Удаление Мемов</summary>
        /// <param name="state">Удаляемые Мемы</param>
        private void MemesRemove(object state)
        {
            /// Получение коллекции из параметра
            List<MemeDTO> memes = (List<MemeDTO>)state;

            /// Создание коллекции добавляемых Мемов
            List<MemeVM> list = new List<MemeVM>(memes.Count);

            /// Цикл по полученной коллекции
            foreach (MemeDTO meme in memes.ToArray())
            {
                /// Если в имеющейся коллекции есть Мем с таким ID
                MemeVM rvm = (MemeVM)Memes.FirstOrDefault(r => r.Id == meme.Id);
                if (rvm != null)
                {
                    /// Добавление Мема для удаления из коллекции
                    list.Add(rvm);
                    /// Удаление Мема из полученной коллекции
                    memes.Remove(meme);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
            {
                List<MemeVM> folderMemeVMs = SelectedFolder == null ? null : list.Where(x => x.ParentFolderId == SelectedFolder.Id).ToList();
                List<MemeVM> memeTagMemeVMs = SelectedMemeTag == null ? null : list.Where(x => x.MemeTags.Any(mt => mt.Id == SelectedMemeTag.Id)).ToList();

                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<List<MemeVM>, List<MemeVM>, List<MemeVM>>)MemesRemoveUI, list, folderMemeVMs, memeTagMemeVMs);
            }             
            else
            {
                IsMemesLoadedFlag = true;
                BusyCheck();
            }
                

        }

        /// <summary>Метод удаляющий Мемы в коллекции для представления</summary>
        /// <param name="memes">Удаляемые Мемы</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void MemesRemoveUI(List<MemeVM> memes, List<MemeVM> folderMemeVMs, List<MemeVM> memeTagMemeVMs)
        {
            lock (Memes)
            {
                if (folderMemeVMs != null)
                    folderMemeVMs.ForEach(m => SelectedFolder.Memes.Remove(m));
                if (memeTagMemeVMs != null)
                    memeTagMemeVMs.ForEach(m => SelectedMemeTag.Memes.Remove(m));

                memes.ForEach(m =>
                {
                    Memes.Remove(m);
                    m.Dispose();
                });
                
                IsMemesLoadedFlag = true;
                BusyCheck();
            }
        }
    }
}
