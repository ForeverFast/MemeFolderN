using MemeFolderN.MFViewModels.Default;
using System;
using System.Windows.Data;

namespace MemeFolderN.MFViews.Extentions
{
    public static class Handlers
    {
        public static FilterEventHandler OnChildrenNodeFilter => (s, e) =>
        {
            TagCollectionViewSource collection = (TagCollectionViewSource)s;
            if (!(collection.Tag is Guid id))
            {
                if (!(collection.Tag is string str && Guid.TryParse(str, out id)))
                {
                    return;
                }
            }

            if (e.Item is FolderVM node)
            {
                e.Accepted = node.ParentFolderId == id;
            }
        };
    }
}
