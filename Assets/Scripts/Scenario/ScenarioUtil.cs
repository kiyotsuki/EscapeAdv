using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameUtil;

/// <summary>
/// シナリオユーティリティ
/// </summary>
public static class ScenarioUtil
{
	public static void ExecuteScenario(string scenario)
	{
		GetManager<ScenarioManager>().ExecuteScenario(scenario);
	}

	public static bool IsInScenario()
	{
		return GetManager<ScenarioManager>().IsRunning();
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

	public static void ChangePlayer(ParamCharacter.ID id)
	{
		var adventureManager = GameUtil.GetManager<AdventureManager>();
		adventureManager.SetCurrentPlayer(id);
	}

	public static void SetPlayerActives(bool momoka, bool sakura, bool tsubaki)
	{
		var adventureManager = GameUtil.GetManager<AdventureManager>();
		adventureManager.SetPlayerActive(ParamCharacter.ID.Momoka, momoka);
		adventureManager.SetPlayerActive(ParamCharacter.ID.Sakura, momoka);
		adventureManager.SetPlayerActive(ParamCharacter.ID.Tsubaki, momoka);
	}
	
	public static IEnumerator StartTalk(string text)
	{
		var talkWindow = GetManager<ScenarioManager>().GetTalkWindow();
		
		talkWindow.SetText(text);

		return new WaitUntil(() => {
			if(talkWindow.IsTalking() == false)
			{
				talkWindow.Hide();
				return true;
			}
			return false;
		});
	}

	public static IEnumerator AddTalk(string text)
	{
		var talkWindow = GetManager<ScenarioManager>().GetTalkWindow();
		talkWindow.AddText(text);

		return new WaitUntil(() => {
			if (talkWindow.IsTalking() == false)
			{
				talkWindow.Hide();
				return true;
			}
			return false;
		});
	}

	public static void HideTalk()
	{
		var talkWindow = GetManager<ScenarioManager>().GetTalkWindow();
		talkWindow.Hide();
	}
}
