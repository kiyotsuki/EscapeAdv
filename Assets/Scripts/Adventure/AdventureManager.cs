using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		_useItemDisplay.AddButtonListener(() =>
		{
			SetUseItem(null);
		});
		yield break;
	}

	public override void OnStartGame()
	{
		_itemMenuButton.onClick.AddListener(() =>
		{
			var menuManager = GameUtil.GetManager<MenuManager>();
			menuManager.OpenInventoryMenu();
		});
	}

	public void SetUseItem(ParamItem.Data data)
	{
		if (data == null)
		{
			_useItemId = ParamItem.ID.Invalid;
			_useItemDisplay.SetAnimationTrigger("Out");
			return;
		}
		_useItemId = data.Id;
		_useItemDisplay.SetLabelText(data.Name);
		_useItemDisplay.SetAnimationTrigger("In");
	}

	public ParamItem.ID GetUseItemId()
	{
		return _useItemId;
	}

	[SerializeField]
	GameObject _hudCanvas;

	[SerializeField]
	Button _itemMenuButton;

	[SerializeField]
	GameItem _useItemDisplay;
	

	ParamItem.ID _useItemId = ParamItem.ID.Invalid;
}
