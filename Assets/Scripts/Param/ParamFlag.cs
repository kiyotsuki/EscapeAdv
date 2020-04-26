using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ゲーム内パラメータ
/// ParamGeneratorによって自動出力
/// </summary>
public class ParamFlag
{
	public enum ID
	{
		NONE = -1,
		TEST = 0,
	}
	
	public class Data
	{
		public Data(ParamFlag.ID Id, bool InitialValue)
		{
			this.Id = Id;
			this.InitialValue = InitialValue;
		}
		
		public ParamFlag.ID Id { get; }
		public bool InitialValue { get; }
	}
	
	private static readonly Data[] data = 
	{
		new Data((ID)0, false),
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
