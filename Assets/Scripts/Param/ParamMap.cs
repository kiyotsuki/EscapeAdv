using UnityEngine;

/// <summary>
/// ゲーム内パラメータ
/// ParamGeneratorによって自動出力
/// </summary>
public class ParamMap
{
	public enum ID
	{
		Invalid = -1,
		Test = 0,
	}
	
	public static int Count
	{
		get { return 1; }
	}
	
	public class Data
	{
		public Data(ParamMap.ID Id, string Name, string Prefab)
		{
			this.Id = Id;
			this.Name = Name;
			this.Prefab = Prefab;
		}
		
		public ParamMap.ID Id { get; }
		public string Name { get; }
		public string Prefab { get; }
	}
	
	private static readonly Data[] data = 
	{
		new Data((ID)0, "テストマップ", "TestMap"),
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
