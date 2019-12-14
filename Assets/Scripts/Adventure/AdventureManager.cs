using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		_useItemCanselButton.onClick.AddListener(() =>
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
			_useItemDisplayAnimator.SetTrigger("Out");
			return;
		}
		_useItemId = data.Id;
		_useItemNameLabel.text = data.Name;
		_useItemDisplayAnimator.SetTrigger("In");
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
	Animator _useItemDisplayAnimator;

	[SerializeField]
	Text _useItemNameLabel;

	[SerializeField]
	Button _useItemCanselButton;


	ParamItem.ID _useItemId = ParamItem.ID.Invalid;
}
