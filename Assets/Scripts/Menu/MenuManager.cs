using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		_filterRaycaster.enabled = false;
		yield break;
	}

	private DialogFrame getDialogFrame()
	{
		foreach (var frame in _dialogList)
		{
			if(frame.gameObject.activeSelf == false)
			{
				return frame;
			}
		}
		var dialogFrame = GameUtil.CreateInstance(_dialogFrameSource);
		_dialogList.Add(dialogFrame);
		return dialogFrame;
	}

	public void OpenSaveDialog()
	{
	}

	public void OpenOkDialog(string text, Action onClickOk = null)
	{
		var dialogFrame = getDialogFrame();
		var content = dialogFrame.CreateContent(_simpleDialogSource);
		content.Setup(text, onClickOk);
		dialogFrame.Open();
	}

	public void OpenYesNoDialog(string text, Action onClickYes, Action onClickNo = null)
	{
		var dialogFrame = getDialogFrame();
		var content = dialogFrame.CreateContent(_simpleDialogSource);
		content.Setup(text, onClickYes, onClickNo);
		dialogFrame.Open();
	}
	
	public void OpenItemUseDialog(ParamItem.ID item)
	{
		var dialogFrame = getDialogFrame();
		var content = dialogFrame.CreateContent(_itemUseDialogSource);
		content.Setup(item);
		dialogFrame.Open();
	}


	public void AddBackScreen()
	{
		if (_backScreenCount == 0)
		{
			_menuFade.In();
		}
		_backScreenCount++;
	}

	public void RemoveBackScreen()
	{
		if (_backScreenCount == 1)
		{
			_menuFade.Out();
		}
		_backScreenCount--;
	}

	public void AddTouchFilter()
	{
		if (_touchFilterCount == 0)
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
	SaveDialog _saveDialog;
	
	[SerializeField]
	GraphicRaycaster _filterRaycaster;

	[SerializeField]
	DialogFrame _dialogFrameSource;

	[SerializeField]
	SimpleDialog _simpleDialogSource;

	[SerializeField]
	ItemUseDialog _itemUseDialogSource;


	List<DialogFrame> _dialogList = new List<DialogFrame>();

	int _backScreenCount = 0;
	int _touchFilterCount = 0;
}
