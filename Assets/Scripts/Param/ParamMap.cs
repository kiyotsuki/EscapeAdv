using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ゲーム内パラメータ
/// ParamGeneratorによって自動出力
/// </summary>
public class ParamMap
{
	public enum ID
	{
		NONE = -1,
		WAITING_ROOM = 0,
		EXAMINATION_ROOM = 1,
		TREATMENT_ROOM = 2,
		OFFICE_ROOM = 3,
		STORE_ROOM = 4,
		DIRECTOR_ROOM = 5,
	}
	
	public class Data
	{
		public Data(ParamMap.ID Id, string Name, ParamMapItem.ID ItemList)
		{
			this.Id = Id;
			this.Name = Name;
			this.ItemList = ItemList;
		}
		
		public ParamMap.ID Id { get; }
		public string Name { get; }
		public ParamMapItem.ID ItemList { get; }
	}
	
	private static readonly Data[] data = 
	{
		new Data((ID)0, "待合室", ParamMapItem.ID.WR_AROUND),
		new Data((ID)1, "診察室", ParamMapItem.ID.ER_AROUND),
		new Data((ID)2, "治療室", ParamMapItem.ID.NONE),
		new Data((ID)3, "事務室", ParamMapItem.ID.NONE),
		new Data((ID)4, "倉庫", ParamMapItem.ID.NONE),
		new Data((ID)5, "院長室", ParamMapItem.ID.NONE),
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
