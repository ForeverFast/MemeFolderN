using MemeFolderN.Common.DTOClasses;
using MemeFolderN.MFModel.Common.Extentions;
using MemeFolderN.MFViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Wpf
{
    public partial class MFViewModel : MFViewModelBase
    {
        private void Model_ChangedFoldersEvent(object sender, ActionType action, List<FolderDTO> foldersDTO)
        {
            if (foldersDTO.Any())
                switch (action)
                {
                    case ActionType.Add:
                        Task.Factory.StartNew(FoldersAdd, foldersDTO);
                        break;
                    case ActionType.Changed:
                        Task.Factory.StartNew(FoldersChanged, foldersDTO);
                        break;
                    case ActionType.Remove:
                        Task.Factory.StartNew(FoldersRemove, foldersDTO);
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

        /// <summary>Добавление Папок</summary>
        /// <param name="state">Добавляемые Папки</param>
        private void FoldersAdd(object state)
        {
            /// Получение коллекции из параметра
            List<FolderDTO> folders = (List<FolderDTO>)state;

            /// Создание коллекции добавляемых Папок
            List<FolderVM> list = new List<FolderVM>(folders.Count);

            /// Цикл по полученной коллекции
            foreach (FolderDTO folder in folders.ToArray())
            {
                /// Если в имеющейся коллекции нет папки с таким ID
                if (Folders.All(r => r.Id != folder.Id))
                {
                    /// Создание новой папки для добавления в коллекцию
                    FolderVM newFolderVM = new FolderVM(folder);

                    list.Add(newFolderVM);
                    /// Удаление созданной из полученной коллекции
                    folders.Remove(folder);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<IEnumerable<FolderVM>>)FoldersAddUI, list);
            else
            {
                IsFoldersLoadedFlag = true;
                BusyCheck();
            }
        }

        /// <summary>Метод добавляющий Папки в коллекцию для представления</summary>
        /// <param name="folders">Добавляемые Папки</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void FoldersAddUI(IEnumerable<FolderVM> folders)
        {
            lock (Folders)
            {
                foreach (FolderVM folder in folders)
                    Folders.Add(folder);
                IsFoldersLoadedFlag = true;
                BusyCheck();
            }  
        }


        /// <summary>Изменение Папок</summary>
        /// <param name="state">Изменяемые Папки</param>
        private void FoldersChanged(object state)
        {
            /// Получение коллекции из параметра
            List<FolderDTO> folders = (List<FolderDTO>)state;

            /// Создание коллекции изменяемых Папок
            Dictionary<FolderDTO, FolderVM> list = new Dictionary<FolderDTO, FolderVM>(folders.Count);

            /// Цикл по полученной коллекции
            foreach (FolderDTO folder in folders.ToArray())
            {
                /// Если в имеющейся коллекции есть Папка с таким ID
                FolderVM fvm = (FolderVM)Folders.FirstOrDefault(r => r.Id == folder.Id);
                if (fvm != null)
                {
                    /// Создание новой пары Данные и Папка для изменения в коллекции
                    list.Add(folder, fvm);
                    /// Удаление Папки из полученной коллекции
                    folders.Remove(folder);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<Dictionary<FolderDTO, FolderVM>>)FoldersChangedUI, list);
            else
            {
                IsFoldersLoadedFlag = true;
                BusyCheck();
            }

        }

        /// <summary>Метод изменяющий Папки в коллекции для представления</summary>
        /// <param name="folders">DTO тип с новыми данными и Папками</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void FoldersChangedUI(Dictionary<FolderDTO, FolderVM> folders)
        {
            lock (Folders)
            {
                foreach (var folder in folders)
                    folder.Value.CopyFromDTO(folder.Key);
                IsFoldersLoadedFlag = true;
                BusyCheck();
            }
        }


        /// <summary>Удаление Папок</summary>
        /// <param name="state">Удаляемые Папки</param>
        private void FoldersRemove(object state)
        {
            /// Получение коллекции из параметра
            List<FolderDTO> folders = (List<FolderDTO>)state;

            /// Создание коллекции добавляемых Папок
            List<FolderVM> list = new List<FolderVM>(folders.Count);

            List<MemeVM> memes = new List<MemeVM>();

            /// Цикл по полученной коллекции
            foreach (FolderDTO folder in folders.ToArray())
            {
                /// Если в имеющейся коллекции есть Папка с таким ID
                FolderVM rvm = (FolderVM)Folders.FirstOrDefault(r => r.Id == folder.Id);
                if (rvm != null)
                {
                    /// Добавление Папки для удаления из коллекции
                    list.Add(rvm);

                    Memes.Where(m => m.ParentFolderId == folder.Id)
                        .ToList()
                        .ForEach(m => memes.Add((MemeVM)m));

                    /// Удаление Папки из полученной коллекции
                    folders.Remove(folder);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
            {
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<List<FolderVM>, List<MemeVM>>)FoldersRemoveUI, list, memes);
            }
            else
            {
                IsFoldersLoadedFlag = true;
                IsMemesLoadedFlag = true;
                BusyCheck();
            }
           

        }

        /// <summary>Метод удаляющий Папки в коллекции для представления</summary>
        /// <param name="folders">Удаляемые Папки</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void FoldersRemoveUI(List<FolderVM> folders, List<MemeVM> memes)
        {
            lock (Folders)
            {
                foreach (FolderVM folder in folders)
                {
                    Folders.Remove(folder);
                    navigationManager.RemoveDataByKey(folder.Id.ToString());
                    folder.Dispose();
                }
                
                IsFoldersLoadedFlag = true;
                BusyCheck();
            }

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
