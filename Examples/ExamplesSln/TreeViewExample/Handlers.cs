using System.Windows.Data;

namespace TreeViewExample
{
    public static class Handlers
    {
        public static FilterEventHandler OnChildrenNodeFilter => (s, e) =>
        {
            TagCollectionViewSource collection = (TagCollectionViewSource)s;
            if (!(collection.Tag is int id))
            {
                if (!(collection.Tag is string str && int.TryParse(str, out id)))
                {
                    return;
                }
            }

            if (e.Item is Node node)
            {
                e.Accepted = node.ParentId == id;
            }
        };
    }
}
