using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScenarioUtil;

public partial class ScenarioCoroutine
{
	public static IEnumerator Opening()
	{
		//ChangeMap("Entrance");

		var adventureManager = GameUtil.GetManager<AdventureManager>();
		adventureManager.ClearMapItems();

		ChangePlayer(ParamCharacter.ID.Momoka);
		yield return FadeIn(3);
		yield return new WaitForSeconds(1);
		yield return StartTalk("おぉ～…！");

		yield return new WaitForSeconds(1);
		yield return StartTalk("すごーい！\n本物の廃墟だよー！！");

		ChangePlayer(ParamCharacter.ID.Sakura);
		yield return StartTalk("あーもー\nまた勝手に入って・・・");
		yield return StartTalk("走ったら危ないでしょ！\nもっと足元とか、周囲に気を付けて・・・");

		ChangePlayer(ParamCharacter.ID.Momoka);
		yield return StartTalk("やたー！\nさて、ふぉとじぇにっくな場所を探すよ！");

		ChangePlayer(ParamCharacter.ID.Sakura);
		yield return StartTalk("だめだこりゃ、聞いてないな・・・");

		adventureManager.AddMapItem("あたりを見回す", 0, WR_Around);

		yield break;
	}

	public static IEnumerator WR_Around()
	{
		ChangePlayer(ParamCharacter.ID.Momoka);
		yield return StartTalk("うーん・・・");
		yield return StartTalk("なんかこうかび臭いなぁ。");

		yield break;
	}


	public static IEnumerator TestEvent()
	{
		ChangePlayer(ParamCharacter.ID.Momoka);
		yield return StartTalk("お、何かあるよ！");

		ChangePlayer(ParamCharacter.ID.Sakura);
		yield return StartTalk("どれどれ、見せてみなさい？");

		ChangePlayer(ParamCharacter.ID.Tsubaki);
		yield return StartTalk("何かわかりましたか？");
	}

	public static IEnumerator NotFound()
	{
		yield return StartTalk("特になんもないなぁ");
	}


	public static IEnumerator CheckItem_Key()
	{
		yield return StartTalk("これは・・・鍵だね！");
	}

	public static IEnumerator UseItem_Key()
	{
		yield return StartTalk("鍵をつかったよ");
	}
}
