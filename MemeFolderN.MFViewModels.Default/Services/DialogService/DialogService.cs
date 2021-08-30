using MemeFolderN.MFViewModelsBase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFViewModels.Default.Services
{
    public class DialogService : DialogServiceBase, IDialogService
    {
        public DialogService(ShowDialogDelegete showDialogDelegete) : base(showDialogDelegete)
        {

        }
    }
}
