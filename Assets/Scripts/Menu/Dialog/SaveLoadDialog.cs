using UnityEngine;
using System;
using UnityEngine.UI;

public class SaveLoadDialog : DialogContentBase
{
	public void Start()
	{
		_closeButton.onClick.AddListener(() =>
		{
			Close(null);
		});
	}

	public void Setup()
	{
		var saveManager = GameUtil.GetManager<SaveManager>();

		for (int i = 0; i < _saveItems.Length; i++)
		{
			var saveLabel = saveManager.GetSaveLabel(i);
			_saveItems[i].Setup(i, saveLabel);
		}
	}
	
	[SerializeField]
	private SaveItem[] _saveItems;

	[SerializeField]
	private Button _closeButton;
}
