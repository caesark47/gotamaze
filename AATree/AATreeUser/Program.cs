using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AATree;

namespace AATreeUser {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("test");
            var tree = AATreeFactory<int>.newAATree();
            //tree = tree.insert(10);

            var rand = new Random();
            for(int i=0;i<30;i++){
                tree = tree.insert(rand.Next(99));
            }


            Console.Write(tree.disp("t"));//ツリー？表示

            Console.WriteLine(tree.disp("s"));//集合？表示

            Console.WriteLine(tree.has(20));//探索

            Console.ReadLine();
        }
    }
}
