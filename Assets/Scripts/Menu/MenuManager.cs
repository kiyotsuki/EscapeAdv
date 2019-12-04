using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : ManagerBase
{
	public override void Initialize()
	{
		_menuCanvas = GameUtil.GetNamedSceneObject("MenuCanvas");

		var itemButtonObject = GameUtil.GetNamedSceneObject("ItemButton");
		var itemButton = itemButtonObject.GetComponent<Button>();

		itemButton.onClick.AddListener(onClickItemMenu);

		var trans = _menuCanvas.transform;
		var itemMenuObject = trans.Find("ItemMenu");
		_itemMenu = itemMenuObject.GetComponent<ItemMenu>();
	}

	public void onClickItemMenu()
	{
		var list = new List<ParamItem.ID>();
		for (int i = 0; i < ParamItem.Count; i++)
		{
			list.Add((ParamItem.ID)i);
		}
		_itemMenu.Open(list);
	}

	public void CloseMenu()
	{
		_itemMenu.gameObject.SetActive(false);
	}

	GameObject _menuCanvas = null;

	ItemMenu _itemMenu = null;
}
