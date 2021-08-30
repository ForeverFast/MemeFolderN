using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModels.Default.Services;

namespace MemeFolderN.MFViewModels.Default.MethodCommands
{
    public class MemeTagMethodCommandsClass : IMemeTagMethodCommandsClass
    {
        private readonly IDialogService dialogService;
        private readonly IMFModel model;

        public MemeTagMethodCommandsClass(IDialogService dialogService, IMFModel model)
        {
            this.dialogService = dialogService;
            this.model = model;
        }

        public virtual async void MemeTagAddMethodAsync()
        {
            MemeTagDTO notSavedMemeTagDTO = await dialogService.MemeTagDtoOpenAddDialog();
            if (notSavedMemeTagDTO != null)
                await model.AddMemeTagAsync(notSavedMemeTagDTO);
        }

        public virtual async void MemeTagChangeMethodAsync(MemeTagDTO memeTagDTO)
        {
            MemeTagDTO notSavedEditedMemeTagDTO = await dialogService.MemeTagDtoOpenEditDialog(memeTagDTO);
            if (notSavedEditedMemeTagDTO != null)
                await model.ChangeMemeTagAsync(notSavedEditedMemeTagDTO);
        }

        public virtual async void MemeTagDeleteMethodAsync(MemeTagDTO memeTagDTO)
        {
            await model.DeleteMemeTagAsync(memeTagDTO);
        }
    }
}
