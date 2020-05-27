using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class SaveData
{
	// 現在のマップ
	private GameDefine.MapId _currentMap;

	// 各キャラクターごとの所持アイテム
	private List<ParamItem.ID>[] _hasItems = new List<ParamItem.ID>[]{
		new List<ParamItem.ID>(),
		new List<ParamItem.ID>(),
		new List<ParamItem.ID>()
	};

	// ゲーム進行フラグ
	private byte[] _saveFlags = new byte[(int)GameDefine.SaveFlag.Max];

	public SaveData()
	{
	}

	public byte GetSaveFlag(GameDefine.SaveFlag id)
	{
		return _saveFlags[(int)id];
	}

	public void SetSaveFlag(GameDefine.SaveFlag id, byte flag)
	{
		_saveFlags[(int)id] = flag;
	}

	public bool HasItem(ParamCharacter.ID chara, ParamItem.ID item)
	{
		return _hasItems[(int)chara].Contains(item);
	}

	public void AddItem(ParamCharacter.ID chara, ParamItem.ID item, bool flag)
	{
		_hasItems[(int)chara].Add(item);
	}

	public List<ParamItem.ID> GetItems(ParamCharacter.ID chara)
	{
		return _hasItems[(int)chara];
	}

	public void SetCurrentMap(GameDefine.MapId id)
	{
		_currentMap =id;
	}

	public GameDefine.MapId GetCurrentMap()
	{
		return _currentMap;
	}

	public void Save(BinaryWriter writer)
	{
		writer.Write((byte)_currentMap);

		for (int i = 0; i < _hasItems.Length; i++)
		{
			var count = _hasItems[i].Count;
			writer.Write((byte)count);
			for (int j = 0; j < count; j++)
			{
				writer.Write((byte)_hasItems[i][j]);
			}
		}

		for (int i = 0; i < _saveFlags.Length; i++)
		{
			writer.Write(_saveFlags[i]);
		}
	}

	public void Load(BinaryReader reader)
	{
		_currentMap = (GameDefine.MapId) reader.ReadByte();

		for (int i = 0; i < _hasItems.Length; i++)
		{
			var count = reader.ReadByte();
			_hasItems[i].Clear();
			for (int j = 0; j < count; j++)
			{
				_hasItems[i].Add((ParamItem.ID)reader.ReadByte());
			}
		}

		for (int i = 0; i < _saveFlags.Length; i++)
		{
			_saveFlags[i] = reader.ReadByte();
		}
	}
}

