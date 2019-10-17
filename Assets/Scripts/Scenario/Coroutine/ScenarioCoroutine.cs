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

	public static IEnumerator TestEvent()
	{
		yield return StartTalk("モモカ", "移動してみるよ！");

		yield return RequestMove(ParamPlayer.ID.Momoka, 3, 0);

		yield return StartTalk("モモカ", "次はおねえちゃん！");

		yield return RequestMove(ParamPlayer.ID.Sakura, 10, 10);

		yield return StartTalk("モモカ", "全員集合！");

		RequestMove(ParamPlayer.ID.Sakura, 8, 5);
		RequestMove(ParamPlayer.ID.Momoka, 8, 5);
		yield return RequestMove(ParamPlayer.ID.Tsubaki, 8, 5);

	}
}
