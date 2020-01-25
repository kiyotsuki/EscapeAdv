using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class AnimationPlayer: MonoBehaviour
{
	public void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void SetActive(bool flag)
	{
		gameObject.SetActive(flag);
	}

	private IEnumerator playAnimation(string clipName, Action onFinish, bool endInactive)
	{
		var hash = Animator.StringToHash(clipName);
		_animator.Play(hash);

		while (true)
		{
			var info = _animator.GetCurrentAnimatorStateInfo(0);
			if(info.shortNameHash != hash)
			{
				yield return null;
				continue;
			}
			if(info.normalizedTime < 1.0)
			{
				yield return null;
				continue;
			}
			break;
		}
		if(onFinish != null)
		{
			onFinish();
		}
		if(endInactive)
		{
			SetActive(false);
		}
		yield break;
	}

	public void PlayAnimation(string clipName, Action onFinish = null, bool endInactive = false)
	{
		if(gameObject.activeSelf == false)
		{
			gameObject.SetActive(true);
		}
		StopAllCoroutines();
		StartCoroutine(playAnimation(clipName, onFinish, endInactive));
	}

	public void PlayOneShot(Action onFinish = null)
	{
		var name = _animator.runtimeAnimatorController.name;
		PlayAnimation(name + "@Play", onFinish, true);
	}

	public void PlayIn(Action onFinish = null)
	{
		var name = _animator.runtimeAnimatorController.name;
		PlayAnimation(name + "@In", onFinish, false);
	}

	public void PlayOut(Action onFinish = null)
	{
		var name = _animator.runtimeAnimatorController.name;
		PlayAnimation(name + "@Out", onFinish, true);
	}

	Animator _animator = null;
}
