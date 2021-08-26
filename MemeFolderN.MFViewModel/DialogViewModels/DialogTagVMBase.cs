using MemeFolderN.MFViewModelsBase.BaseViewModels;

namespace MemeFolderN.MFViewModelsBase.DialogViewModels
{
    public class DialogTagVMBase : BaseDialogViewModel
    {
        //#region Поля
        //private MemeTag _model;
        //#endregion

        //public MemeTag Model { get => _model; set => SetProperty(ref _model, value); }

        //#region Конструкторы
        //public DialogTagVMBase(MemeTag model,
        //                   string dialogTitle) : base()
        //{
        //    Model = model;

        //    DialogTitle = dialogTitle;
        //}

        //#endregion
        public DialogTagVMBase(string dialogTitle) : base(dialogTitle)
        {
        }
    }
}

