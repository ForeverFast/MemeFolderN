using MemeFolderN.MFViewModelsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MemeVM : MemeVMBase
    {
        public bool IsMemeTagsLoaded { get => _isMemeTagsLoaded; set => SetProperty(ref _isMemeTagsLoaded, value); }

        #region Поля для хранения значений свойств
        private bool _isMemeTagsLoaded;
        #endregion
    }
}
