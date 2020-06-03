using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Diagnostics;
using UnityEngine;


/// <summary>
/// Unity用に拡張した機能です
/// </summary>
namespace SKLibrary
{
	//--------------------------------ここから下は拡張--------------------------------

	public static class Extension
	{
		/// <summary>
		/// 入れ替え
		/// </summary>
		public static void Swap(ref sbyte x, ref sbyte y)
		{
			x ^= y;
			y ^= x;
			x ^= y;
		}
		public static void Swap(ref byte x, ref byte y)
		{
			x ^= y;
			y ^= x;
			x ^= y;
		}
		public static void Swap(ref short x, ref short y)
		{
			x ^= y;
			y ^= x;
			x ^= y;
		}
		public static void Swap(ref ushort x, ref ushort y)
		{
			x ^= y;
			y ^= x;
			x ^= y;
		}
		public static void Swap(ref int x, ref int y)
		{
			x ^= y;
			y ^= x;
			x ^= y;
		}
		public static void Swap(ref uint x, ref uint y)
		{
			x ^= y;
			y ^= x;
			x ^= y;
		}
		public static void Swap(ref long x, ref long y)
		{
			x ^= y;
			y ^= x;
			x ^= y;
		}
		public static void Swap(ref ulong x, ref ulong y)
		{
			x ^= y;
			y ^= x;
			x ^= y;
		}
		public static void Swap(ref char x, ref char y)
		{
			x ^= y;
			y ^= x;
			x ^= y;
		}
		public static void Swap(ref float x, ref float y)
		{
			float z = x;
			x = y;
			y = z;
		}
		public static void Swap(ref double x, ref double y)
		{
			double z = x;
			x = y;
			y = z;
		}
		public static void Swap(ref string x, ref string y)
		{
			string z = x;
			x = y;
			y = z;

		}
		public static void Swap<T>(ref T x, ref T y)where T:UnityEngine.Object
		{
			T z = x;
			x = y;
			y = z;
		}

	}
	/// <summary>
	/// boolの拡張
	/// </summary>
	public static class BoolExtension
	{
		/// <summary>
		/// ランダムなbool値を返します。
		/// </summary>
		/// <returns></returns>
		public static bool RandomBool()
		{
			return UnityEngine.Random.Range(0, 2) == 0 ? true : false;
		}

		/// <summary>
		/// boolのtrue or falseを切り替える
		/// </summary>
		/// <param name="_self"></param>
		/// <returns></returns>
		public static bool Switching(this bool _self)
		{
			_self = _self ? false : true;
			return _self;
		}

	}
	/// <summary>
	/// 配列の拡張
	/// </summary>
	public static class ArrayExtension
	{
		/// <summary>
		/// 配列の先頭を返します
		/// </summary>
		public static T First<T>(this T[] _self)
		{
			return _self[0];
		}

		/// <summary>
		/// 配列の最後を返します
		/// </summary>
		public static T Last<T>(this T[] _self)
		{
			return _self[_self.Length];
		}

		/// <summary>
		/// クイックソート
		/// </summary>
		/// <param name="array">ソートする配列</param>
		/// <param name="left">ソート範囲の最初のインデックス</param>
		/// <param name="right">ソート範囲の最後のインデックス</param>
		/// <param name="ascending"></param>
		public static void QuickSort(int[] array, int left, int right, bool ascending = true)
		{

			// 交換回数を保持する変数です。
			int swapNum = 0;
			// ソートする範囲が1要素以下であれば処理をしない
			if (left >= right)
			{
				return;
			}
			// 左から確認していくインデックスを格納します。
			int i = left;

			// 右から確認していくインデックスを格納します。
			int j = right;

			// ピボット選択に使う配列の中央のインデックスを計算します。
			int mid = (left + right) / 2;

			// ピボットを決定します。
			int pivot = GetMediumValue(array[i], array[mid], array[j]);

			while (true)
			{
				if (ascending)
				{
					// ピボットの値以上の値を持つ要素を左から確認します。
					while (array[i] < pivot)
					{
						i++;
					}

					// ピボットの値以下の値を持つ要素を右から確認します。
					while (array[j] > pivot)
					{
						j--;
					}
				}
				else
				{
					// ピボットの値以上の値を持つ要素を左から確認します。
					while (array[i] > pivot)
					{
						i++;
					}

					// ピボットの値以下の値を持つ要素を右から確認します。
					while (array[j] < pivot)
					{
						j--;
					}
				}


				// 左から確認したインデックスが、右から確認したインデックス以上であれば外側のwhileループを抜けます。
				if (i >= j)
				{
					break;
				}

				// そうでなければ、見つけた要素を交換します。
				Extension.Exchange(ref array[j],ref array[i]);

				// 交換を行なった要素の次の要素にインデックスを進めます。
				i++;
				j--;

				// 交換回数を増やします。
				swapNum++;
			}
			// 左側の範囲について再帰的にソートを行います。
			QuickSort(array, left, i - 1, ascending);

			// 右側の範囲について再帰的にソートを行います。
			QuickSort(array, j + 1, right,ascending);
		}

