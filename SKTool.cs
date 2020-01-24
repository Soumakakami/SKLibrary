using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Unity用に拡張した機能です
/// </summary>
namespace SKTool.Unity
{
	/// <summary>
	/// Listを拡張する場合ここに記述
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
			GameObject gameObject=null;
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
			GameObject gameObject = null;
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

	}

}

namespace SKTool
{
	public static class BasicClass
	{
		/// <summary>
		/// 入れ替え
		/// </summary>
		/// <typeparam name="T">なんでいいよ</typeparam>
		/// <param name="x">入れ替え</param>
		/// <param name="y">入れ替え</param>
		public static void Exchange<T>(ref T x,ref T y)where T:struct
		{
			T z = x;
			x = y;
			y = z;
		}
	}
}