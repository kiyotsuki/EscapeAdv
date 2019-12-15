using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームメイン
/// メインループを持つクラス
/// 各マネージャを管理する
/// </summary>
public class GameMain : MonoBehaviour
{
	/// <summary>
	/// インスタンス
	/// </summary>
	public static GameMain Instance { get; private set; }


	/// <summary>
	/// ゲームの開始処理
	/// ここで全部のマネージャーなどを用意
	/// </summary>
	void Start()
	{
		// Staticアクセスできるようにする
		Instance = this;

		// マネージャを取得
		_managers = this.GetComponents<ManagerBase>();
	}

	public void Update()
	{
		if(!_initialized)
		{
			// 未初期化なら全てのマネージャの初期化終了を待つ
			// 初期化自体は各マネージャがStartで行っている
			bool complete = true;
			for (int i = 0; i < _managers.Length; i++)
			{
				if(_managers[i].IsInitialized() == false)
				{
					complete = false;
					break;
				}
			}

			// 全てのマネージャの初期化が終わった
			if(complete)
			{
				// 初期化完了通知
				for (int i = 0; i < _managers.Length; i++)
				{
					_managers[i].OnStartGame();
				}
				_initialized = true;
			}
		}
		else
		{
			//　初期化後ループ呼び出し
			for (int i = 0; i < _managers.Length; i++)
			{
				_managers[i].OnUpdateGame();
			}
		}
	}

	/// <summary>
	/// マネージャ取得
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public T GetManager<T>() where T : ManagerBase
	{
		foreach (var manager in _managers)
		{
			if (manager is T)
			{
				return (T)manager;
			}
		}
		return null;
	}
	
	// 各マネージャ実体
	ManagerBase[] _managers = null;

	// 初期化終了フラグ
	bool _initialized = false;
}
