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

	public static class AATreeFactory<T>
	where T:IComparable{
		public static IAATree<T> newAATree() {
			return new Sentinel<T>();
		}
		public static IAATree<T> newAATree(T arg) {
			return new Node<T>(arg);
		}
	}
	
	/// <summary>nullを意味するノード。値はない。
	/// 葉は左右ともsentinelを子に持っている
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Sentinel<T> : IAATree<T>
	where T : IComparable {
		public int level { get { return 0; } set { } }
		public IAATree<T> l { get { return null; } set { } }
		public IAATree<T> r { get { return null; } set { } }
		public bool isSentinel() { return true; }
		public IAATree<T> pull() { return this; }
		public IAATree<T> skew() { return this; }
		public IAATree<T> split() { return this; }
		public IAATree<T> insert(T arg) {
			return new Node<T>(arg);
		}
		public IAATree<T> delete(T arg) { return this; }
		public string disp(string arg) { return string.Empty; }
		public bool has(T arg) { return false; }
		//一応作ったがsentinelのval呼び出しは好ましくないのでisSentinel()で事前に調べて避けるべき
		//今のところどこでも使ってないはず
		public T val { get { return default(T); } }
		public T min() { return default(T); }
		public T max() { return default(T); }
	}


}
	

