using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameDefine;
using static ScenarioUtil;
using static ScenarioCoroutine;

public partial class AdventureManager
{
	public void SetupMap(MapId map)
	{
		ClearMapItems();
		switch (map)
		{
			case MapId.Room01:
				setupRoom01();
				break;
		}
	}

	private void setupRoom01()
	{
		SetMapName("真っ暗な部屋");

		AddMapItem("周りを見る", "icon_eye", Room01_Around);

		var count = GetFlag(SaveFlag.Room01_CheckAround);
		if (count >= 1)
		{
			AddMapItem("ベッドを見る", "icon_point", Room01_Bed);
			if (count >= 2)
			{
				AddMapItem("天井を見る", "icon_point", Room01_Ceiling);
			}
			AddMapItem("机を見る", "icon_point", Room01_Desk);
			AddMapItem("カーテンを見る", "icon_point", Room01_InnerDoor);
			AddMapItem("ドアを見る", "icon_door", Room01_ExitDoor);
		}


	}
}
