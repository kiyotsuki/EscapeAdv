using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Animator))]
public class GameItem : MonoBehaviour
{
	public void Awake()
	{
		_animator = GetComponent<Animator>();
	}

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

	public void In(Action onEnd = null)
	{
		SetAnimationTrigger("In", onEnd);
	}

	public void Out(Action onEnd = null)
	{
		SetAnimationTrigger("Out", onEnd);
	}

	public void SetAnimationTrigger(string trigger, Action onEnd = null)
	{
		if(gameObject.activeSelf == false)
		{
			gameObject.SetActive(true);
		}
		_animator.SetTrigger(trigger);
		_onEndAnimation = onEnd;
	}

	public void SetActive(bool flag)
	{
		gameObject.SetActive(flag);
	}

	public void Update()
	{
		if(_animator == null || _onEndAnimation == null)
		{
			return;
		}
		var info = _animator.GetCurrentAnimatorStateInfo(0);
		if(_animationNormalizedTime < 1.0f && info.normalizedTime >= 1.0f)
		{
			_onEndAnimation();
			_onEndAnimation = null;
		}
		else
		{
			_animationNormalizedTime = info.normalizedTime;
		}
	}

	[SerializeField]
	private Button[] _buttons;

	[SerializeField]
	private Text[] _labels;

	[SerializeField]
	private Image[] _images;

	Animator _animator = null;
	Action _onEndAnimation = null;
	float _animationNormalizedTime = 0;
}
