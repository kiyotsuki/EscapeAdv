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

	public static void SetPlayer(ParamPlayer.ID id, int x, int y)
	{
		var manager = GameUtil.GetManager<PlayerManager>();
		manager.SetPlayer(id, x, y);
	}

	public static void ChangePlayer(ParamPlayer.ID id)
	{
		var manager = GameUtil.GetManager<PlayerManager>();
		manager.ChangePlayer(id);
	}

	public static IEnumerator StartTalk(string text)
	{
		var talkWindow = GetManager().GetTalkWindow();
		var currentPlayer = GameUtil.GetPlayerController();

		talkWindow.SetName(currentPlayer.GetName());
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

	public static IEnumerator RequestMove(ParamPlayer.ID id, int cx, int cy)
	{
		var playerManager = GameUtil.GetManager<PlayerManager>();
		var controller = playerManager.GetPlayerController(id);
		controller.RequestMove(cx, cy);

		return new WaitUntil(() => {
			return controller.IsMoving() == false;
		});
	}
	
}
