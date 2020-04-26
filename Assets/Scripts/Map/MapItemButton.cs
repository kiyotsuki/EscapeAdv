using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapItemButton : MonoBehaviour
{
	public void Setup(string text, int iconIndex, Action onClick)
	{
		_text.text = text;
		_icon.sprite = _iconSprites[iconIndex];

		_button.onClick.AddListener(() =>
		{
			onClick();
		});
	}

	[SerializeField]
	private Image _icon;

	[SerializeField]
	private Button _button;

	[SerializeField]
	private Text _text;

	[SerializeField]
	private Sprite[] _iconSprites;
}
