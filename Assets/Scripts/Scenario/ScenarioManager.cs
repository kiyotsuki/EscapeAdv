using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class ScenarioManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		_talkWindow.Hide();
		yield break;
	}
	
	public void ExecuteScenario(System.Func<IEnumerator> scenario)
	{
		if (_running)
		{
			Debug.LogError("シナリオ実行中に別のシナリオを実行しようとしました Scenario=" + scenario);
			return;
		}
		StartCoroutine(executeScenario(scenario));
	}


	private IEnumerator executeScenario(System.Func<IEnumerator> scenario)
	{
		GameUtil.GetManager<AdventureManager>().OnStartEvent();
		
		// シナリオ実行開始状態に
		_running = true;

		// シナリオ実行
		yield return scenario();

		// 以下シナリオ実行終了
		var fadeManager = GameUtil.GetManager<FadeManager>();
		if (fadeManager.IsCoverd())
		{
			// 最後フェードかかってたら開ける
			yield return ScenarioUtil.FadeIn();
		}

		// トークを隠す
		_talkWindow.Hide();
		GameUtil.GetManager<AdventureManager>().OnEndEvent();

		_running = false;
	}

	/// <summary>
	/// シナリオ実行中か判定
	/// </summary>
	/// <returns></returns>
	public bool IsRunning()
	{
		return _running;
	}

	/// <summary>
	/// 会話用ウィンドウ取得
	/// </summary>
	/// <returns></returns>
	public TalkWindow GetTalkWindow()
	{
		return _talkWindow;
	}

	/// <summary>
	/// 選択肢ウィンドウ取得
	/// </summary>
	/// <returns></returns>
	public SelectionWindow GetSelectionWindow()
	{
		return _selectionWindow;
	}

	[SerializeField]
	TalkWindow _talkWindow = null;

	[SerializeField]
	SelectionWindow _selectionWindow = null;

	bool _running = false;

}
