using UnityEngine;

/// <summary>
/// ゲーム内パラメータ
/// ParamGeneratorによって自動出力
/// </summary>
public class ParamTest
{
	public enum ID
	{
		Invalid = -1,
		Test1 = 0,
		Test2 = 1,
		Test3 = 2,
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
