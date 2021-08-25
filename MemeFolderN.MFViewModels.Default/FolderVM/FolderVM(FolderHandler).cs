using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Extentions;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default.FolderVM
{
    public partial class FolderVM : FolderVMBase
    {
        /// <summary>Обработчик события изменения в папке</summary>
        /// <param name="sender">Источник события</param>
        /// <param name="action">Действие события</param>
        /// <param name="foldersDTO">Папки затронутые событием</param>
        private void Model_ChangedFoldersEvent(object sender, ActionType action, List<FolderDTO> foldersDTO)
        {
            IEnumerable<FolderDTO> sortedFolders = foldersDTO.Where(f => f.ParentFolderId != this.ParentFolderId);

            switch (action)
            {
                case ActionType.Add:
                    if (sortedFolders.Any())
                        Task.Factory.StartNew(FoldersAdd, sortedFolders);
                    break;
                case ActionType.Changed:
                    if (sortedFolders.Any())
                        Task.Factory.StartNew(FoldersChanged, sortedFolders);
                    break;
                case ActionType.Remove:
                    if (sortedFolders.Any())
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

            /// Создание коллекции добавляемых Папок
            List<FolderVM> list = new List<FolderVM>(folders.Count);

            /// Цикл по полученной коллекции
            foreach (FolderDTO folder in folders.ToArray())
            {
                /// Если в имеющейся коллекции нет папки с таким ID
                if (Folders.All(r => r.Id != folder.Id))
                {
                    /// Создание новой папки для добавления в коллекцию
                    FolderVM newFolderVM = new FolderVM(_navigationService, model, dispatcher);
                    newFolderVM.CopyFromDTO(folder);
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
            lock (Folders)
                foreach (FolderVM folder in folders)
                    Folders.Add(folder);
        }



        /// <summary>Изменение Комнат</summary>
        /// <param name="state">Изменяемые Комнаты</param>
        private void FoldersChanged(object state)
        {
            /// Получение коллекции из параметра
            List<FolderDTO> folders = (List<FolderDTO>)state;

            /// Создание коллекции изменяемых Комнат
            Dictionary<FolderDTO, FolderVM> list = new Dictionary<FolderDTO, FolderVM>(folders.Count);

            /// Цикл по полученной коллекции
            foreach (FolderDTO folder in folders.ToArray())
            {
                /// Если в имеющейся коллекции есть Комната с таким ID
                FolderVM fvm = (FolderVM)Folders.FirstOrDefault(r => r.Id == folder.Id);
                if (fvm != null)
                {
                    /// Создание новой пары Данные и Комната для изменения в коллекции
                    list.Add(folder, fvm);
                    /// Удаление Комнаты из полученной коллекции
                    folders.Remove(folder);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<Dictionary<FolderDTO, FolderVM>>)FoldersChangedUI, list);

        }

        /// <summary>Метод изменяющий Комнаты в коллекции  для представления</summary>
        /// <param name="folders">DTO тип с новыми данными и Комната</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void FoldersChangedUI(Dictionary<FolderDTO, FolderVM> folders)
        {
            lock (Folders)
                foreach (var folder in folders)
                    folder.Value.CopyFromDTO(folder.Key);
        }

        /// <summary>Удаление Комнат</summary>
        /// <param name="state">Удаляемые Комнаты</param>
        private void FoldersRemove(object state)
        {
            List<FolderDTO> folders = (List<FolderDTO>)state;

            /// Создание коллекции добавляемых папрк
            List<FolderVM> list = new List<FolderVM>(folders.Count);

            /// Цикл по полученной коллекции
            foreach (FolderDTO folder in folders.ToArray())
            {
                /// Если в имеющейся коллекции есть комната с таким ID
                FolderVM rvm = (FolderVM)Folders.FirstOrDefault(r => r.Id == folder.Id);
                if (rvm != null)
                {
                    /// Добавление Комнаты для удаления из коллекции
                    list.Add(rvm);
                    /// Удаление Комнаты из полученной коллекции
                    folders.Remove(folder);
                }
            }

            /// Если в добавляемой коллекции есть элементы
            if (list.Count > 0)
                /// Вызов метода добавления в коллекцию в потоке UI
                dispatcher.BeginInvoke((Action<List<FolderVM>>)FoldersRemoveUI, list);

        }

        /// <summary>Метод удаляющий Комнаты в коллекции  для представления</summary>
        /// <param name="folders">Удаляемые Комнаты</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void FoldersRemoveUI(List<FolderVM> folders)
        {
            lock (Folders)
                foreach (FolderVM folder in folders)
                    Folders.Remove(folder);
        }
    }
}
