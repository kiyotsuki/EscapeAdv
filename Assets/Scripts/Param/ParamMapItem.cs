using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ゲーム内パラメータ
/// ParamGeneratorによって自動出力
/// </summary>
public class ParamMapItem
{
	public enum ID
	{
		NONE = -1,
		WR_AROUND = 0,
		WR_COUNTER = 1,
		WR_SHELF = 2,
		WR_SOFA = 3,
		WR_DOOR_ER = 4,
		WR_DOOR_EXIT = 5,
		ER_AROUND = 6,
		ER_DESK = 7,
		ER_MACHINE = 8,
		ER_WINDOW = 9,
		ER_BED = 10,
		ER_DOOR_WR = 11,
		ER_DOOR_OR = 12,
	}
	
	public class Data
	{
		public Data(ParamMapItem.ID Id, string Name, int IconIndex)
		{
			this.Id = Id;
			this.Name = Name;
			this.IconIndex = IconIndex;
		}
		
		public ParamMapItem.ID Id { get; }
		public string Name { get; }
		public int IconIndex { get; }
	}
	
	private static readonly Data[] data = 
	{
		new Data((ID)0, "周りを見回す", 0),
		new Data((ID)1, "カウンター", 1),
		new Data((ID)2, "雑誌棚", 1),
		new Data((ID)3, "古びたソファー", 1),
		new Data((ID)4, "引き戸式のドア", 2),
		new Data((ID)5, "外へのドア", 2),
		null,
		new Data((ID)7, "周りを見回す", 0),
		new Data((ID)8, "診察机", 1),
		new Data((ID)9, "大きな機械", 1),
		new Data((ID)10, "ひび割れた窓", 1),
		new Data((ID)11, "ベッド", 1),
		new Data((ID)12, "待合室へのドア", 2),
		new Data((ID)13, "奥のドア", 2),
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