		private static int GetMediumValue(int top, int mid, int bottom)
		{
			 //配列の最初が中央値より低い時
			if (top < mid)
			{
				if (mid < bottom)
				{
					return mid;
				}
				else if (bottom < top)
				{
					return top;
				}
				else
				{
					return bottom;
				}
			}
			else
			{
				if (bottom < mid)
				{
					return mid;
				}
				else if (top < bottom)
				{
					return top;
				}
				else
				{
					return bottom;
				}
			}
		}
	}

	/// <summary>
	/// Listを拡張
	/// </summary>
	public static class ListExtension
	{

		/// <summary>
		/// Listの先頭を返します
		/// </summary>
		public static T First<T>(this List<T> _self)
		{
			return _self[0];
		}

		/// <summary>
		/// Listの最後を返します
		/// </summary>
		public static T Last<T>(this List<T> _self)
		{
			return _self[_self.Count];
		}

		/// <summary>
		/// 要素の中にあるnullを削除する
		/// </summary>
		public static void RemoveNull<T>(this List<T> _self) where T : UnityEngine.Object
		{
			for (int i = 0; i < _self.Count; i++)
			{
				if (!_self[i])
				{
					_self.RemoveAt(i);
					i--;
				}
			}
		}
		/// <summary>
		/// 要素の中にnullがあればそこに割り振る場合によってはAddする
		/// </summary>
		/// <param name="_add">追加する要素</param>
		/// <param name="_addFlag">Addする場合(true)</param>
		public static void Allocation<T>(this List<T> _self,T _add,bool _addFlag=false) where T : UnityEngine.Object
		{

			for (int i = 0; i < _self.Count; i++)
			{
				if (!_self[i])
				{
					_self[i] = _add;
					return;
				}
			}
			if (_addFlag)
			{
				_self.Add(_add);
			}
		}
	}

	/// <summary>
	/// ゲームオブジェクトを拡張
	/// </summary>
	public static class GameObjectExtension
	{
		/// <summary>
		/// 自身から一番近い位置のオブジェクトを返します
		/// </summary>
		/// <param name="_self">自身</param>
		/// <param name="_list">チェックしたいリスト</param>
		/// <returns></returns>
		public static GameObject GetClosestObjectList(this GameObject _self,List<GameObject> _list)
		{
			return _list
				.OrderBy(x => (x.transform.position - _self.transform.position).magnitude)
				.First().gameObject;

		}
		/// <summary>
		/// 自身から一番近い位置のオブジェクトを返します
		/// </summary>
		/// <param name="_self">自身</param>
		/// <param name="_array">チェックしたい配列</param>
		/// <returns></returns>
		public static GameObject GetClosestObjectArray(this GameObject _self, GameObject[] _array)
		{
			return _array
				.OrderBy(x => (x.transform.position - _self.transform.position).magnitude)
				.First().gameObject;
		}
		/// <summary>
		/// 拡張メソッドでも自身を破壊できるようにする
		/// </summary>
		/// <param name="_self"></param>
		public static void Destroy(this GameObject _self)
		{
			GameObject.Destroy(_self);
		}
		/// <summary>
		/// コンポーネントを取得(なければ追加)
		/// </summary>
		public static T GetOrAddComponent<T>(this GameObject _self) where T : Component
		{
			return _self.GetComponent<T>() ?? _self.AddComponent<T>();
		}
		/// <summary>
		/// 最上層の親オブジェクトを返します。親オブジェクトが存在しない場合自身を返す。
		/// </summary>
		/// <param name="_self"></param>
		/// <returns></returns>
		public static GameObject GetTopParent(this GameObject _self)
		{
			GameObject topParent = _self;
			while (topParent.transform.parent == true)
			{
				topParent = topParent.transform.parent.gameObject;
			}
			return topParent;
		}
		/// <summary>
		/// 深い階層まで子オブジェクトを名前で検索します
		/// </summary>
		public static GameObject FindDeep(this GameObject self, string name, bool includeInactive = false)
		{
			GameObject[] children = self.GetComponentsInChildren<GameObject>(includeInactive);
			GameObject ret = null;

			foreach (var gameObject in children)
			{
				if (gameObject.name == name)
				{
					ret = gameObject;
				}
			}
			return ret;
		}
		/// <summary>
		/// 全ての子オブジェクトを返します
		/// </summary>
		/// <param name="self">自身</param>
		/// <param name="includeInactive">非アクティブのオブジェクトの取得するか</param>
		/// <returns></returns>
		public static GameObject[] GetChildren(this GameObject _self, bool includeInactive = false)
		{
			return _self
				.GetComponentsInChildren<GameObject>(includeInactive)
				.Where(x => x != _self.transform)
				.Select(x => x.gameObject)
				.ToArray();
		}

