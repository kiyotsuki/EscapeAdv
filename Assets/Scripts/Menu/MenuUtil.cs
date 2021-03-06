﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameUtil;

/// <summary>
/// メニューユーティリティ
/// </summary>
public static class MenuUtil
{
	public static void AddBackScreen()
	{
		GetManager<MenuManager>().AddBackScreen();
	}

	public static void RemoveBackScreen()
	{
		GetManager<MenuManager>().RemoveBackScreen();
	}

	public static void AddTouchFilter()
	{
		GetManager<MenuManager>().AddTouchFilter();
	}

	public static void RemoveTouchFilter()
	{
		GetManager<MenuManager>().RemoveTouchFilter();
	}
}
