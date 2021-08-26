using System.Threading.Tasks;

namespace MemeFolderN.MFViewModelsBase.Services
{
    public delegate Task<object?> ShowDialogDelegete(object dialogFolderVM, object dialogId);

    public interface IDialogServiceBase
    {
        void ShowMessage(string text);
        void ShowFolder(string path);
        string FileBrowserDialog(string Extension = "*.jpg;*.png");
        string FolderBrowserDialog();
    }
}
