using MemeFolderN.EntityFramework.Services;
using MemeFolderN.Extentions.Services;
using MemeFolderN.MFModel.Wpf.Abstractions;
using MemeFolderN.MFModelBase.Extentions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeFolderN.MFModelBase.Wpf
{
    public partial class MFModelWpf : MFModelBase, IMFModelWpf, IDragDtopLogic
    {
        private readonly IUserSettingsService userSettingsService;
        private readonly IExtentionalDataService extentionalDataService;

        public MFModelWpf(IFolderDataService folderDataService,
            IMemeDataService memeDataService,
            IMemeTagDataService memeTagDataService,
            IMemeTagNodeDataService memeTagNodeDataService,
            IUserSettingsService userSettingsService,
            IExtentionalDataService extentionalDataService) : base(folderDataService, memeDataService, memeTagDataService, memeTagNodeDataService)
        {
            this.userSettingsService = userSettingsService;
            this.extentionalDataService = extentionalDataService;
        }

        #region Вспомогательные методы

        protected async Task<string> GetParentFolderPath(Guid? guid)
        {
            string path = string.Empty;
            if (guid != null && guid != Guid.Empty)
            {
                path = (await folderDataService.GetById((Guid)guid))?.FolderPath;
                if (string.IsNullOrEmpty(path))
                    throw new MFModelException($"Не удалось найти папку для сохранения мема.", MFModelExceptionEnum.NotSaved);
                return path;
            }
            else
                return userSettingsService.RootFolderPath;
        }

        #endregion;
    }
}
