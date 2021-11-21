using MemeFolderN.MFViewModelsBase;
using MemeFolderN.MFViewModelsBase.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.DevTools.ViewModels
{
    public class DevViewModel : OnPropertyChangedClass
    {
        public RelayCommand CallGcCommand => _callGcCommand ?? (_callGcCommand =
           new RelayCommandAction(CallGc));

        private void CallGc()
        {
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }


        #region Поля для хранения значений свойств
        private RelayCommand _callGcCommand;
        //private RelayCommand _folderAddCommand;
        //private RelayCommand _folderAddNonParametersCommand;
        //private RelayCommand _folderChangeCommand;
        //private RelayCommand _folderDeleteCommand;
        #endregion

    }
}
