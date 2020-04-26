using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ゲーム内パラメータ
/// ParamGeneratorによって自動出力
/// </summary>
public class ParamCharacter
{
	public enum ID
	{
		NONE = -1,
		Momoka = 0,
		Sakura = 1,
		Tsubaki = 2,
	}
	
	public class Data
	{
		public Data(ParamCharacter.ID Id, string Name, string IconName, string ImageName, ParamHeart.ID InitHeart)
		{
			this.Id = Id;
			this.Name = Name;
			this.IconName = IconName;
			this.ImageName = ImageName;
			this.InitHeart = InitHeart;
		}
		
		public ParamCharacter.ID Id { get; }
		public string Name { get; }
		public string IconName { get; }
		public string ImageName { get; }
		public ParamHeart.ID InitHeart { get; }
	}
	
	private static readonly Data[] data = 
	{
		new Data((ID)0, "モモカ", "IconMomoka", "ImageMomoka", ParamHeart.ID.Curious),
		new Data((ID)1, "サクラ", "IconSakura", "ImageSakura", ParamHeart.ID.Serious),
		new Data((ID)2, "ツバキ", "IconTsubaki", "ImageTsubaki", ParamHeart.ID.Cool),
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
