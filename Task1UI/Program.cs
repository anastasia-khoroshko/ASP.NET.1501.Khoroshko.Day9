using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;
using System.Collections;

namespace Task1UI
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> tree=new BinaryTree<int>();
            tree.Add(2);
            tree.Add(3);
            tree.Add(1);
            tree.Add(0);
            tree.Add(7);
            tree.Add(5);
            var t=tree.InOrder();
            IEnumerable<int> s = tree.PreOrder();
            var k = tree.PostOrder();
        }
    }
}
