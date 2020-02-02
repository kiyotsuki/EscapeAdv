using UnityEngine;

/// <summary>
/// ゲーム内パラメータ
/// ParamGeneratorによって自動出力
/// </summary>
public class ParamHeart
{
	public enum ID
	{
		Invalid = -1,
		Curious = 0,
		Serious = 1,
		Cool = 2,
		Doubt = 3,
		Scare = 4,
		Brave = 5,
	}
	
	public static int Count
	{
		get { return 6; }
	}
	
	public class Data
	{
		public Data(ParamHeart.ID Id, string Name)
		{
			this.Id = Id;
			this.Name = Name;
		}
		
		public ParamHeart.ID Id { get; }
		public string Name { get; }
	}
	
	private static readonly Data[] data = 
	{
		new Data((ID)0, "好奇心"),
		new Data((ID)1, "責任感"),
		new Data((ID)2, "冷静"),
		new Data((ID)3, "懐疑心"),
		new Data((ID)4, "恐怖心"),
		new Data((ID)5, "勇敢"),
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
	
}
