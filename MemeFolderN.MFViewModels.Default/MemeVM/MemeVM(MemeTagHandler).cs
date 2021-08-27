using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Extentions;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MemeVM : MemeVMBase
    {
        private void Model_ChangedMemeTagsEvent(object sender, ActionType action, List<MemeTagDTO> memeTagsDTO)
        {
            if (memeTagsDTO.Any())
                switch (action)
                {
                    case ActionType.Changed:
                        Task.Factory.StartNew(MemeTagsChanged, memeTagsDTO);
                        break;
                    default:
                        return;
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
                MemeTagVM mtvm = (MemeTagVM)Tags.FirstOrDefault(r => r.Id == memeTag.Id);
                if (mtvm != null)
                {
                    /// Создание новой пары Данные и Тег для изменения в коллекции
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
        /// <param name="tags">DTO тип с новыми данными и Тегами</param>
        /// <remarks>Метод должен выполняться в UI потоке</remarks>
        private void MemeTagsChangedUI(Dictionary<MemeTagDTO, MemeTagVM> tags)
        {
            lock (Tags)
                foreach (var tag in tags)
                    tag.Value.CopyFromDTO(tag.Key);
        }
    }
}
