using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewExample
{
    public class Node
    {
        public int Id { get; }
        public int ParentId { get; }

        public string Title { get; }

        public Node(int id, int parentId, string title)
        {
            Id = id;
            ParentId = parentId;
            Title = title;
        }

        public static ObservableCollection<Node> Nodes { get; }
            = new ObservableCollection<Node>()
            {
                new Node(1, 0, "Первый"),
                new Node(2, 0, "Второй"),
                new Node(11, 1, "Третий"),
                new Node(12, 1, "Четвёртый"),
                new Node(13, 1, "Пятый"),
                new Node(21, 2, "Шестой"),
                new Node(111, 11, "Седьмой"),
                new Node(121, 12, "Восьмой"),
                new Node(211, 21, "Девятый"),
            };

    }
}
