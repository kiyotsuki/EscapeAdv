using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScenarioUtil;

public partial class ScenarioCoroutine
{
	public static IEnumerator Opening()
	{
		ChangeMap("TestMap");
		SetPlayer(ParamPlayer.ID.Momoka, 6, 10);
		yield return FadeIn(3);
		yield return new WaitForSeconds(1);
		yield return StartTalk("おぉ～…！");

		yield return RequestMove(ParamPlayer.ID.Momoka, 6, 5);
		yield return new WaitForSeconds(1);

		yield return StartTalk("すごーい！\n本物の廃墟だよー！！");
		
		SetPlayer(ParamPlayer.ID.Sakura, 6, 10);
		yield return StartTalk("あーもー\nまた勝手に入って・・・");
		yield return StartTalk("走ったら危ないでしょ！\nもっと足元とか、周囲に気を付けて・・・");

		ChangePlayer(ParamPlayer.ID.Momoka);
		yield return RequestMove(ParamPlayer.ID.Momoka, 3, 5);
		yield return StartTalk("うわぁ、壁とかすっごいボロボロ！\n");
		yield return AddTalk("テンションあがるぅ！");

		ChangePlayer(ParamPlayer.ID.Sakura);
		yield return StartTalk("あー・・・");
		yield return AddTalk("こりゃ\n全く聞いてないな・・・");

		SetPlayer(ParamPlayer.ID.Tsubaki, 7, 10);
		yield return StartTalk("ふふっ\n");
		yield return AddTalk("相変わらず元気ですね、モモカちゃんは。");

		ChangePlayer(ParamPlayer.ID.Sakura);
		yield return StartTalk("はぁ、\nツバキもわざわざ\nついてこなくてもよかったのに。");
		yield return StartTalk("こんなの見ても\n楽しくないでしょー？");

		ChangePlayer(ParamPlayer.ID.Tsubaki);
		yield return StartTalk("いえいえ、私もちょっと興味があったんです。");
		yield return StartTalk("老朽化した建物って、\nなんだか歴史を感じませんか？");

		ChangePlayer(ParamPlayer.ID.Momoka);
		yield return RequestMove(ParamPlayer.ID.Momoka, 7, 7);
		yield return StartTalk("さすがツバキちゃん！\n");
		yield return AddTalk("廃墟はロマンだよねっ！");

		ChangePlayer(ParamPlayer.ID.Tsubaki);
		yield return StartTalk("ふふっ\n");
		yield return AddTalk("そうですね。");

		ChangePlayer(ParamPlayer.ID.Sakura);
		yield return StartTalk("わかった、わかったわよ\n");
		yield return AddTalk("でも危ないことは絶対ダメなんだからね！");
		yield return StartTalk("もしちょっとでも危険そうなら・・・");

		ChangePlayer(ParamPlayer.ID.Momoka);
		yield return StartTalk("あーもーわかってますぅー！\n");
		yield return AddTalk("ちゃんと注意するから！");
		yield return StartTalk("お姉ちゃんはノリ悪いんだからなぁー");

		yield break;
	}

	public static IEnumerator TestEvent()
	{
		yield return StartTalk("移動してみるよ！");

		yield return RequestMove(ParamPlayer.ID.Momoka, 3, 0);

		yield return StartTalk("次はおねえちゃん！");

		yield return RequestMove(ParamPlayer.ID.Sakura, 10, 10);

		yield return StartTalk("全員集合！");

		RequestMove(ParamPlayer.ID.Sakura, 8, 5);
		RequestMove(ParamPlayer.ID.Momoka, 8, 5);
		yield return RequestMove(ParamPlayer.ID.Tsubaki, 8, 5);

	}
}
