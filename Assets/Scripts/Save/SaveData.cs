using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class SaveData
{
	// 現在のマップ
	private ParamMap.ID _currentMap;

	// 各キャラクターごとの状態
	private CharaStatus[] _charaStatuses = new CharaStatus[ParamCharacter.Count];
	
	// 各キャラクターごとの所持アイテム
	private List<ParamItem.ID>[] _hasItems = new List<ParamItem.ID>[]{
		new List<ParamItem.ID>(),
		new List<ParamItem.ID>(),
		new List<ParamItem.ID>()
	};

	// ゲーム進行フラグ
	private bool[] _saveFlags = new bool[(int)SaveFlag.Max];


	public enum SaveFlag
	{
		TestA,
		TestB,
		TestC,
		Max,
	}

	public enum CharaStatus
	{
		Normal,
		Scare,
		Brave,
		Inactive,
	}

	public SaveData()
	{
		_charaStatuses[2] = CharaStatus.Inactive;
	}

	public void SetCharaStatus(ParamCharacter.ID id, CharaStatus status)
	{
		_charaStatuses[(int)id] = status;
	}

	public CharaStatus GetCharaStatus(ParamCharacter.ID id)
	{
		return _charaStatuses[(int)id];
	}

	public bool GetSaveFlag(SaveFlag id)
	{
		return _saveFlags[(int)id];
	}

	public void SetSaveFlag(SaveFlag id, bool flag)
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

	public void SetCurrentMap(ParamMap.ID map, bool momoka, bool sakura, bool tsubaki)
	{
		_currentMap = map;
	}

	public ParamMap.ID GetCurrentMap()
	{
		return _currentMap;
	}

	public void Save(BinaryWriter writer)
	{
		writer.Write((byte)_currentMap);

		for (int i = 0; i < _charaStatuses.Length; i++)
		{
			writer.Write((byte)_charaStatuses[i]);
		}

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
		_currentMap = (ParamMap.ID) reader.ReadByte();

		for (int i = 0; i < _charaStatuses.Length; i++)
		{
			_charaStatuses[i] = (CharaStatus)reader.ReadByte();
		}

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
			_saveFlags[i] = reader.ReadBoolean();
		}
	}
}