		/// <summary>
		/// X座標を設定します
		/// </summary>
		public static void SetPositionX(this GameObject self, float x)
		{
			self.transform.position = new Vector3(x,self.transform.position.y,self.transform.position.z);
		}
		/// <summary>
		/// Y座標を設定します
		/// </summary>
		public static void SetPositionY(this GameObject self, float y)
		{
			self.transform.position = new Vector3(self.transform.position.x,y, self.transform.position.z);
		}
		/// <summary>
		/// Z座標を設定します
		/// </summary>
		public static void SetPositionZ(this GameObject self, float z)
		{
			self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y, z);
		}

		/// <summary>
		/// X座標を加算します
		/// </summary>
        public static void AddPositionX(this GameObject self, float x)
		{
			self.transform.position += new Vector3(x, 0, 0);
		}
		/// <summary>
		/// Y座標を加算します
		/// </summary>
        public static void AddPositionY(this GameObject self, float y)
		{
			self.transform.position += new Vector3(0, y, 0);
		}
		/// <summary>
		/// Z座標を加算します
		/// </summary>
        public static void AddPositionZ(this GameObject self, float z)
		{
			self.transform.position += new Vector3(0, 0, z);
		}

		/// <summary>
		/// 座標を0にリセットします
		/// </summary>
		/// <param name="self"></param>
        public static void ResetPosition(this GameObject self)
		{
			self.transform.position = Vector3.zero;
		}


	}

	/// <summary>
	/// トランスフォームを拡張
	/// </summary>
	public static class TransformExtension
	{
		/// <summary>
		/// 最上層の親オブジェクトのTransformを返します。親オブジェクトが存在しない場合自身を返す。
		/// </summary>
		/// <param name="_self"></param>
		/// <returns></returns>
        public static Transform GetTopParent(this Transform _self)
		{
			Transform topParent = _self;
			while (topParent.parent == true)
			{
				topParent = topParent.parent;
			}
			return topParent;
		}
		/// <summary>
		/// 深い階層まで子オブジェクトを名前で検索します
		/// </summary>
        public static Transform FindDeep(this Transform self, string name, bool includeInactive = false)
		{
			Transform[] children = self.GetComponentsInChildren<Transform>(includeInactive);
			Transform ret = null;

			foreach (var gameObject in children)
			{
				if (gameObject.name == name)
				{
					ret = gameObject;
				}
			}
			return ret;
		}
		/// <summary>
		/// 全ての子オブジェクトを返します
		/// </summary>
		/// <param name="self">自身</param>
		/// <param name="includeInactive">非アクティブのオブジェクトの取得するか</param>
		/// <returns></returns>
        public static Transform[] GetChildren(this Transform _self, bool includeInactive = false)
		{
			return _self
				.GetComponentsInChildren<Transform>(includeInactive)
				.Where(x => x != _self.transform)
				.Select(x => x.transform)
				.ToArray();
		}

		/// <summary>
		/// X座標を設定します
		/// </summary>
        public static void SetPositionX(this Transform self, float x)
		{
			self.transform.position = new Vector3(x, self.transform.position.y, self.transform.position.z);
		}
		/// <summary>
		/// Y座標を設定します
		/// </summary>
        public static void SetPositionY(this Transform self, float y)
		{
			self.transform.position = new Vector3(self.transform.position.x, y, self.transform.position.z);
		}
		/// <summary>
		/// Z座標を設定します
		/// </summary>
        public static void SetPositionZ(this Transform self, float z)
		{
			self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y, z);
		}

		/// <summary>
		/// X座標を加算します
		/// </summary>
        public static void AddPositionX(this Transform self, float x)
		{
			self.transform.position += new Vector3(x,0,0);
		}
		/// <summary>
		/// Y座標設定します
		/// </summary>
        public static void AddPositionY(this Transform self, float y)
		{
			self.transform.position += new Vector3(0, y, 0);
		}
		/// <summary>
		/// Z座標を加算します
		/// </summary>
        public static void AddPositionZ(this Transform self, float z)
		{
			self.transform.position += new Vector3(0, 0, z);
		}

		/// <summary>
		/// 座標を0にリセットします
		/// </summary>
		/// <param name="self"></param>
        public static void ResetPosition(this Transform self)
		{
			self.transform.position = Vector3.zero;
		}
	}

	/// <summary>
	/// レンダラーテクスチャを拡張
	/// </summary>
	public static class RenderTextuerExtension
	{
		/// <summary>
		/// レンダラーテクスチャからテクスチャ2Dを生成する(かなり重たい処理なので使用には注意)
		/// </summary>
		/// <param name="_self">自身</param>
		/// <param name="mainCamera">どのカメラの映像か</param>
		/// <returns></returns>
		public static Texture2D CreateTexture2D(this RenderTexture _self,Camera mainCamera)
		{
			//Texture2Dを作成
			Texture2D texture2D = new Texture2D(_self.width, _self.height, TextureFormat.ARGB32, false, false);

			//subCameraにRenderTextureを入れる
			mainCamera.targetTexture = _self;

			//手動でカメラをレンダリングします
			mainCamera.Render();


			RenderTexture.active = _self;
			texture2D.ReadPixels(new Rect(0, 0, _self.width, _self.height), 0, 0);
			texture2D.Apply();

			//元に戻す別のカメラを用意してそれをRenderTexter用にすれば下のコードはいらないです。
			mainCamera.targetTexture = null;
			RenderTexture.active = null;

			return texture2D;
		}
	}

	/// <summary>
	/// テクスチャ2Dを拡張
	/// </summary>
	public static class Texture2DExtension
	{
		public static Sprite CreateSprite(this Texture2D tex2D)
		{
			Sprite sprite = Sprite.Create(tex2D, new Rect(0f, 0f, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f), 100f);
			return sprite;
		}
	}
	//--------------------------------ここから上は拡張--------------------------------

	/// <summary>
	///  新しいGameObject
	/// </summary>
	public static class GameObjectUtils
	{
		/// <summary>
		/// 指定した文字列が含まれているオブジェクトを取得
		/// </summary>
		/// <param name="name">含めたい文字列</param>
		/// <returns>取得したオブジェクトの配列</returns>
		public static GameObject[] FindContainsName(string name)
		{
			//一度すべてのオブジェクトを取得
			Transform[] gameObjects=GameObject.FindObjectsOfType<Transform>();
			//取得したオブジェクトの名前の中に指定した文字列が含まれていた場合配列に格納
			GameObject[] ret = gameObjects
									.Where(x => 0 <= x.name.IndexOf(name))
									.Select(x => x.gameObject)
									.ToArray();
			return ret;
		}
	}

	/// <summary>
	/// 新しいRandom
	/// </summary>
	public static class RandomUtils
	{
		/// <summary>
		/// 配列の中の要素をランダムで返す
		/// </summary>
		public static T RandomArray<T>(T[] values)
		{
			return values[UnityEngine.Random.Range(0, values.Length)];
		}

		/// <summary>
		/// listの中の要素をランダムで返す
		/// </summary>
		public static T RandomList<T>(List<T> values)
		{
			return values[UnityEngine.Random.Range(0, values.Count)];
		}

		/// <summary>
		/// ランダムでtrueかfalseを返す
		/// </summary>
		public static bool BoolValue { get { return UnityEngine.Random.Range(0, 2) == 0; } }
	}

	/// <summary>
	///新しいDebug
	/// </summary>
	public static class DebugUtils
	{
		[Conditional("UNITY_EDITOR")]
		/// <summary>
		/// ログを出力します
		/// </summary>
		/// <param name="message">メッセージ</param>
		public static void Log(string message)
		{
			UnityEngine.Debug.Log(message);
		}

		[Conditional("UNITY_EDITOR")]
		/// <summary>
		/// ログを出力します
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="context">メッセージが適用されるオブジェクト</param>
		public static void Log(object message, UnityEngine.Object context)
		{
			UnityEngine.Debug.Log(message, context);
		}

		[Conditional("UNITY_EDITOR")]
		/// <summary>
		/// サイズしてログを出力します
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="size">文字の大きさ</param>
		/// 
		public static void Log(string message,float size)
		{
			UnityEngine.Debug.Log("<size="+size+">" + message+"</size>");
		}

		[Conditional("UNITY_EDITOR")]
		/// <summary>
		/// サイズしてログを出力します
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="size">文字の大きさ</param>
		/// <param name="context">メッセージが適用されるオブジェクト</param>
		public static void Log(string message, float size, UnityEngine.Object context)
		{
			UnityEngine.Debug.Log("<size=" + size + ">" + message + "</size>",context);
		}

		[Conditional("UNITY_EDITOR")]
		/// <summary>
		/// 色を変更してログを出力します
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="color">文字の色</param>
		public static void Log(string message, Color color)
		{
			UnityEngine.Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGB(color) + "> "+ message + "</color>");
		}

		[Conditional("UNITY_EDITOR")]
		/// <summary>
		/// 色を変更してログを出力します
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="color">文字の色</param>
		/// <param name="context">メッセージが適用されるオブジェクト</param>
		public static void Log(string message, Color color, UnityEngine.Object context)
		{
			UnityEngine.Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGB(color) + "> " + message + "</color>", context);
		}

		[Conditional("UNITY_EDITOR")]
		/// <summary>
		/// 色を変更してログを出力します
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="size">文字の大きさ</param>
		/// <param name="color">文字の色</param>
		public static void Log(string message, float size,Color color)
		{
			UnityEngine.Debug.Log("<color=#"+ ColorUtility.ToHtmlStringRGB(color) + "><size=" + size + ">" + message + "</size></color>");
		}

		[Conditional("UNITY_EDITOR")]
		/// <summary>
		/// 色を変更してログを出力します
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="size">文字の大きさ</param>
		/// <param name="color">文字の色</param>
		/// <param name="context">メッセージが適用されるオブジェクト</param>
		public static void Log(string message, float size, Color color, UnityEngine.Object context)
		{
			UnityEngine.Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGB(color) + "><size=" + size + ">" + message + "</size></color>", context);
		}

		[Conditional("UNITY_EDITOR")]
		/// <summary>
		/// エラーログを出力します
		/// </summary>
		/// <param name="message">メッセージ</param>
		public static void LogError(string message)
		{
			UnityEngine.Debug.LogError(message);
		}
		[Conditional("UNITY_EDITOR")]
		/// <summary>
		/// エラーログを出力します
		/// </summary>
		/// <param name="message">メッセージ</param>
		public static void LogError(string message, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogError(message,context);
		}

		[Conditional("UNITY_EDITOR")]
		/// <summary>
		/// 警告ログを出力します
		/// </summary>
		/// <param name="message">メッセージ</param>
		public static void LogWarning(string message)
		{
			UnityEngine.Debug.LogWarning(message);
		}
		[Conditional("UNITY_EDITOR")]
		/// <summary>
		/// 警告ログを出力します
		/// </summary>
		/// <param name="message">メッセージ</param>
		public static void LogWarning(string message, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogWarning(message,context);
		}

		/// <summary>
		/// 配列のログを出します
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="_self"></param>
		public static void ArrayLog<T>(T[] _self)
		{
			int i = 0;
			foreach (var item in _self)
			{
				UnityEngine.Debug.Log("[" + i + "]要素目    " + _self[i]);
				i++;
			}
		}
		public static void ArrayLog<T>(T[,] _self)
		{
			string str = "";
			for (int i = 0; i < _self.GetLength(0); i++)
			{
				str += "[要素" + i + "列目]:";
				for (int j = 0; j < _self.GetLength(1); j++)
				{
					str += _self[i, j] + ":";
				}
				UnityEngine.Debug.Log(str);
				str = "";
			}
		}
	}
}

