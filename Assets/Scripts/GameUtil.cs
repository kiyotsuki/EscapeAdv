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
}
