using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionWindow : MonoBehaviour
{
	public void Awake()
	{
		for (int i = 0; i < _buttons.Length; i++)
		{
			var index = i;
			_buttons[i].AddListener(() =>
			{
				_selectedIndex = index;
			});
		}
	}

	public IEnumerator StartSelection(string text0, string text1 = null, string text2 = null, string text3 = null)
	{
		this.gameObject.SetActive(true);

		var texts = new string[] { text0, text1, text2, text3 };
		for (int i = 0; i < texts.Length; i++)
		{
			var text = texts[i];
			var button = _buttons[i];
			if(string.IsNullOrEmpty(text))
			{
				button.gameObject.SetActive(false);
			}
			else
			{
				button.gameObject.SetActive(true);
				button.SetText(text);
			}
		}

		_selectedIndex = -1;
		while (_selectedIndex < 0) yield return null;

		this.gameObject.SetActive(false);
		yield break;
	}

	public int GetSelectedIndex()
	{
		return _selectedIndex;
	}

	[SerializeField]
	private SelectionButton[] _buttons;

	private int _selectedIndex = 0;
}
