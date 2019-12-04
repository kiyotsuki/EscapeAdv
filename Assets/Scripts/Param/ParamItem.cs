using UnityEngine;

/// <summary>
/// ゲーム内パラメータ
/// ParamGeneratorによって自動出力
/// </summary>
public class ParamItem
{
	public enum ID
	{
		Invalid = -1,
		Dummy = 0,
		Key = 1,
		Light = 2,
		Diary = 3,
	}
	
	public class Data
	{
		public Data(ParamItem.ID Id, string Name, string Desc)
		{
			this.Id = Id;
			this.Name = Name;
			this.Desc = Desc;
		}
		
		public ParamItem.ID Id { get; }
		public string Name { get; }
		public string Desc { get; }
	}
	
	private static readonly Data[] data = 
	{
		new Data((ID)0, "ダミーアイテム", "ダミーのアイテム"),
		new Data((ID)1, "どこかの鍵", "どこかの鍵。どこかで使える"),
		new Data((ID)2, "懐中電灯", "非常用の懐中電灯"),
		new Data((ID)3, "日記", "院長のものと思われる日記帳"),
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
