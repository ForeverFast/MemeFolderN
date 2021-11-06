using GongSolutions.Wpf.DragDrop;
using MemeFolderN.MFViewModelsBase;
using System;
using System.Linq;
using System.Windows;

namespace MemeFolderN.MFViewModels.Default
{
    public partial class MFViewModel : MFViewModelBase, IDropTarget
    {
        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;

            var dataObject = dropInfo.Data as IDataObject;

            dropInfo.Effects = dataObject != null && dataObject.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.Move;
        }

        public async void Drop(IDropInfo dropInfo)
        {
            try
            {
                IsBusy = true;
                IsFoldersLoadedFlag = false;
                IsMemesLoadedFlag = false;


                var dataObject = dropInfo.Data as DataObject;
                if (dataObject != null && dataObject.ContainsFileDropList())
                {
                    var files = dataObject.GetFileDropList().Cast<string>().ToList();
                    await model.AddInputDataAsync(SelectedFolder.CopyDTO(), files);
                }
            }
            catch (Exception ex)
            {
                IsFoldersLoadedFlag = true;
                IsMemesLoadedFlag = true;
                BusyCheck();
                OnException(ex);
            }
        }
    }
}
