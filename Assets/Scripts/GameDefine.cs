using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム定数
/// </summary>
public static class GameDefine
{
	public enum SaveFlag
	{
		Room01_CheckAround,
		Room01_CheckBed,
		Room01_CheckDesk,
		Room01_CheckDoor,

		TestA,
		TestB,
		TestC,

		Max
	}

	public enum MapId
	{
		Room01,
		Room02,
		Room03,

		Max
	}

	public enum TalkType
	{
		Default,
		Add,
		AddLine,
	}

}
