using UnityEngine;

/// <summary>
/// ゲーム内パラメータ
/// ParamGeneratorによって自動出力
/// </summary>
public class ParamCard
{
	public enum ID
	{
		Invalid = -1,
		Tap1 = 0,
		Tap5 = 1,
		Tap10 = 2,
		RapidTap = 3,
		HighSpeed = 4,
		TimeStop = 5,
		DoubleSkill = 6,
		TradeOff = 7,
		LastAttack = 8,
		SilentWorld = 9,
	}
	
	public class Data
	{
		public Data(string Name, string Desc, int Rate)
		{
			this.Name = Name;
			this.Desc = Desc;
			this.Rate = Rate;
		}
		
		public string Name { get; }
		public string Desc { get; }
		public int Rate { get; }
	}
	
	private static readonly Data[] data = 
	{
		new Data("タップ＋１", "タップ数を1増やす", 20),
		new Data("タップ＋５", "タップ数を5増やす", 20),
		new Data("タップ＋１０", "タップ数を10増やす", 20),
		new Data("ラピッドタップ", "このゲーム中、タップするたびに追加で1タップする", 8),
		new Data("紫電一閃", "次の一回だけ、タップ数を20倍にする", 8),
		new Data("明鏡止水", "5秒間時間を止める", 8),
		new Data("二重詠唱", "次に使う能力を2回使用する", 5),
		new Data("等価交換", "このゲーム中、タップ数が2倍になり、残り時間の半分を失う", 5),
		new Data("終焉の鐘", "残り時間が5秒以下である間、タップ数が5倍になる", 4),
		new Data("サイレントワールド", "時間の経過速度を半減させる", 2),
	};
	
	public static Data Get(int id)
	{
		if( id < 0 || data.Length <= id ) return null;
		return data[id];
	}
	
	public static Data Get(ID id)
	{
		return Get((int)id);
	}
	
	public static int Count
	{
		get { return data.Length; }
	}
}
