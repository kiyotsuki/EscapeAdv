using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームユーティリティ
/// </summary>
public static class GameUtil
{
	public static T GetManager<T>() where T : ManagerBase
	{
		return GameMain.Instance.GetManager<T>();
	}
	
	public static GameObject FindChild(GameObject go, string name)
	{
		if(go == null)
		{
			Debug.LogError("FindChild GameObjectがnullです Name=" + name);
			return null;
		}
		var trans = go.transform.Find(name);
		if(trans == null)
		{
			Debug.LogError("FindChild 指定の子が見つかりません Name=" + name + " Parent=" + go.name);
			return null;
		}
		return trans.gameObject;
	}
}
