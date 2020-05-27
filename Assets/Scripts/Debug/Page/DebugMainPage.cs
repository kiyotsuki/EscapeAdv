using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMainPage : DebugManager.DebugPage
{
	public override void Open(DebugManager manager)
	{
		var adventureManager = GameUtil.GetManager<AdventureManager>();

		manager.AddButton("オープニング開始", ()=> {
			ScenarioUtil.ExecuteScenario(ScenarioCoroutine.Opening);
			manager.HideMenu();
		});

		manager.AddButton("オープニング後", () => {
			adventureManager.ChangeMap(GameDefine.MapId.Room01);
			manager.HideMenu();
		});


		manager.AddButton("フラグ", () => {
			manager.OpenPage(new DebugSaveFlagPage());
		});



	}
}
