using UnityEngine;
using UnityEngine.UI;
using System;


public class DialogFrame : AnimationPlayer
{
	public void Start()
	{
		_backScreen.onClick.AddListener(onClickBackScreen);
	}

	private void onClickBackScreen()
	{
		if (_isModal == false)
		{
			Close(null);
		}
	}

	public void Open(bool isModal = false)
	{
		MenuUtil.AddTouchFilter();
		_isModal = isModal;
		PlayIn(() =>
		{
			_content.OnOpen();
			MenuUtil.RemoveTouchFilter();
		});
	}

	public void Close(Action onCloseEnd)
	{
		MenuUtil.AddTouchFilter();
		PlayOut(() =>
		{
			MenuUtil.RemoveTouchFilter();
			_content.OnClose();
			onCloseEnd?.Invoke();
		});
	}
	
	public T CreateContent<T>(T source) where T : DialogContentBase
	{
		if(_content != null)
		{
			Destroy(_content.gameObject);
			_content = null;
		}
		_content = GameUtil.CreateInstance<T>(source, _contentRoot);
		_content.SetFrame(this);
		return (T)_content;
	}


	[SerializeField]
	private Transform _contentRoot;

	[SerializeField]
	private Button _backScreen;

	private bool _isModal;
	private DialogContentBase _content;
}
