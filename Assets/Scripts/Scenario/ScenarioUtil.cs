using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シナリオユーティリティ
/// </summary>
public static class ScenarioUtil
{
	public static ScenarioManager GetManager()
	{
		return GameUtil.GetManager<ScenarioManager>();
	}

	public static void ExecuteScenario(string scenario)
	{
		GetManager().ExecuteScenario(scenario);
	}

	public static IEnumerator FadeIn(float time = 0.3f)
	{
		var manager = GameUtil.GetManager<FadeManager>();
		manager.FadeIn(time);

		return new WaitUntil(() => {
			return manager.IsFading() == false;
		});
	}

	public static IEnumerator FadeOut(float time = 0.3f)
	{
		var manager = GameUtil.GetManager<FadeManager>();
		manager.FadeOut(time);

		return new WaitUntil(() => {
			return manager.IsFading() == false;
		});
	}

	public static void ChangeMap(string name)
	{
		var manager = GameUtil.GetManager<MapManager>();
		manager.ChangeMap(name);
	}

	public static IEnumerator StartTalk(string name, string text)
	{
		var talkWindow = GetManager().GetTalkWindow();
		talkWindow.gameObject.SetActive(true);
		talkWindow.SetName(name);
		talkWindow.SetText(text);

		return new WaitUntil(() => {
			return talkWindow.IsTalking() == false;
		});
	}

	public static IEnumerator AddTalk(string text)
	{
		var talkWindow = GetManager().GetTalkWindow();
		talkWindow.gameObject.SetActive(true);
		
		return new WaitUntil(() => {
			return talkWindow.IsTalking() == false;
		});
	}

	public static void HideTalk()
	{
		var talkWindow = GetManager().GetTalkWindow();
		talkWindow.gameObject.SetActive(false);
	}


}
