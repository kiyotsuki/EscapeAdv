using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameUtil;

/// <summary>
/// シナリオユーティリティ
/// </summary>
public static class ScenarioUtil
{
	public static void ExecuteScenario(System.Func<IEnumerator> scenario)
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

	public static void ChangeMap(GameDefine.MapId id)
	{
		var saveData = GetSaveData();
		saveData.SetCurrentMap(id);
	}

	public static GameDefine.MapId GetCurrentMap()
	{
		var saveData = GetSaveData();
		return saveData.GetCurrentMap();
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

	
	public static IEnumerator Talk(string text, GameDefine.TalkType type = GameDefine.TalkType.Default)
	{
		var talkWindow = GetManager<ScenarioManager>().GetTalkWindow();
		if (type == GameDefine.TalkType.Default)
		{
			talkWindow.SetText(text);
		}
		else
		{
			if(type == GameDefine.TalkType.AddLine)
			{
				text = "\n" + text;
			}
			talkWindow.AddText(text);
		}

		return new WaitUntil(() => {
			if(talkWindow.IsTalking() == false)
			{
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


	public static byte GetFlag(GameDefine.SaveFlag id)
	{
		return GetSaveData().GetSaveFlag(id);
	}

	public static void SetFlag(GameDefine.SaveFlag id, byte flag)
	{
		GetSaveData().SetSaveFlag(id, flag);
	}


	public static IEnumerator Selection(string text0, string text1 = null, string text2 = null, string text3 = null)
	{
		var selectionWindow = GetManager<ScenarioManager>().GetSelectionWindow();
		return selectionWindow.StartSelection(text0, text1, text2, text3);
	}

	public static int GetSelectedIndex()
	{
		var selectionWindow = GetManager<ScenarioManager>().GetSelectionWindow();
		return selectionWindow.GetSelectedIndex();
	}
}

