using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class DialogBase : GameItem
{
	public void Open()
	{
		MenuUtil.AddBackScreen();
		In();
	}

	public void Close()
	{
		MenuUtil.RemoveBackScreen();
		Out();
	}

	public void Decide(Action onDecide)
	{
		MenuUtil.RemoveBackScreen();
		Out(onDecide);
	}

	protected override void OnStartAnimation()
	{
		MenuUtil.AddTouchFilter();
	}

	protected override void OnEndAnimation()
	{
		MenuUtil.RemoveTouchFilter();
	}
}
