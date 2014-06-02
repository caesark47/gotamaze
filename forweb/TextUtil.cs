using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication_clib {
	static public class TextUtil {
		/// <summary>パスワードなどからハッシュ生成。ソルトは事前につけておくこと。
		/// 生成方法の変更があるときはv101を新設すること</summary>
		/// <param name="arg">ハッシュ生成対象文字列</param>
		/// <returns>バージョン表記+ハッシュ文字列（計100byte）</returns>
		static public string getPasswordHash_v100(string arg) {
			int stretch = 1000;//ストレッチ回数
			var target = arg;
			for (int i = 0; i < stretch; i++) {
				target = _getHash_one(i.ToString() + target + i.ToString());
			}
			return "v100" + target;
		}
		/// <summary>一回ハッシュ計算をする</summary>
		/// <param name="arg">対象文字列</param>
		/// <returns>16進法表示のハッシュ</returns>
		static private String _getHash_one(String arg) {
			var ret = String.Empty;
			string target = arg;
			byte[] bytes = Encoding.UTF8.GetBytes(target);
			HashAlgorithm crypto = new SHA384CryptoServiceProvider();
			byte[] hashed = crypto.ComputeHash(bytes);

			StringBuilder hashedText = new StringBuilder();
			for (int i = 0; i < hashed.Length; i++) {
				hashedText.AppendFormat("{0:X2}", hashed[i]);
			}
			ret = hashedText.ToString();
			return ret;
		}
	}
}