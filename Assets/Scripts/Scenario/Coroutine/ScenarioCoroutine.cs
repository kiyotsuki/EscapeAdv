using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScenarioUtil;

public partial class ScenarioCoroutine
{
	public static IEnumerator Opening()
	{
		ChangeMap("Entrance");
		ChangePlayer(ParamCharacter.ID.Momoka);
		yield return FadeIn(3);
		yield return new WaitForSeconds(1);
		yield return StartTalk("おぉ～…！");

		yield return new WaitForSeconds(1);

		yield return StartTalk("すごーい！\n本物の廃墟だよー！！");

		ChangePlayer(ParamCharacter.ID.Sakura);
		yield return StartTalk("あーもー\nまた勝手に入って・・・");
		yield return StartTalk("走ったら危ないでしょ！\nもっと足元とか、周囲に気を付けて・・・");
		
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
