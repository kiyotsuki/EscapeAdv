using UnityEngine;
using System;
using UnityEngine.UI;

public class SaveLoadDialog : DialogContentBase
{
	private void Start()
	{
		_itemSource.gameObject.SetActive(false);
	}

	public void Setup(SaveData[] saveData)
	{
		foreach (var data in saveData)
		{
			var item = GameUtil.CreateInstance(_itemSource);
			item.Setup(data);
		}
	}

	[SerializeField]
	private SaveItem _itemSource;
}
