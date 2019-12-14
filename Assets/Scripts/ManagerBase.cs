using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マネージャー基底クラス
/// 配置されると自動的にコルーチンを使った初期化を行う
/// </summary>
public class ManagerBase : MonoBehaviour
{
	public void Start()
	{
		// 初期化開始
		StartCoroutine(initialize());
	}

	private IEnumerator initialize()
	{
		yield return Setup();
		_initialized = true;
		yield break;
	}

	/// <summary>
	/// 初期化処理
	/// </summary>
	/// <returns></returns>
	protected virtual IEnumerator Setup()
	{
		yield break;
	}

	/// <summary>
	/// ゲーム開始通知
	/// マネージャの初期化終了時に一度だけ呼ばれる
	/// </summary>
	public virtual void OnStartGame()
	{

	}

	/// <summary>
	/// ゲームループ
	/// 初期化完了後であることが担保されているメインループ
	/// </summary>
	public virtual void OnUpdateGame()
	{

	}

	public bool IsInitialized()
	{
		return _initialized;
	}

	bool _initialized = false;
}
