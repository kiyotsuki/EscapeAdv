using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMainPage : DebugManager.DebugPage
{
	public override void Open(DebugManager manager)
	{
		manager.AddButton("シナリオ", ()=> {
			manager.OpenPage(new DebugScenarioPage());
		});
		manager.AddButton("フラグ", () => {
			manager.OpenPage(new DebugSaveFlagPage());
		});
	}
}