/// <summary>
/// セーブ&ロード機能
/// </summary>
namespace SKLibrary.SaveAndLoad
{
	public static class SaveLoadSystem
	{
		//ベースとなるフォルダ名
		private const string BASE_FOLDER_NAME = "/Save/";

		//デフォルトのフォルダ名
		private const string DEFAULT_FOLDER_NAME = "SaveFolder";

		/// <summary>
		/// フォルダー名の基づいてファイルをセーブ及びロードする時に使用するパスを決める
		/// </summary>
		/// <param name="folderName">フォルダ名</param>
		/// <returns>パス</returns>
		static string CreateSavePath(string _folderName = DEFAULT_FOLDER_NAME)
		{
			string savePath;

			//使っているプラットフォームを確認(IPhoneかどうか確認)
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				savePath = Application.persistentDataPath + BASE_FOLDER_NAME;
			}
			//それ以外のプラットフォームの場合
			else
			{
				savePath = Application.persistentDataPath + BASE_FOLDER_NAME;
			}

		#if UNITY_EDITOR || UNITY_STANDALONE_WIN
			savePath = Application.dataPath + BASE_FOLDER_NAME;
		#endif

			// セーブパス + SaveManager/
			savePath = savePath + _folderName + "/";

			return savePath;
		}

		/// <summary>
		/// 保存するファイル名を決める
		/// </summary>
		/// <param name="_fileName">ファイル名</param>
		/// <returns>保存されるファイル名</returns>
		static string SaveFileName(string _fileName)
		{
			return _fileName + ".binary";
		}

