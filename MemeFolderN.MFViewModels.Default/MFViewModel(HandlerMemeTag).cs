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
        private void Model_ChangedMemeTagsEvent(object sender, ActionType action, List<MemeTagDTO> memeTagsDTO)
        {
            if (memeTagsDTO.Any())
                switch (action)
                {
                    case ActionType.Add:
                        Task.Factory.StartNew(MemeTagsAdd, memeTagsDTO);
                        break;
                    case ActionType.Changed:
                        Task.Factory.StartNew(MemeTagsChanged, memeTagsDTO);
                        break;
                    case ActionType.Remove:
                        Task.Factory.StartNew(MemeTagsRemove, memeTagsDTO);
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

        /// <summary>Добавление Тегов</summary>
        /// <param name="state">Добавляемые Теги</param>
        private void MemeTagsAdd(object state)
        {
            /// Получение коллекции из параметра
            List<MemeTagDTO> memeTags = (List<MemeTagDTO>)state;

            /// Создание коллекции добавляемых Тегов
            List<MemeTagVM> list = new List<MemeTagVM>(memeTags.Count);

            /// Цикл по полученной коллекции
            foreach (MemeTagDTO memeTag in memeTags.ToArray())
            {
                /// Если в имеющейся коллекции нет Тега с таким ID
                if (Folders.All(r => r.Id != memeTag.Id))
                {
                    /// Создание нового Тега для добавления в коллекцию
                    MemeTagVM newMemeTagVM = new MemeTagVM(memeTag);

                    list.Add(newMemeTagVM);
                    /// Удаление созданной из полученной коллекции
                    memeTags.Remove(memeTag);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<IEnumerable<MemeTagVM>>)MemeTagsAddUI, list);
        }

        /// <summary>Метод добавляющий Теги в коллекцию для представления</summary>
        /// <param name="memeTags">Добавляемые Теги</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void MemeTagsAddUI(IEnumerable<MemeTagVM> memeTags)
        {
            lock (MemeTags)
            {
                foreach (MemeTagVM memeTag in memeTags)
                    MemeTags.Add(memeTag);
                IsBusy = false;
            }
        }


        /// <summary>Изменение Тегов</summary>
        /// <param name="state">Изменяемые Теги</param>
        private void MemeTagsChanged(object state)
        {
            /// Получение коллекции из параметра
            List<MemeTagDTO> memeTags = (List<MemeTagDTO>)state;

            /// Создание коллекции изменяемых Тегов
            Dictionary<MemeTagDTO, MemeTagVM> list = new Dictionary<MemeTagDTO, MemeTagVM>(memeTags.Count);

            /// Цикл по полученной коллекции
            foreach (MemeTagDTO memeTag in memeTags.ToArray())
            {
                /// Если в имеющейся коллекции есть Тег с таким ID
                MemeTagVM mtvm = (MemeTagVM)MemeTags.FirstOrDefault(r => r.Id == memeTag.Id);
                if (mtvm != null)
                {
                    /// Создание новой пары Данные и Теги для изменения в коллекции
                    list.Add(memeTag, mtvm);
                    /// Удаление Тега из полученной коллекции
                    memeTags.Remove(memeTag);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<Dictionary<MemeTagDTO, MemeTagVM>>)MemeTagsChangedUI, list);

        }

        /// <summary>Метод изменяющий Теги в коллекции для представления</summary>
        /// <param name="memeTags">DTO тип с новыми данными и Тегами</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void MemeTagsChangedUI(Dictionary<MemeTagDTO, MemeTagVM> memeTags)
        {
            lock (MemeTags)
            {
                foreach (var memeTag in memeTags)
                    memeTag.Value.CopyFromDTO(memeTag.Key);
                IsBusy = false;
            }        
        }


        /// <summary>Удаление Тегов</summary>
        /// <param name="state">Удаляемые Теги</param>
        private void MemeTagsRemove(object state)
        {
            /// Получение коллекции из параметра
            List<MemeTagDTO> memeTags = (List<MemeTagDTO>)state;

            /// Создание коллекции добавляемых Тегов
            List<MemeTagVM> list = new List<MemeTagVM>(memeTags.Count);

            /// Цикл по полученной коллекции
            foreach (MemeTagDTO memeTag in memeTags.ToArray())
            {
                /// Если в имеющейся коллекции есть Тег с таким ID
                MemeTagVM mtvm = (MemeTagVM)MemeTags.FirstOrDefault(r => r.Id == memeTag.Id);
                if (mtvm != null)
                {
                    /// Добавление Тега для удаления из коллекции
                    list.Add(mtvm);
                    /// Удаление Тега из полученной коллекции
                    memeTags.Remove(memeTag);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<List<MemeTagVM>>)MemeTagsRemoveUI, list);

        }

        /// <summary>Метод удаляющий Теги в коллекции для представления</summary>
        /// <param name="memeTags">Удаляемые Теги</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void MemeTagsRemoveUI(List<MemeTagVM> memeTags)
        {
            lock (MemeTags)
            {
                foreach (MemeTagVM memeTag in memeTags)
                    MemeTags.Remove(memeTag);
                IsBusy = false;
            }   
        }
    }
}
