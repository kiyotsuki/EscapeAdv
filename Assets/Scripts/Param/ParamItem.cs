using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ゲーム内パラメータ
/// ParamGeneratorによって自動出力
/// </summary>
public class ParamItem
{
	public enum ID
	{
		NONE = -1,
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
	
	public static int Count
	{
		get { return data.Length; }
	}
	
	public static Data Get(ID id)
	{
		return Get((int)id);
	}
	public static Data Get(int index)
	{
		if (index < 0 || data.Length <= index) return null;
		return data[index];
	}
	
	public static List<Data> GetList(ID id)
	{
		return GetList((int)id);
	}
	public static List<Data> GetList(int index)
	{
		if (index < 0 || data.Length <= index) return null;
		var list = new List<Data>();
		for (int i = index; i < data.Length; i++)
		{
			if (data[i] == null) break;
			list.Add(data[i]);
		}
		return list;
	}
}
