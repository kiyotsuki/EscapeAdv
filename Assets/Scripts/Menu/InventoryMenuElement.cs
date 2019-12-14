using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryMenuElement : MonoBehaviour
{
	public void Setup(string name, UnityAction onClickCheck, UnityAction onClickUse)
	{
		_checkButton.onClick.AddListener(onClickCheck);
		_useButton.onClick.AddListener(onClickUse);

		_nameLabel.text = name;
	}


	[SerializeField]
	private Button _checkButton, _useButton;

	[SerializeField]
	private Image _iconImage;

	[SerializeField]
	private Text _nameLabel;
}