		/// <summary>
		/// セーブする
		/// </summary>
		/// <param name="_saveObject"></param>
		/// <param name="_fileName">ファイルの名前</param>
		/// <param name="_folderName">フォルダの名前</param>
		public static void Save(object _saveObject, string _fileName ,string _folderName = DEFAULT_FOLDER_NAME)
		{
			//セーブパスを決める
			string savePath = CreateSavePath(_folderName);

			//ファイル名を決める
			string saveFileName = SaveFileName(_fileName);

			//ディレクトリがあるか確認、なければ作成
			if (!Directory.Exists(savePath))
			{
				Directory.CreateDirectory(savePath);
			}

			//クラスをバイナリとして扱う
			BinaryFormatter formatter = new BinaryFormatter();

			//ファイル作成
			FileStream saveFile = File.Create(savePath + saveFileName);

			//オブジェクトをシリアル化しディスク上にファイル書き込み
			formatter.Serialize(saveFile, _saveObject);

			//書き込み終了
			saveFile.Close();
		}

		/// <summary>
		/// ロードする
		/// </summary>
		/// <param name="_fileName">ファイルの名前</param>
		/// <param name="_folderName">フォルダの名前</param>
		/// <returns></returns>
		public static object Load(string _fileName, string _folderName = DEFAULT_FOLDER_NAME)
		{
			//セーブパス(指定フォルダ名)を取得
			string savePath = CreateSavePath(_folderName);

