using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScenarioUtil;

public partial class ScenarioCoroutine
{
	public static IEnumerator Opening()
	{
		yield return FadeIn(3);

		yield return StartTalk("モモカ", "おぉ～、これはこれは・・・");

		yield return AddTalk("なかなかのものではないですかねっ");

		yield return StartTalk("サクラ", "なによ、どうしたの？");

		HideTalk();

		yield return FadeOut(3);

		yield break;
	}
}
