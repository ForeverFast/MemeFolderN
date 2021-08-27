using MemeFolderN.Core.DTOClasses;
using MemeFolderN.MFModelBase.Abstractions;
using MemeFolderN.MFViewModels.Default.Services;
using MemeFolderN.MFViewModelsBase;
using MemeFolderN.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MemeVM : MemeVMBase
    {
        /// <summary>Диспетчер UI потока</summary>
        private readonly Dispatcher dispatcher;
        private readonly IDialogService dialogService;

        public MemeVM(INavigationService navigationService,
            IDialogService dialogService,
            IMFModel model,
            Dispatcher dispatcher) : base(navigationService, model)
        {
            this.dispatcher = dispatcher;
            this.dialogService = dialogService;

            model.ChangedMemeTagsEvent += Model_ChangedMemeTagsEvent;
        }

       

        public MemeVM(INavigationService navigationService,
            IDialogService dialogService,
            IMFModel model,
            Dispatcher dispatcher,
            MemeDTO memeDTO) : this(navigationService, dialogService, model, dispatcher)
        {
            this.CopyFromDTO(memeDTO);
        }

        public override void CopyFromDTO(MemeDTO dto)
        {
            base.CopyFromDTO(dto);

            if (IsMemeTagsLoaded)
                return;

            if (dto.Tags?.Count > 0)
            {
                lock (Tags)
                {
                    foreach (MemeTagDTO memeTag in dto.Tags)
                        Tags.Add(new MemeTagVM(memeTag));

                    IsBusy = false;
                    IsLoaded = IsMemeTagsLoaded = true;
                }
            }
        }

        public override MemeDTO CopyDTO()
        {
            MemeDTO baseDTO = base.CopyDTO();

            baseDTO = baseDTO with
            {
                Tags = this.Tags.Select(mt => mt.CopyDTO()).ToList()
            };

            return baseDTO;
        }

        public override void Dispose()
        {
            
        }
    }
}
