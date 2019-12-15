using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameItem : MonoBehaviour
{
	public void AddButtonListener(UnityAction action, int index = 0)
	{
		_buttons[index].onClick.AddListener(action);
	}

	public void SetLabelText(string text, int index = 0)
	{
		_labels[index].text = text;
	}

	public void SetImageSprite(Sprite sprite, int index = 0)
	{
		_images[index].sprite = sprite;
	}

	public void SetAnimationTrigger(string trigger, int index = 0)
	{
		_animators[index].SetTrigger(trigger);
	}

	[SerializeField]
	private Animator[] _animators;

	[SerializeField]
	private Button[] _buttons;

	[SerializeField]
	private Text[] _labels;

	[SerializeField]
	private Image[] _images;
}
