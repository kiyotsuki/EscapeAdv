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

	public static GameObject GetNamedSceneObject(string name)
	{
		return GameMain.Instance.GetNamedSceneObject(name);
	}

	public static void StartCoroutine(IEnumerator iEnumerator)
	{
		GameMain.Instance.StartCoroutine(iEnumerator);
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

	public static MapData GetCurrentMap()
	{
		var manager = GetManager<MapManager>();
		if(manager == null)
		{
			return null;
		}
		return manager.GetCurrentMap();
	}

	/// <summary>
	/// プレイヤーコントローラ取得
	/// 引数を省略するとカレントプレイヤーを取得する
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public static PlayerController GetPlayerController(ParamPlayer.ID id = ParamPlayer.ID.Invalid)
	{
		var manager = GetManager<PlayerManager>();
		if(manager == null)
		{
			return null;
		}
		return manager.GetPlayerController(id);
	}
}
