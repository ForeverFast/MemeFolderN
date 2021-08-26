using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModelsBase.BaseViewModels
{
    public abstract class BaseDialogViewModel : OnPropertyChangedClass
    {
        private string _dialogTitle;

        public string DialogTitle { get => _dialogTitle; set => SetProperty(ref _dialogTitle, value); }

        protected BaseDialogViewModel(string dialogTitle)
        {
            DialogTitle = dialogTitle;
        }
    }
}
