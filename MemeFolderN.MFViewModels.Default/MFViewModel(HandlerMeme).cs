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
                List<MemeVM> curMemes = new List<MemeVM>();
                if (SelectedFolder != null)
                {
                    curMemes = list.Where(m => m.ParentFolderId == SelectedFolder.Id && !SelectedFolder.Memes.Any(x => x.Id == m.Id))
                        .ToList();
                         
                }

                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<List<MemeVM>, List<MemeVM>>)MemesAddUI, list, curMemes);
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
        private void MemesAddUI(List<MemeVM> memes, List<MemeVM> curMemes)
        {
            lock (Memes)
            {
                foreach (MemeVM meme in memes)
                    Memes.Add(meme);
                curMemes.ForEach(m => SelectedFolder.Memes.Add(m));
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

            /// Цикл по полученной коллекции
            foreach (MemeDTO meme in memes.ToArray())
            {
                /// Если в имеющейся коллекции есть Мем с таким ID
                MemeVM mvm = (MemeVM)Memes.FirstOrDefault(r => r.Id == meme.Id);
                if (mvm != null)
                {
                    /// Создание новой пары Данные и Мем для изменения в коллекции
                    list.Add(meme, mvm);
                    /// Удаление Мема из полученной коллекции
                    memes.Remove(meme);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<Dictionary<MemeDTO, MemeVM>>)MemesChangedUI, list);
            else
            {
                IsMemesLoadedFlag = true;
                BusyCheck();
            }

        }

        /// <summary>Метод изменяющий Мемы в коллекции для представления</summary>
        /// <param name="memes">DTO тип с новыми данными и Мемами</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void MemesChangedUI(Dictionary<MemeDTO, MemeVM> memes)
        {
            lock (Memes)
            {
                foreach (var meme in memes)
                    meme.Value.CopyFromDTO(meme.Key);
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
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<List<MemeVM>>)MemesRemoveUI, list);
            else
            {
                IsMemesLoadedFlag = true;
                BusyCheck();
            }
                

        }

        /// <summary>Метод удаляющий Мемы в коллекции для представления</summary>
        /// <param name="memes">Удаляемые Мемы</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void MemesRemoveUI(List<MemeVM> memes)
        {
            lock (Memes)
            {
                foreach (MemeVM meme in memes)
                {
                    Memes.Remove(meme);
                    meme.Dispose();
                }

                IsMemesLoadedFlag = true;
                BusyCheck();
            }
        }
    }
}
