using MemeFolderN.EntityFramework.Services;

namespace MemeFolderN.MFModelBase.Default
{
    public partial class MFModel : MFModelBase
    {
        public MFModel(IFolderDataService folderDataService,
            IMemeDataService memeDataService,
            IMemeTagDataService memeTagDataService,
            IMemeTagNodeDataService memeTagNodeDataService) : base(folderDataService, memeDataService, memeTagDataService, memeTagNodeDataService)
        {

        }
    }
}
