using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームメイン
/// メインループを持つクラス
/// ここで各マネージャを生成し管理する
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

		// 登録されたオブジェクトをNamedSceneObjectに登録
		foreach(var go in _sceneObjects)
		{
			var name = go.name;
			var key = name.GetHashCode();
			if(_namedSceneObject.ContainsKey(key))
			{
				Debug.LogError("同じ名前のSceneObjectが存在します Name=" + name);
				continue;
			}
			_namedSceneObject.Add(key, go);
		}

		// 各マネージャ作成
		_managers.Add(new FadeManager());
		_managers.Add(new MapManager());
		_managers.Add(new ScenarioManager());
		_managers.Add(new DebugManager());

		// 各マネージャ初期化
		foreach (var manager in _managers)
		{
			manager.Initialize();
		}

		// 初期化終了通知
		foreach (var manager in _managers)
		{
			manager.OnInitialized();
		}
	}

	void Update()
	{
		foreach (var manager in _managers)
		{
			manager.OnUpdate();
		}
	}

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
	
	public GameObject GetNamedSceneObject(string name)
	{
		var hash = name.GetHashCode();
		if (_namedSceneObject.ContainsKey(hash) == false)
		{
			Debug.LogError("登録されていないSceneObjectを取得しようとしました Name=" + name);
			return null;
		}
		return _namedSceneObject[hash];
	}

	// 各マネージャ
	private List<ManagerBase> _managers = new List<ManagerBase>();

	// 名前付きシーンオブジェクト
	private Dictionary<int, GameObject> _namedSceneObject = new Dictionary<int, GameObject>();

	[SerializeField]
	private GameObject[] _sceneObjects;
}
