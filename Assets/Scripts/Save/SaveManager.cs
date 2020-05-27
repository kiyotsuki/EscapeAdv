using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveManager : ManagerBase
{
	private SaveData _saveData = new SaveData();
	private byte[] _saveBuffer = new byte[100000];

	protected override IEnumerator Setup()
	{
		yield break;
	}
	
	public void LoadSlot(int slot)
	{
		var code = PlayerPrefs.GetString($"Data[{slot}]");
		if(code == null)
		{
			// このスロットにセーブデータがなかった
			return;
		}

		var bytes = System.Convert.FromBase64String(code);
		var stream = new MemoryStream(bytes);

		_saveData.Load(new BinaryReader(stream));
		stream.Close();
		
		GameUtil.GetManager<AdventureManager>().OnLoad(_saveData);
	}

	public void SaveSlot(int slot)
	{
		GameUtil.GetManager<AdventureManager>().OnSave(_saveData);

		var stream = new MemoryStream(_saveBuffer);
		_saveData.Save(new BinaryWriter(stream));
		//stream.Close();

		var code = System.Convert.ToBase64String(_saveBuffer, 0, (int)stream.Position);
		PlayerPrefs.SetString($"Data[{slot}]", code);

		var now = System.DateTime.Now;
		var saveLabel = now.ToString() + ",";
		saveLabel += "MapName,";

		PlayerPrefs.SetString($"Label[{slot}]", saveLabel);

		PlayerPrefs.Save();
	}

	public void DeleteSlot(int slot)
	{
		PlayerPrefs.DeleteKey($"Data[{slot}]");
		PlayerPrefs.DeleteKey($"Label[{slot}]");
	}

	public SaveData GetSaveData()
	{
		return _saveData;
	}

	public string GetSaveLabel(int slot)
	{
		return PlayerPrefs.GetString($"Label[{slot}]");
	}
}
