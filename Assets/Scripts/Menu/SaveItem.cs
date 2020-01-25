using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class SaveItem : MonoBehaviour
{
	public void Start()
	{
		_saveButton.onClick.AddListener(onClickSave);
		_loadButton.onClick.AddListener(onClickLoad);
		_deleteButton.onClick.AddListener(onClickDelete);
	}

	public void Setup(int slotNo)
	{
		Setup(slotNo, $"スロット{slotNo}:データなし", "", "");
		_loadButton.gameObject.SetActive(false);
		_deleteButton.gameObject.SetActive(false);
	}

	public void Setup(int slotNo, string slotLabel, string titleLabel, string playerLabel)
	{
		_slotNo = slotNo;

		_slotLabel.text = slotLabel;
		_titleLabel.text = titleLabel;
		_playerLabel.text = playerLabel;

		_loadButton.gameObject.SetActive(true);
		_deleteButton.gameObject.SetActive(true);
	}

	private void onClickSave()
	{
		var menuManager = GameUtil.GetManager<MenuManager>();
		menuManager.OpenYesNoDialog($"スロット{_slotNo}にデータを上書きします。\nよろしいですか？", null);
	}

	private void onClickLoad()
	{
		var menuManager = GameUtil.GetManager<MenuManager>();
		menuManager.OpenYesNoDialog($"スロット{_slotNo}のデータを読み込みます。\nよろしいですか？", () =>
		{

		});
	}

	private void onClickDelete()
	{
		var menuManager = GameUtil.GetManager<MenuManager>();
		menuManager.OpenYesNoDialog($"スロット{_slotNo}のデータを削除します。\nよろしいですか？", () =>
		{

		});
	}


	[SerializeField]
	Text _slotLabel, _titleLabel, _playerLabel;

	[SerializeField]
	Button _saveButton, _loadButton, _deleteButton;

	int _slotNo;
}
