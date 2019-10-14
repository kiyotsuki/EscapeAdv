using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ScenarioManager : ManagerBase
{
	public override void Initialize()
	{
		_scenarioCanvas = GameUtil.GetNamedSceneObject("ScenarioCanvas");

		var talkWindowObject = GameUtil.FindChild(_scenarioCanvas, "TalkWindow");
		_talkWindow = talkWindowObject.GetComponent<TalkWindow>();
		talkWindowObject.SetActive(false);
	}		


	public override void OnInitialized()
	{
		ScenarioUtil.ChangeMap("TestMap");
		ScenarioUtil.SetPlayer(ParamPlayer.ID.Momoka, 10, 10);
		ScenarioUtil.SetPlayer(ParamPlayer.ID.Sakura, 11, 10);
		ScenarioUtil.SetPlayer(ParamPlayer.ID.Tsubaki, 12, 10);
	}

	/// <summary>
	/// シナリオを起動する
	/// ScenarioCoroutineクラスから該当の名前を探して実行する
	/// </summary>
	/// <param name="name"></param>
	public void ExecuteScenario(string scenario)
	{
		if(_runningScenario)
		{
			Debug.LogError("シナリオ実行中に別のシナリオを実行しようとしました Scenario=" + scenario);
			return;
		}
		GameUtil.StartCoroutine(executeScenario(scenario));
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

		// シナリオ実行
		_runningScenario = true;
		yield return method.Invoke(null, null);

		// 以下シナリオ実行終了

		var fadeManager = GameUtil.GetManager<FadeManager>();
		if (fadeManager.IsCoverd())
		{
			// 最後フェードかかってたら開ける
			yield return ScenarioUtil.FadeIn();
		}

		// トークを隠す
		ScenarioUtil.HideTalk();

		_runningScenario = false;
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
	public bool RunningScenario()
	{
		return _runningScenario;
	}

	bool _runningScenario = false;

	GameObject _scenarioCanvas = null;

	TalkWindow _talkWindow = null;
}
