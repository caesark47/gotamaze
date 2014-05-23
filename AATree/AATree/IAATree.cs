using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AATree {
	public interface IAATree<T>
	where T : IComparable {
		IAATree<T> skew();
		IAATree<T> split();
		IAATree<T> pull();
		IAATree<T> levelDown();
		T val { get; }
		T min();
		T max();
		IAATree<T> insert(T arg);
		IAATree<T> delete(T arg);
		int level { get; set; }
		IAATree<T> l { get; set; }
		IAATree<T> r { get; set; }
		bool isSentinel();
		bool has(T arg);
		string disp(string arg);
	}
}
