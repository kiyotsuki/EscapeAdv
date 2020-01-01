using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		_menuFade.AddButtonListener(onClickBackScreen);
		_filterRaycaster.enabled = false;
		yield break;
	}
	
	public void OpenItemDialog(ParamItem.ID item)
	{
		_itemDialog.Setup(item);
		_itemDialog.Open();
		_currentDialog = _itemDialog;
	}

	private void onClickBackScreen()
	{
		if (_currentDialog != null)
		{
			_currentDialog.Close();
			_currentDialog = null;
		}
	}

	public void AddBackScreen()
	{
		if(_backScreenCount == 0)
		{
			_menuFade.In();
		}
		_backScreenCount++;
	}

	public void RemoveBackScreen()
	{
		if(_backScreenCount == 1)
		{
			_menuFade.Out();
		}
		_backScreenCount--;
	}

	public void AddTouchFilter()
	{
		if(_touchFilterCount == 0)
		{
			_filterRaycaster.enabled = true;
		}
		_touchFilterCount++;
	}

	public void RemoveTouchFilter()
	{
		if (_touchFilterCount == 1)
		{
			_filterRaycaster.enabled = false;
		}
		_touchFilterCount--;
	}

	[SerializeField]
	GameItem _menuFade;

	[SerializeField]
	ItemDialog _itemDialog;

	[SerializeField]
	GraphicRaycaster _filterRaycaster;

	DialogBase _currentDialog;

	int _backScreenCount = 0;
	int _touchFilterCount = 0;
}
