using MemeFolderN.EntityFramework.Services;
using MemeFolderN.MFModel.Base;

namespace MemeFolderN.MFModel.MFModel
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
