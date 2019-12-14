using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
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

	[SerializeField]
	GameObject _hudCanvas;

	[SerializeField]
	Button _itemMenuButton;

	ParamItem.ID _usingItem = ParamItem.ID.Invalid;
}
