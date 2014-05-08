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
		IAATree<T> insert(T arg);
		//IAATree<T> delete();
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
		public string disp(string arg) { return string.Empty; }
		public bool has(T arg) { return false; }
		//一応作ったがsentinelのval呼び出しは好ましくないのでisSentinel()で事前に調べて避けるべき
		//今のところどこでも使ってないはず
		public T val { get { return default(T); } }
	}

	/// <summary>葉、枝</summary>
	/// <typeparam name="T"></typeparam>
	public class Node<T> : IAATree<T>
	where T : IComparable {
		public T val { get; private set; }
		public int level { get; set; }
		public IAATree<T> l { get; set; }
		public IAATree<T> r { get; set; }
		public bool isSentinel() { return false; }
		//葉ノードのレベルは1である。
		//左の子ノードのレベルは親ノードのレベルより必ず小さい。
		//右の子ノードのレベルは親ノードのレベル以下である。
		//右の孫ノードのレベルは祖父（祖母）ノードのレベルより必ず小さい。
		//レベルが1より大きいノードは、必ず2つの子ノードを持つ。

		/// <summary>skew+splitとしてpullを定義する
		/// skew直後にsplitする状況は、左右に水平リンクが存在する時である
		/// 操作の結果、本体のレベルを引き上げただけになる
		/// </summary>
		/// <returns></returns>
		public IAATree<T> pull() {
			if(l.level == level && level == r.level) {
				level++;
			}
			return this;
		}

		public IAATree<T> skew() {
			if (l.level == level) {
				var L = l;
				this.l = L.r;
				L.r = this;
				return L;
			}
			return this;
		}

		public IAATree<T> split() {
			if (level == r.r.level) {
				var R = r;
				r = R.l;
				R.l = this;
				R.level++;
				return R;
			}
			return this;
		}

		public Node(T arg) {
			level = 1;
			l = new Sentinel<T>();
			r = new Sentinel<T>();
			val = arg;
		}

		public IAATree<T> insert(T arg) {
			if (val.CompareTo(arg) > 0) {
				l = l.insert(arg);
			} else {
				r = r.insert(arg);
			}
			return this.pull().skew().split();
		}

		public string disp(string arg) {
			var ret = string.Empty;
			if (arg == "t") {//ツリー？表示
				ret += l.disp(arg);
				ret += new string(' ', level) + val.ToString() + "\r\n";
				ret += r.disp(arg);
			} else if (arg == "s") {//集合？表示
				ret += "(";
				ret += l.disp(arg);
				ret += val.ToString();
				ret += r.disp(arg);
				ret += ")";
			}

			return ret;
		}

		public bool has(T arg){
			if (arg.CompareTo(val) < 0) { return l.has(arg); }
			if (arg.CompareTo(val) > 0) { return r.has(arg); }
			if (arg.CompareTo(val) == 0) { return true; }
			return false;
		}
	}
}
	

