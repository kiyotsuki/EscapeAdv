using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScenarioPage : DebugManager.DebugPage
{
	public override void Open(DebugManager manager)
	{
		manager.AddButton("オープニング開始", ()=> {
			ScenarioUtil.ExecuteScenario(ScenarioCoroutine.Opening);
			manager.HideMenu();
		});
	}
}
