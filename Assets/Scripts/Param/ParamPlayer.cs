using UnityEngine;

/// <summary>
/// ゲーム内パラメータ
/// ParamGeneratorによって自動出力
/// </summary>
public class ParamPlayer
{
	public enum ID
	{
		Invalid = -1,
		Momoka = 0,
		Sakura = 1,
		Tsubaki = 2,
	}
	
	public class Data
	{
		public Data(ParamPlayer.ID Id, string Name, string IconName, string ImageName, ParamHeart.ID InitHeart)
		{
			this.Id = Id;
			this.Name = Name;
			this.IconName = IconName;
			this.ImageName = ImageName;
			this.InitHeart = InitHeart;
		}
		
		public ParamPlayer.ID Id { get; }
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
