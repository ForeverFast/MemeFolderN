using MemeFolderN.EntityFramework.Services;
using MemeFolderN.MFModel.Abstractions;

namespace MemeFolderN.MFModel.Base
{
    public partial class MFModelBase : IMFModel
    {
        protected MFModelBase(IFolderDataService folderDataService,
            IMemeDataService memeDataService,
            IMemeTagDataService memeTagDataService,
            IMemeTagNodeDataService memeTagNodeDataService)
        {
            this.folderDataService = folderDataService;
            this.memeDataService = memeDataService;
            this.memeTagDataService = memeTagDataService;
            this.memeTagNodeDataService = memeTagNodeDataService;
        }
    }
}
