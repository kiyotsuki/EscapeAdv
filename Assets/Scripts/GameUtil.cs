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

	public static T CreateInstance<T>(T source, GameObject parent) where T : MonoBehaviour
	{
		return CreateInstance<T>(source, parent.transform);
	}

	public static T CreateInstance<T>(T source, Transform parent = null) where T : MonoBehaviour
	{
		if (parent == null)
		{
			parent = source.transform.parent;
		}
		GameObject go = GameObject.Instantiate(source.gameObject);
		go.transform.SetParent(parent, false);
		go.SetActive(true);
		return go.GetComponent<T>();
	}
}
