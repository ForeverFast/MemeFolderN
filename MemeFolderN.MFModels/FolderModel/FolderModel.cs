using MemeFolderN.Core.DTOClasses;
using MemeFolderN.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.MFModels
{
    public class FolderModel : FolderModelBase
    {


        protected override void AddFolder(FolderDTO folderDTO)
        {

        }

        protected override void ChangeFolder(FolderDTO folderDTO)
        {
            throw new NotImplementedException();
        }

        protected override void DeleteFolder(FolderDTO folderDTO)
        {
            throw new NotImplementedException();
        }

        protected override List<FolderDTO> GetFolders(FolderDTO folderDTO)
        {
            throw new NotImplementedException();
        }

        #region Конструкторы

        public FolderModel(IFolderDataService folderDataService) : base(folderDataService)
        {

        }

        #endregion
    }
}
