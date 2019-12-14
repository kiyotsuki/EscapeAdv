using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		var go = GameObject.Instantiate(_inventoryMenuPref);
		go.transform.SetParent(_menuCanvas.transform, false);

		_inventoryMenu = go.GetComponent<InventoryMenu>();
		yield break;
	}

	public void OpenInventoryMenu()
	{
		var list = new List<ParamItem.ID>();
		for (int i = 0; i < ParamItem.Count; i++)
		{
			list.Add((ParamItem.ID)i);
		}
		_inventoryMenu.Setup(list);
		_inventoryMenu.Open();
	}

	[SerializeField]
	GameObject _menuCanvas = null;

	[SerializeField]
	GameObject _inventoryMenuPref = null;

	InventoryMenu _inventoryMenu = null;
}
