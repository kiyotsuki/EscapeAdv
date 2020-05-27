using UnityEngine;
using System;
using UnityEngine.UI;

public class ItemUseDialog : DialogContentBase
{
	public void Start()
	{
		_checkButton.onClick.AddListener(() =>
		{
			Close(() =>
			{
				//ScenarioUtil.ExecuteScenario("CheckItem_" + _item);
			});
		});

		_useButton.onClick.AddListener(() =>
		{
			Close(() =>
			{
				//ScenarioUtil.ExecuteScenario("UseItem_" + _item);
			});
		});
	}

	public void Setup(ParamItem.ID item)
	{
		_item = item;
		var data = ParamItem.Get(item);

		_itemName.text = data.Name;
		_itemDesc.text = data.Desc;
	}

	ParamItem.ID _item = ParamItem.ID.NONE;

	[SerializeField]
	private Text _itemName, _itemDesc;

	[SerializeField]
	private Image _itemIcon;

	[SerializeField]
	private Button _checkButton, _useButton;
}
