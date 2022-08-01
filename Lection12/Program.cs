using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lection12
{
    public class TreeNode<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
    }

    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private TreeNode<T> _root;

        public void Add(T element)
        {
            if(_root != null)
            {
                Add(element, _root);
            }
            else
            {
                _root = new TreeNode<T> { Value = element };
            }
        }

        private void Add(T element, TreeNode<T> current)
        {
            if(element.CompareTo(current.Value) == -1)
            {
                if(current.Left != null)
                {
                    Add(element, current.Left);
                }
                else
                {
                    current.Left = new TreeNode<T> { Value = element };
                }
            }
            else if(element.CompareTo(current.Value) == 1)
            {
                if (current.Right != null)
                {
                    Add(element, current.Right);
                }
                else
                {
                    current.Right = new TreeNode<T> { Value = element };
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetElement(_root);
        }

        private IEnumerator<T> GetElement(TreeNode<T> node)
        {
            if(node.Left != null)
            {
                var enumerator = GetElement(node.Left);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
            yield return node.Value;
            if(node.Right != null)
            {
                var enumerator = GetElement(node.Right);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }
    }

    public class MyLinkedList<T> : IEnumerable<T>
    {
        private int _count;
        private Node<T> _root;

        public MyLinkedList()
        {
        }

        public void AddFront(T element)
        {
            if(_root != null)
            {
                Node<T> temp = new Node<T> { Value = element, Next = _root };
                _root = temp;
            }
            else
            {
                _root = new Node<T> { Value = element };
            }
        }

        public void AddBack(T element)
        {
            if (_root != null)
            {
                Node<T> temp = _root;
                while(temp.Next != null)
                {
                    temp = temp.Next;
                }

                temp.Next = new Node<T> { Value = element };
            }
            else
            {
                _root = new Node<T> { Value = element };
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> temp = _root;
            while(temp != null)
            {
                yield return temp.Value;
                temp = temp.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class User : IComparable<User>
    {
        public string FirstName { get; set; }
        public DateTime DoB { get; set; }
        public string PhoneNumber { get; set; }

        public int CompareTo(User other)
        {
            var result = DoB.CompareTo(other.DoB);
            if(result == 0)
            {
                result = PhoneNumber.CompareTo(other.PhoneNumber);
            }

            return result;
        }

        public override string ToString()
        {
            return $"{FirstName} {DoB} {PhoneNumber}";
        }
    }

    public class FirstNameUserComparer : IComparer<User>
    {
        private int _coef => IsAscending ? 1 : -1;
        public bool IsAscending { get; set; }
        public FirstNameUserComparer(bool isAscending = true)
        {
            IsAscending = isAscending;
        }

        public int Compare(User x, User y)
        {
            return x.FirstName.CompareTo(y.FirstName) * _coef;
        }
    }

    class Program
    {
        static void Sort(int[] elements, bool ascending = true)
        {
            int coef = ascending ? 1 : -1;
            for (int i = 0; i < elements.Length - 1; i++)
            {
                for (int j = i + 1; j < elements.Length; j++)
                {
                    if(elements[i].CompareTo(elements[j]) == coef)
                    {
                        Swap(ref elements[i], ref elements[j]);
                    }
                }
            }
        }

        private static void Swap(ref int a, ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }

        static void Demo1()
        {
            int[] items = new int[10];
            Random random = new Random();
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = random.Next(100);
                Console.Write($"\t{items[i]} ");
            }

            Console.WriteLine();
            Sort(items, true);

            for (int i = 0; i < items.Length; i++)
            {
                Console.Write($"\t{items[i]} ");
            }
        }

        static void Demo2()
        {
            var data = DateTime.Now;
            User a = new User
            {
                DoB = data,
                FirstName = "badaw",
                PhoneNumber = "131234"
            };
            User b = new User
            {
                DoB = data.Subtract(TimeSpan.FromSeconds(100)),
                FirstName = "qqqqq",
                PhoneNumber = "555555"
            };
            User c = new User
            {
                DoB = data,
                FirstName = "asdasda",
                PhoneNumber = "131231"
            };

            User[] users = new User[3];
            users[0] = a;
            users[1] = b;
            users[2] = c;
            Array.Sort(users, new FirstNameUserComparer());

            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
        }

        static void Demo3()
        {

            User a = new User
            {
                DoB = DateTime.Now,
                FirstName = "badaw",
                PhoneNumber = "131234"
            };
            User b = new User
            {
                DoB = DateTime.Now.Subtract(TimeSpan.FromSeconds(100)),
                FirstName = "qqqqq",
                PhoneNumber = "555555"
            };
            var result = Max(a, b);
            Console.WriteLine(result);
            var max = Max(10, 20);
            var max2 = Max(10.5, 10.7);
            Max(10, 10.5);
            Max('c', 10);
            Console.WriteLine(max);
            Console.WriteLine(max2);
        }

        static void Demo4()
        {
            MyLinkedList<int> items = new MyLinkedList<int>();
            items.AddFront(10);
            items.AddFront(20);
            items.AddFront(30);
            items.AddBack(40);
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        static void Demo5()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            Random random = new Random();
            var items = Enumerable.Range(1, 10)
                .Select(x => random.Next(10))
                .Distinct();
            foreach (var item in items)
            {
                tree.Add(item);
            }

            foreach (var item in tree)
            {
                Console.WriteLine(item);
            }
        }

        static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) == 1 ? a : b;
        }

        static void Main(string[] args)
        {
            HashSet<int> hs = new HashSet<int>();
            hs.Add(10);
            Dictionary<int, User> users = new Dictionary<int, User>();
            users.Add(10, new User
            {
                DoB = DateTime.Now,
                FirstName = "Vasya",
                PhoneNumber = "123456"
            });
            users.Add(20, new User
            {
                DoB = DateTime.Now.Subtract(TimeSpan.FromSeconds(100)),
                FirstName = "Petya",
                PhoneNumber = "53242"
            });

            if (users.ContainsKey(10))
            {
                Console.WriteLine(users[10]);
            }

            if(users.TryGetValue(20, out var user))
            {
                Console.WriteLine(user);
            }
            if(users.TryAdd(30, new User()))
            {

            }

            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.AddLast(10);
            linkedList.AddFirst(20);
            linkedList.AddLast(15);
            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }
        }
    }
}
