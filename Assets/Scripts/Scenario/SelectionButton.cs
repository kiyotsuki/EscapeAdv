using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionButton : MonoBehaviour
{
	public void AddListener(UnityAction action)
	{
		_button.onClick.AddListener(action);
	}

	public void SetText(string text)
	{
		_text.text = text;
	}

	[SerializeField]
	private Button _button;

	[SerializeField]
	private Text _text;
}
