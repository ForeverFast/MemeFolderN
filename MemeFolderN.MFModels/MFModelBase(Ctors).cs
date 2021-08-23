using MemeFolderN.EntityFramework.Services;
using MemeFolderN.MFModelBase.Abstractions;

namespace MemeFolderN.MFModelBase
{
    public partial class MFModelBase : IMFModel, IFolderModel, IMemeModel, IMemeTagModel
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
