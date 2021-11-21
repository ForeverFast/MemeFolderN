using MemeFolderN.Data.Services;
using MemeFolderN.MFModel.Common.Abstractions;

namespace MemeFolderN.MFModel.Common
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
