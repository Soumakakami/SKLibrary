using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SKLibrary
{
	public static class Extension
	{
		/// <summary>
		/// 入れ替え
		/// </summary>
		/// <typeparam name="T">なんでいいよ</typeparam>
		/// <param name="x">入れ替え</param>
		/// <param name="y">入れ替え</param>
		public static void Exchange<T>(ref T x, ref T y) where T : struct
		{
			T z = x;
			x = y;
			y = z;
		}
	}
	public static class BoolExtension
	{
		/// <summary>
		/// ランダムなbool値を返します。
		/// </summary>
		/// <returns></returns>
		public static bool RandomBool()
		{
			bool boolean = (int)UnityEngine.Random.Range(0, 2) == 0 ? true : false;
			return boolean;
		}

		/// <summary>
		/// boolのtrue or falseを切り替える
		/// </summary>
		/// <param name="_self"></param>
		/// <returns></returns>
		public static bool Switch(ref this bool _self)
		{
			_self = _self ? false : true;
			return _self;
		}

	}

}

/// <summary>
/// Unity用に拡張した機能です
/// </summary>
namespace SKLibrary.Unity
{
	/// <summary>
	/// ListをUnity用に拡張する場合ここに記述
	/// </summary>
	public static class ListExtension
	{
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
	/// ゲームオブジェクトを拡張する場合ここに記述
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
			GameObject gameObject=_list[0];
			float dis=Vector3.Distance(_self.transform.position,_list[0].transform.position);
			for (int i = 1; i < _list.Count; i++)
			{
				if (Vector3.Distance(_self.transform.position,_list[i].transform.position)<=dis)
				{
					dis = Vector3.Distance(_self.transform.position, _list[i].transform.position);
					gameObject = _list[i];
				}
			}
			return gameObject;
		}
		/// <summary>
		/// 自身から一番近い位置のオブジェクトを返します
		/// </summary>
		/// <param name="_self">自身</param>
		/// <param name="_array">チェックしたい配列</param>
		/// <returns></returns>
		public static GameObject GetClosestObjectArray(this GameObject _self, GameObject[] _array)
		{
			GameObject gameObject = _array[0];
			float dis = Vector3.Distance(_self.transform.position, _array[0].transform.position);
			for (int i = 1; i < _array.Length; i++)
			{
				if (Vector3.Distance(_self.transform.position, _array[i].transform.position) <= dis)
				{
					dis = Vector3.Distance(_self.transform.position, _array[i].transform.position);
					gameObject = _array[i];
				}
			}
			return gameObject;
		}
		/// <summary>
		/// 拡張メソッドでも自身を破壊できるようにする
		/// </summary>
		/// <param name="_self"></param>
		public static void Destroy(this GameObject _self)
		{
			GameObject.Destroy(_self);
		}

	}
	/// <summary>
	/// 独自のDebugを追加する場合ここに記述
	/// </summary>
	public static class DebugLog
	{
		public static void ArrayLog<T>(T[]_self)
		{
			for (int i = 0; i < _self.Length; i++)
			{
				Debug.Log("["+i+"]要素目    "+_self[i]);
			}
		}
	}

	/// <summary>
	/// Unity.Engine.GameObjectではない独自のGameObject
	/// </summary>
	public static class GameObjectUtils
	{

	}
}

/// <summary>
/// セーブとロードを行える
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

#if UNITY_EDITOR
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
				Debug.LogError("ディレクトリが見つかりませんでした");
				return null;
			}
			if (!File.Exists(saveFileName))
			{
				Debug.LogError("ファイルが見つかりませんでした");
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
	}

}




