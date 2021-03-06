﻿using System.Collections;
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
		_animator.Play("");
	}

	private void resetStatus()
	{
		_onEndCallback = null;
		_animationNormalizedTime = 0;
		_inAnimation = false;
		_endInactive = false;
		setInteractable(true);
	}

	private void setInteractable(bool flag)
	{
		foreach (var button in _buttons)
		{
			button.interactable = flag;
		}
	}

	public void SetActive(bool flag)
	{
		resetStatus();
		gameObject.SetActive(flag);
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
		StartAnimation("In", onEnd);
		_endInactive = false;
	}

	public void Out(Action onEnd = null)
	{
		StartAnimation("Out", onEnd);
		_endInactive = true;
	}

	public void Play(Action onEnd = null)
	{
		var preActive = gameObject.activeSelf;
		StartAnimation("Play", onEnd);
		_endInactive = preActive == false;
	}

	public void StartAnimation(string trigger, Action onEnd = null)
	{
		if (gameObject.activeSelf == false)
		{
			gameObject.SetActive(true);
		}
		_animator.SetTrigger(trigger);

		setInteractable(false);
		_onEndCallback = onEnd;

		_inAnimation = true;

		// アニメーション開始
		OnStartAnimation();
	}


	public void Update()
	{
		if(_inAnimation == false)
		{
			return;
		}

		var info = _animator.GetCurrentAnimatorStateInfo(0);
		if(_animationNormalizedTime < 1.0f && info.normalizedTime >= 1.0f)
		{
			setInteractable(true);
			_animationNormalizedTime = 1.0f;

			//　コールバックがあれば呼び出して削除
			if (_onEndCallback != null)
			{
				_onEndCallback();
				_onEndCallback = null;
			}
			if(_endInactive)
			{
				gameObject.SetActive(false);
				_endInactive = false;
			}
			
			OnEndAnimation();
			_inAnimation = false;
		}
		else
		{
			_animationNormalizedTime = info.normalizedTime;
		}
	}

	protected virtual void OnStartAnimation()
	{
	}

	protected virtual void OnEndAnimation()
	{
	}

	[SerializeField]
	protected Button[] _buttons;

	[SerializeField]
	protected Text[] _labels;

	[SerializeField]
	protected Image[] _images;

	Animator _animator = null;
	Action _onEndCallback = null;
	float _animationNormalizedTime = 0;

	bool _inAnimation = false;
	bool _endInactive = false;
}
