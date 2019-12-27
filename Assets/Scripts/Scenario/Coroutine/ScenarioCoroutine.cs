using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScenarioUtil;

public partial class ScenarioCoroutine
{
	public static IEnumerator Opening()
	{
		ChangeMap("Entrance");
		ChangePlayer(ParamPlayer.ID.Momoka);
		yield return FadeIn(3);
		yield return new WaitForSeconds(1);
		yield return StartTalk("おぉ～…！");

		yield return new WaitForSeconds(1);

		yield return StartTalk("すごーい！\n本物の廃墟だよー！！");

		ChangePlayer(ParamPlayer.ID.Sakura);
		yield return StartTalk("あーもー\nまた勝手に入って・・・");
		yield return StartTalk("走ったら危ないでしょ！\nもっと足元とか、周囲に気を付けて・・・");
		
		yield break;
	}

	public static IEnumerator TestEvent()
	{
		if (GetUseItem() == ParamItem.ID.Diary)
		{
			var adventureManager = GameUtil.GetManager<AdventureManager>();
			adventureManager.SetUseItem(null);

			yield return StartTalk("日記を使ったよ！");
		}
		else
		{
			ChangePlayer(ParamPlayer.ID.Momoka);
			yield return StartTalk("お、何かあるよ！");

			ChangePlayer(ParamPlayer.ID.Sakura);
			yield return StartTalk("どれどれ、見せてみなさい？");

			ChangePlayer(ParamPlayer.ID.Tsubaki);
			yield return StartTalk("何かわかりましたか？");
		}
	}

	public static IEnumerator NotFound()
	{
		yield return StartTalk("特になんもないなぁ");
	}


	public static IEnumerator CheckItem_Key()
	{
		yield return StartTalk("これは・・・鍵だね！");
	}
}
