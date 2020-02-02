using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class SaveItem : MonoBehaviour
{
	[SerializeField]
	Text _slotLabel, _titleLabel, _playerLabel;

	[SerializeField]
	Button _saveButton, _loadButton, _deleteButton;

	int _slotNo;
	
	public void Start()
	{
		_saveButton.onClick.AddListener(onClickSave);
		_loadButton.onClick.AddListener(onClickLoad);
		_deleteButton.onClick.AddListener(onClickDelete);
	}

	public void Setup(int slotNo, string label)
	{
		_slotNo = slotNo;
		if(String.IsNullOrEmpty(label))
		{
			_slotLabel.text = $"【スロット{slotNo}】データなし";
			_titleLabel.text = "";
			_playerLabel.text = "";

			_loadButton.gameObject.SetActive(false);
			_deleteButton.gameObject.SetActive(false);
		}
		else
		{
			var split = label.Split(',');
			_slotLabel.text = $"【スロット{slotNo}】" + split[0];
			_titleLabel.text = split[1];
			_playerLabel.text = split[2];

			_loadButton.gameObject.SetActive(true);
			_deleteButton.gameObject.SetActive(true);
		}
	}

	private void onClickSave()
	{
		var menuManager = GameUtil.GetManager<MenuManager>();
		menuManager.OpenYesNoDialog($"スロット{_slotNo}にデータを上書きします。\nよろしいですか？", () =>
		{
			var saveManager = GameUtil.GetManager<SaveManager>();
			saveManager.SaveSlot(_slotNo);

			var label = saveManager.GetSaveLabel(_slotNo);
			Setup(_slotNo, label);

			menuManager.OpenOkDialog($"スロット{_slotNo}にデータを保存しました。");
		});
	}

	private void onClickLoad()
	{
		var menuManager = GameUtil.GetManager<MenuManager>();
		menuManager.OpenYesNoDialog($"スロット{_slotNo}のデータを読み込みます。\nよろしいですか？", () =>
		{
			var saveManager = GameUtil.GetManager<SaveManager>();
			saveManager.LoadSlot(_slotNo);
		});
	}

	private void onClickDelete()
	{
		var menuManager = GameUtil.GetManager<MenuManager>();
		menuManager.OpenYesNoDialog($"スロット{_slotNo}のデータを削除します。\nよろしいですか？", () =>
		{
			var saveManager = GameUtil.GetManager<SaveManager>();
			saveManager.DeleteSlot(_slotNo);

			Setup(_slotNo, "");

			menuManager.OpenOkDialog($"スロット{_slotNo}のデータを削除しました。");
		});
	}
}
