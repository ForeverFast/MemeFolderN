using MemeFolderN.MFViewModels.Wpf;
using System;
using System.Windows.Data;

namespace MemeFolderN.MFViews.Wpf.Extentions
{
    public static class Handlers
    {
        public static FilterEventHandler OnChildrenNodeFilter => (s, e) =>
        {
            TagCollectionViewSource collection = (TagCollectionViewSource)s;
            if (!(collection.Tag is Guid id))
            {
                if (!(collection.Tag is string str && (Guid.TryParse(str, out id) || (str == "null" && Guid.TryParse("00000000-0000-0000-0000-000000000000", out id)))))
                {
                    return;
                }
            }

            if (e.Item is FolderVM node)
            {
                if (id == Guid.Empty)
                    e.Accepted = node.ParentFolderId == null;
                else
                    e.Accepted = node.ParentFolderId == id;
            }
        };
    }
}