			//バイナリファイル名を取得
			string saveFileName = savePath + SaveFileName(_fileName);

			//返すデータ
			object returnObject;

			//Saveディレクトリまたはバイナリファイルが存在しない場合
			if (!Directory.Exists(savePath))
			{
				UnityEngine.Debug.LogError("ディレクトリが見つかりませんでした");
				return null;
			}
			if (!File.Exists(saveFileName))
			{
				UnityEngine.Debug.LogError("ファイルが見つかりませんでした");
				return null;
			}

			BinaryFormatter formatter = new BinaryFormatter();

			//ファイル開く
			FileStream saveFile = File.Open(saveFileName, FileMode.Open, FileAccess.Read, FileShare.Read);

			//バイナリファイルをデシリアル化しオブジェクトに変換する
			returnObject = formatter.Deserialize(saveFile);

			saveFile.Close();

			return returnObject;
		}

		/// <summary>
		/// 指定したフォルダにどれだけファイルがあるか
		/// </summary>
		/// <param name="_folderName">フォルダ名</param>
		/// <param name="_extension">拡張子(例:*.binary)</param>
		/// <returns></returns>
		public static int ReadFiles(string _folderName = DEFAULT_FOLDER_NAME, string _extension="*.binary")
		{
			int count=0;
			string savePath = CreateSavePath(_folderName);
			count = Directory.GetFiles(savePath, "*.binary", SearchOption.AllDirectories).Length;
			return count;
		}


