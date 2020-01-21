using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
}

namespace SKTool
{
	public static class BasicClass
	{
		public static void Exchange<T>(ref T x,ref T y)where T:struct
		{
			T z = x;
			x = y;
			y = z;
			
		}
		
	}		
}