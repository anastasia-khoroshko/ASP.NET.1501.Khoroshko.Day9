using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class BinaryTree<TItem>:IEnumerable<TItem> where TItem:IComparable<TItem>
    {
        private class Node<TValue>
        {
            public TValue Value { get; set; }
            public Node<TValue> Left { get; set; }
            public Node<TValue> Right { get; set; }
            public Node(TValue value)
            {
                Value = value;
            }
        }

        private Node<TItem> root;
        public BinaryTree() { }
        public BinaryTree(ICollection<TItem> collection,IComparer<TItem> comparer)
        {
            foreach (TItem elem in collection)
                Add(elem,comparer);
        }

        public void Add(TItem elem, IComparer<TItem> comparer)
        {
            if (elem == null) throw new ArgumentNullException();
            if (comparer == null) comparer = Comparer<TItem>.Default;
            var node = new Node<TItem>(elem);
            if (root == null) 
                root = node;
                
            else
            {
                Node<TItem> current = root, parent = null;
                while(current!=null)
                {
                    parent = current;
                    if (comparer.Compare(elem,current.Value) > 0) current = current.Right;
                    else current = current.Left;
                }

                if (comparer.Compare(elem, parent.Value) > 0) parent.Right=node;
                else parent.Left=node;
            }
        }
        public IEnumerable<TItem> PreOrder()
        {
            if (root == null) yield break;
            var stack = new Stack<Node<TItem>>();
            stack.Push(root);
            while(stack.Count>0)
            {
                var node = stack.Pop();
                yield return node.Value;
                if (node.Left!= null) stack.Push(node.Left);
                if (node.Right != null) stack.Push(node.Right);
            }
        }

        public IEnumerable<TItem> PostOrder()
        {
            if (root == null) yield break;
            var stack = new Stack<Node<TItem>>();
            var node = root;
            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    if (stack.Count > 0 && node.Right == stack.Peek())
                    {
                        stack.Pop();
                        stack.Push(node);
                        node = node.Right;
                    }
                    else { yield return node.Value; node = null; }                    
                }
                else
                {
                    if (node.Right != null) stack.Push(node.Right);
                    stack.Push(node);
                    node = node.Left;
                }

            }
        }

        public IEnumerable<TItem> InOrder()
        {
            if (root == null) yield break;
            var stack = new Stack<Node<TItem>>();
            Node<TItem> node = root;
            while(stack.Count>0 || node!=null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    yield return node.Value;
                    node = node.Right;
                }
                else
                {
                    stack.Push(node);
                    node = node.Left;
                }

            }
        }

        public bool Find<TItem>(TItem elem)
        {
            if (root == null || elem == null) throw new ArgumentException();
            Node<TItem> node = root;
            while(node!=null)
            {
                if (node.Value.Equals(elem)) return true;
                if (Comparer<TItem>.Default.Compare(elem, node.Value) > 0) node = node.Right;
                else node = node.Left;
            }
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return InOrder().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }
}
