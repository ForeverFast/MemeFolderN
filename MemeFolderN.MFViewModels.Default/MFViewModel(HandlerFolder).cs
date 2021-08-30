using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Extentions;
using MemeFolderN.MFViewModelsBase;
using MemeFolderN.MFViewModelsBase.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MFViewModel : MFViewModelBase
    {
        private void Model_ChangedFoldersEvent(object sender, ActionType action, List<FolderDTO> foldersDTO)
        {
            List<FolderDTO> sortedFolders = foldersDTO.Where(f => f.ParentFolderId == null).ToList();

            if (sortedFolders.Any())
                switch (action)
                {
                    case ActionType.Add:
                        Task.Factory.StartNew(FoldersAdd, sortedFolders);
                        break;
                    case ActionType.Changed:
                        Task.Factory.StartNew(FoldersChanged, sortedFolders);
                        break;
                    case ActionType.Remove:
                        Task.Factory.StartNew(FoldersRemove, sortedFolders);
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

            //Tuple<List<FolderDTO>, IFolder> kort = (Tuple<List<FolderDTO>, IFolder>)state;

            /// Создание коллекции добавляемых Папок
            List<FolderVM> list = new List<FolderVM>(folders.Count);

            /// Цикл по полученной коллекции
            foreach (FolderDTO folder in folders.ToArray())
            {
                /// Если в имеющейся коллекции нет папки с таким ID
                if (RootFolders.All(r => r.Id != folder.Id))
                {
                    /// Создание новой папки для добавления в коллекцию
                    FolderVM newFolderVM = new FolderVM(_navigationService, dialogService, model, dispatcher, folder);

                    list.Add(newFolderVM);
                    /// Удаление созданной из полученной коллекции
                    folders.Remove(folder);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<IEnumerable<FolderVM>>)FoldersAddUI, list);
        }

        /// <summary>Метод добавляющий Папки в коллекцию для представления</summary>
        /// <param name="folders">Добавляемые Папки</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void FoldersAddUI(IEnumerable<FolderVM> folders)
        {
            lock (RootFolders)
            {
                foreach (FolderVM folder in folders)
                    RootFolders.Add(folder);
                IsBusy = false;
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
                FolderVM fvm = (FolderVM)RootFolders.FirstOrDefault(r => r.Id == folder.Id);
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

        }

        /// <summary>Метод изменяющий Папки в коллекции для представления</summary>
        /// <param name="folders">DTO тип с новыми данными и Папками</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void FoldersChangedUI(Dictionary<FolderDTO, FolderVM> folders)
        {
            lock (RootFolders)
            {
                foreach (var folder in folders)
                    folder.Value.CopyFromDTO(folder.Key);
                IsBusy = false;
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

            /// Цикл по полученной коллекции
            foreach (FolderDTO folder in folders.ToArray())
            {
                /// Если в имеющейся коллекции есть Папка с таким ID
                FolderVM rvm = (FolderVM)RootFolders.FirstOrDefault(r => r.Id == folder.Id);
                if (rvm != null)
                {
                    /// Добавление Папки для удаления из коллекции
                    list.Add(rvm);
                    /// Удаление Папки из полученной коллекции
                    folders.Remove(folder);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<List<FolderVM>>)FoldersRemoveUI, list);

        }

        /// <summary>Метод удаляющий Папки в коллекции для представления</summary>
        /// <param name="folders">Удаляемые Папки</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void FoldersRemoveUI(List<FolderVM> folders)
        {
            lock (RootFolders)
            {
                foreach (FolderVM folder in folders)
                    RootFolders.Remove(folder);
                IsBusy = false;
            }
               
        }
    }
}
