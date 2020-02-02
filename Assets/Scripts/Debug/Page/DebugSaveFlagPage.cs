using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSaveFlagPage : DebugManager.DebugPage
{
	Text[] _buttonTexts;

	public override void Open(DebugManager manager)
	{
		_buttonTexts = new Text[(int)SaveData.SaveFlag.Max];

		var saveManager = GameUtil.GetManager<SaveManager>();
		var saveData = saveManager.GetSaveData();
		for (int i = 0; i < (int)SaveData.SaveFlag.Max; i++)
		{
			var flag = (SaveData.SaveFlag)i;
			_buttonTexts[i] = manager.AddButton($"{flag}:{saveData.GetSaveFlag(flag)}", () =>
			{
				var b = !saveData.GetSaveFlag(flag);
				saveData.SetSaveFlag(flag, b);
				_buttonTexts[(int)flag].text = $"{flag}:{b}";
			});
		}
	}
}
