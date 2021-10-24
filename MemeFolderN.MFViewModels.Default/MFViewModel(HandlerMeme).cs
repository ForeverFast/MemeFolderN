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
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<IEnumerable<MemeVM>>)MemesAddUI, list);
        }

        /// <summary>Метод добавляющий Мемы в коллекцию для представления</summary>
        /// <param name="memes">Добавляемые Мемы</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void MemesAddUI(IEnumerable<MemeVM> memes)
        {
            lock (Memes)
            {
                foreach (MemeVM meme in memes)
                    Memes.Add(meme);
                IsBusy = false;
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

        }

        /// <summary>Метод изменяющий Мемы в коллекции для представления</summary>
        /// <param name="memes">DTO тип с новыми данными и Мемами</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void MemesChangedUI(Dictionary<MemeDTO, MemeVM> memes)
        {
            lock (Memes)
                foreach (var meme in memes)
                    meme.Value.CopyFromDTO(meme.Key);
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

        }

        /// <summary>Метод удаляющий Мемы в коллекции для представления</summary>
        /// <param name="memes">Удаляемые Мемы</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void MemesRemoveUI(List<MemeVM> memes)
        {
            lock (Memes)
            {
                foreach (MemeVM meme in memes)
                    Memes.Remove(meme);
                IsBusy = false;
            }
        }
    }
}
