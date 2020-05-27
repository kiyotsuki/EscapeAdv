using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScenarioUtil;
using static GameDefine;
using UnityEditor;

public partial class ScenarioCoroutine
{
	public static IEnumerator Opening()
	{
		ChangeMap(MapId.Room01);
		ChangePlayer(ParamCharacter.ID.Momoka);

		yield return FadeIn(3);
		yield return new WaitForSeconds(1);
		yield return Talk("う～ん……");

		yield return Talk("ふあぁ。");
		yield return Talk("あれ、寝ちゃってた……", TalkType.AddLine);

		yield return Talk("って、う、うん？");
		yield return Talk("うわーどこだここ、真っ暗じゃん！", TalkType.AddLine);

		yield return Talk("……私、");
		yield return Talk("何してたんだっけ……？", TalkType.AddLine);

		yield break;
	}

	/// <summary>
	/// 周りを見る
	/// </summary>
	public static IEnumerator Room01_Around()
	{
		if (GetFlag(SaveFlag.Room01_CheckAround) == 0)
		{
			yield return Talk("う～ん、暗いなぁ……");

			yield return Talk("とりあえず、\n私はベッドで寝てたみたい。");
			yield return Talk("ほかには、ちょっと小さめの机があるのと、ドアが一つみえるのと……");
			yield return Talk("部屋の隅に、カーテンのかかった場所があるね。\nなんだろあれ？");

			SetFlag(SaveFlag.Room01_CheckAround, 1);
			yield break;
		}

		if (GetFlag(SaveFlag.Room01_CheckBed) == 1)
		{
			yield return Talk("なんか木の板散ってると思ったら、\n天井に穴が開いてるー！");

			yield return Talk("ははぁ、だからちょこっと薄明るいんだな。");
			yield return Talk("でもこの感じだと、外も夜かな……", TalkType.AddLine);

			yield return Talk("それにしても危ないなぁ。");
			yield return Talk("こんなとこに穴が空いてたら、だれか落ちちゃうかもしれないじゃん～", TalkType.AddLine);

			yield return Talk("……");
			yield return Talk("…………", TalkType.AddLine);

			yield return Talk("これ私落ちたやつだーーー！！！");

			yield return Talk("なんかちょっと思い出してきたもん！");
			yield return Talk("も～～落っこちた先で寝落ちなんてどうかしてるよ！大物だよ！", TalkType.AddLine);

			yield return Talk("早いとこ上に戻んなきゃ！", TalkType.AddLine);

			SetFlag(SaveFlag.Room01_CheckBed, 2);
			SetFlag(SaveFlag.Room01_CheckAround, 2);
			yield break;
		}

		yield return Talk("う～ん、もう特に気になるものはないかな？");
		yield break;
	}


	public static IEnumerator Room01_Bed()
	{
		if (GetFlag(SaveFlag.Room01_CheckBed) == 0)
		{
			yield return Talk("保健室にあるみたいなパイプベッドかな。");

			yield return Talk("寝心地は微妙！", TalkType.AddLine);

			yield return Talk("あれ、よく見たらベッドの上に砕けた木みたいなのが散らばってる。");
			yield return Talk("なんだろこの板……？");
			SetFlag(SaveFlag.Room01_CheckBed, 1);
			yield break;
		}

		if (GetFlag(SaveFlag.Room01_CheckBed) == 1)
		{
			yield return Talk("ベッドの上、砕けた木の板みたいなのが散らばってる。");
			yield return Talk("なんだろこの板？");
			yield break;
		}

		yield return Talk("ベッドの上、砕けた木の板が散らばってる。");
		yield return Talk("私が上から落っこちてきたときのヤツだ……");
		yield break;
	}

	public static IEnumerator Room01_Desk()
	{
		if (GetFlag(SaveFlag.Room01_CheckDesk) == 0)
		{
			yield return Talk("引き出しが一つだけある、小さな机。");
			yield return Talk("これなら私の机のほうがちょっと大きいかな。", TalkType.AddLine);

			yield return Talk("上には工具箱？みたいなのが置いてある。");

			yield return Talk("引き出しの中身は……\n折り紙に……色鉛筆？");
			yield return Talk("子供用かなぁ。");

			SetFlag(SaveFlag.Room01_CheckDesk, 1);
			yield break;
		}

		yield return Talk("上には工具箱、\n引き出しには折り紙とか色鉛筆。");
		yield return Talk("子供の患者さん用だったのかなぁ。");
		yield return Talk("わりに大人っぽい部屋だけど……", TalkType.AddLine);
		yield break;
	}

	public static IEnumerator Room01_Ceiling()
	{
		yield return Talk("穴だ！");
		yield break;
	}

	public static IEnumerator Room01_InnerDoor()
	{
		yield return Talk("トイレだ！");
		yield break;
	}

	public static IEnumerator Room01_ExitDoor()
	{
		yield return Talk("引き戸タイプのドアだね。");
		yield return Talk("とりあえず、部屋の外にでようかな……？");

		yield return Selection("外に出る", "まだ出ない");

		if(GetSelectedIndex() == 0)
		{
			yield return Talk("よし、行こう！");
		}
		else
		{
			yield return Talk("まだまってよう！");
		}

		yield return Talk("って、あれ？開かない？", TalkType.AddLine);

		yield return Talk("ありゃー、ドアの下に何か針金みたいのが挟まってる。");
		yield return Talk("これのせいかぁ。どうにか引っこ抜けないかな……");

		yield break;

		yield return Talk("う～ん、素手では抜けなさそう。\n何か道具がいるなぁ。");

	}


	public static IEnumerator CheckItem_Key()
	{
		yield return Talk("これは・・・鍵だね！");
	}

	public static IEnumerator UseItem_Key()
	{
		yield return Talk("鍵をつかったよ");
	}
}
