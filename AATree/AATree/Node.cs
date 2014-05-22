using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AATree {
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
			if (l.level == level && level == r.level) {
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
			if(this.has(arg)) { return this; }//データの重複を許さない
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

		public bool has(T arg) {
			if (arg.CompareTo(val) < 0) { return l.has(arg); }
			if (arg.CompareTo(val) > 0) { return r.has(arg); }
			if (arg.CompareTo(val) == 0) { return true; }
			return false;
		}

		public T min() {
			if (l.isSentinel()) { return val; }
			return l.min();
		}

		public T max() {
			if (r.isSentinel()) { return val; }
			return r.max();
		}

		public IAATree<T> delete(T arg) {
			if (!this.has(arg)) { return this; }
			IAATree<T> ret = this;
			if (arg.CompareTo(val) < 0) { l = l.delete(arg); }
			if (arg.CompareTo(val) > 0) { r = r.delete(arg); }
			if (arg.CompareTo(val) == 0) {
				//子が両方sentinelなら（＝葉）自分をsentinelに置き換え
				//sentinelなのでレベル操作などは不要でreturn
				if (l.isSentinel() && r.isSentinel()) { return new Sentinel<T>(); }
				//子が片方しか居なければその子をかわりに接続
				if (l.isSentinel()) { ret = r; }
				if (r.isSentinel()) { ret = l; }
				
				//子が両方いるなら右をたどって一番左を連れてくる
				var tmp = r;
				if (tmp.l.isSentinel()) {
					//自分の右の子に左の子が居なければ付け替えて終了
					tmp.l = this.l;
					ret = tmp;
				} else {
					//sentinelに出くわすまで左向きにたどる
					while (!tmp.l.l.isSentinel()) { tmp = tmp.l; }
					//tmp.lを切り離してthisの位置に持ってくる
					ret = tmp.l;
					tmp.l = tmp.l.r;
					ret.l = this.l;
					ret.r = this.r;
					ret.level = level;
				}
			}
			return ret.levelDown();
		}

		/// <summary>レベルを下げて再平衡化　まだガワだけ</summary>
		/// <returns></returns>
		public IAATree<T> levelDown() {
			IAATree<T> ret = this;
			//葉のレベルは1
			if (l.isSentinel() && r.isSentinel()) { level = 1; }
			//子と離れすぎていたら下げる

			return ret;
			
		}
	}
}
