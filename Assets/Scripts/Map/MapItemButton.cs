using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapItemButton : MonoBehaviour
{
	public void Awake()
	{
		_button.onClick.AddListener(() =>
		{
			if (_onClick != null)
			{
				_onClick();
			}
		});
	}

	public void Setup(string text, Sprite icon, Action onClick)
	{
		_text.text = text;
		_icon.sprite = icon;

		_onClick = onClick;
	}

	[SerializeField]
	private Image _icon;

	[SerializeField]
	private Button _button;

	[SerializeField]
	private Text _text;

	[SerializeField]
	private Sprite[] _iconSprites;

	private Action _onClick;
}
