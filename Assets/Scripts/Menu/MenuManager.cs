using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		_menuFade.AddButtonListener(onClickBackScreen);
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
		if(_screenCount == 0)
		{
			_menuFade.In();
		}
		_screenCount++;
	}

	public void RemoveBackScreen()
	{
		_screenCount--;
		if(_screenCount <= 0)
		{
			_screenCount = 0;
			_menuFade.Out();
		}
	}

	[SerializeField]
	GameItem _menuFade;

	[SerializeField]
	ItemDialog _itemDialog;

	DialogBase _currentDialog;

	int _screenCount = 0;
}
