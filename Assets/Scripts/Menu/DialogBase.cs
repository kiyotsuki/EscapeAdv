using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class DialogBase : GameItem
{
	public void Open(Action onEnd = null)
	{
		MenuUtil.AddBackScreen();
		In(onEnd);
	}

	public void Close(Action onEnd = null)
	{
		MenuUtil.RemoveBackScreen();
		Out(onEnd);
	}
}
