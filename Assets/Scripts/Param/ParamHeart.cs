using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ゲーム内パラメータ
/// ParamGeneratorによって自動出力
/// </summary>
public class ParamHeart
{
	public enum ID
	{
		NONE = -1,
		Curious = 0,
		Serious = 1,
		Cool = 2,
		Doubt = 3,
		Scare = 4,
		Brave = 5,
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
