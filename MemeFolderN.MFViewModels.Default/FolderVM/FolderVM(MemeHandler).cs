using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Extentions;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class FolderVM : FolderVMBase
    {
        private void Model_ChangedMemesEvent(object sender, ActionType action, List<MemeDTO> memesDTO)
        {
            IEnumerable<MemeDTO> sortedMemes = memesDTO.Where(m => m.ParentFolderId != this.ParentFolderId);

            if (sortedMemes.Any())
                switch (action)
                {
                    case ActionType.Add:
                        Task.Factory.StartNew(MemesAdd, sortedMemes);
                        break;
                    case ActionType.Remove:
                        Task.Factory.StartNew(MemesRemove, sortedMemes);
                        break;
                    default:
#if DEBUG
                        ShowMetod($"Какой-то баг {sender}");
#else
                        throw new Exception($"Какой-то баг {sender}");
#endif
                        break;
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
                    MemeVM newMemeVM = new MemeVM(_navigationService, model, dispatcher);
                    newMemeVM.CopyFromDTO(meme);
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
