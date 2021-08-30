using MemeFolderN.EntityFramework.Services;
using MemeFolderN.Extentions.Services;

namespace MemeFolderN.MFModelBase.Default
{
    public partial class MFModel : MFModelBase
    {
        private readonly IUserSettingsService userSettingsService;

        public MFModel(IFolderDataService folderDataService,
            IMemeDataService memeDataService,
            IMemeTagDataService memeTagDataService,
            IMemeTagNodeDataService memeTagNodeDataService,
            IUserSettingsService userSettingsService) : base(folderDataService, memeDataService, memeTagDataService, memeTagNodeDataService)
        {
            this.userSettingsService = userSettingsService;
        }
    }
}
