using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class ItemDialog : DialogBase
{
	public void Start()
	{
		AddButtonListener(onClickCheck, 0);
		AddButtonListener(onClickUse, 1);
	}

	public void Setup(ParamItem.ID item)
	{
		_item = item;
		var data = ParamItem.Get(item);

		SetLabelText(data.Name, 0);
		SetLabelText(data.Desc, 1);
	}

	private void onClickCheck()
	{
		Close(() =>
		{
			ScenarioUtil.ExecuteScenario("CheckItem_" + _item);
		});
	}

	private void onClickUse()
	{
		Close(() =>
		{
			ScenarioUtil.ExecuteScenario("UseItem_" + _item);
		});
	}

	ParamItem.ID _item = ParamItem.ID.Invalid;
}
