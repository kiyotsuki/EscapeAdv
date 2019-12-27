using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ScenarioManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		_talkWindow.Hide();
		yield break;
	}
	
	/// <summary>
	/// シナリオを起動する
	/// ScenarioCoroutineクラスから該当の名前を探して実行する
	/// </summary>
	/// <param name="name"></param>
	public void ExecuteScenario(string scenario)
	{
		if(_running)
		{
			Debug.LogError("シナリオ実行中に別のシナリオを実行しようとしました Scenario=" + scenario);
			return;
		}

		StartCoroutine(executeScenario(scenario));
	}

	private IEnumerator executeScenario(string scenario)
	{
		System.Type type = typeof(ScenarioCoroutine);
		var method = type.GetMethod(scenario);
		if(method == null)
		{
			Debug.LogError("シナリオが見つかりません Scenario=" + scenario);
			yield break;
		}

		// シナリオ実行開始状態に
		_running = true;

		// シナリオ実行
		yield return method.Invoke(null, null);

		// 以下シナリオ実行終了

		var fadeManager = GameUtil.GetManager<FadeManager>();
		if (fadeManager.IsCoverd())
		{
			// 最後フェードかかってたら開ける
			yield return ScenarioUtil.FadeIn();
		}

		// トークを隠す
		_talkWindow.Hide();

		_running = false;
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
	/// シナリオ実行中か判定
	/// </summary>
	/// <returns></returns>
	public bool IsRunning()
	{
		return _running;
	}

	[SerializeField]
	TalkWindow _talkWindow = null;

	bool _running = false;

}