		/// <summary>
		/// 暗号化
		/// </summary>
		public const string savePath = "./date.binary";
		private const string _password = "passwordstring";
		private const string _salt = "saltstring";
		static private RijndaelManaged _rijindeal;
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		static SaveLoadSystem()
		{
			_rijindeal = new RijndaelManaged();
			_rijindeal.KeySize = 128;
			_rijindeal.BlockSize = 128;

			byte[] bsalt = Encoding.UTF8.GetBytes(_salt);
			Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(_password, bsalt);
			deriveBytes.IterationCount = 1000;

			_rijindeal.Key = deriveBytes.GetBytes(_rijindeal.KeySize / 8);
			_rijindeal.IV = deriveBytes.GetBytes(_rijindeal.BlockSize / 8);
		}

		/// <summary>
		/// 暗号化したセーブ
		/// </summary>
		/// <param name="_saveData">保存したいデータ</param>
		/// <param name="_fileName">ファイル名</param>
		/// <param name="_folderName">フォルダ名</param>
		static public void EncryptionSave(object _saveData, string _fileName, string _folderName = DEFAULT_FOLDER_NAME)
		{
			//メモリーストーリーム作成
			MemoryStream stream = new MemoryStream();

			//バイナリフォーマッターを作成
			BinaryFormatter formatter = new BinaryFormatter();

			//セーブパスを決める
			string savePath = CreateSavePath(_folderName);

			//ファイル名を決める
			string saveFileName = SaveFileName(_fileName);



			//					↓書き込み先 ↓シリアル化したクラス
			formatter.Serialize(stream, _saveData);

			//バイナリ配列として保存
			byte[] source = stream.ToArray();

			//AES暗号化
			source = AESlize(source);

			//ディレクトリがあるか確認、なければ作成
			if (!Directory.Exists(savePath))
			{
				Directory.CreateDirectory(savePath);
			}
			//ファイル作成
			FileStream saveFile = File.Create(savePath + saveFileName);

			//オブジェクトをシリアル化しディスク上にファイル書き込み
			saveFile.Write(source,0,source.Length);

			//書き込み終了
			saveFile.Close();

		}

		/// <summary>
		/// 暗号化したデータをロード
		/// </summary>
		/// <param name="_fileName">ロードしたいファイル名</param>
		/// <param name="_folderName">フォルダ名</param>
		/// <returns></returns>
		static public object EncryptionLoad(string _fileName, string _folderName = DEFAULT_FOLDER_NAME)
		{
			object data = null;

			//セーブパスを決める
			string savePath = CreateSavePath(_folderName);

			//バイナリファイル名を取得
			string saveFileName = savePath + SaveFileName(_fileName);


			//Saveディレクトリまたはバイナリファイルが存在しない場合
			if (!Directory.Exists(savePath))
			{
				UnityEngine.Debug.LogError("ディレクトリが見つかりませんでした");
				return null;
			}
			if (!File.Exists(saveFileName))
			{
				UnityEngine.Debug.LogError("ファイルが見つかりませんでした");
				return null;
			}


			FileStream stream = new FileStream(saveFileName, FileMode.Open, FileAccess.Read);
			MemoryStream memStream = new MemoryStream();
				
			const int size = 4096;
			byte[] buffer = new byte[size];
			int numBytes;

			while ((numBytes = stream.Read(buffer, 0, size)) > 0)
			{
				memStream.Write(buffer, 0, numBytes);
			}

			byte[] source = memStream.ToArray();
			source = DeAESlize(source);

			MemoryStream memStream2 = new MemoryStream(source);
			
			BinaryFormatter formatter = new BinaryFormatter();
			data = formatter.Deserialize(memStream2) as object;

			return data;
		}

		static private byte[] AESlize(byte[] data)
		{
			ICryptoTransform encryptor = _rijindeal.CreateEncryptor();
			byte[] encrypted = encryptor.TransformFinalBlock(data, 0, data.Length);

			encryptor.Dispose();

			return encrypted;
		}
		static private byte[] DeAESlize(byte[] data)
		{
			ICryptoTransform decryptor = _rijindeal.CreateDecryptor();
			byte[] plain = decryptor.TransformFinalBlock(data, 0, data.Length);

			return plain;
		}
	}
}