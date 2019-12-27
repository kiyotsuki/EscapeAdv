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

	public static bool IsInScenario()
	{
		return GetManager().IsRunning();
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

	public static void ChangePlayer(ParamPlayer.ID id)
	{
		var adventureManager = GameUtil.GetManager<AdventureManager>();
		adventureManager.SetCurrentPlayer(id);
	}

	public static void SetPlayerActives(bool momoka, bool sakura, bool tsubaki)
	{
		var adventureManager = GameUtil.GetManager<AdventureManager>();
		adventureManager.SetPlayerActive(ParamPlayer.ID.Momoka, momoka);
		adventureManager.SetPlayerActive(ParamPlayer.ID.Sakura, momoka);
		adventureManager.SetPlayerActive(ParamPlayer.ID.Tsubaki, momoka);
	}
	
	public static IEnumerator StartTalk(string text)
	{
		var talkWindow = GetManager().GetTalkWindow();
		
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
		var talkWindow = GetManager().GetTalkWindow();
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
		var talkWindow = GetManager().GetTalkWindow();
		talkWindow.Hide();
	}

	public static ParamItem.ID GetUseItem()
	{
		var adventureManager = GameUtil.GetManager<AdventureManager>();
		return adventureManager.GetUseItem();
	}
}
