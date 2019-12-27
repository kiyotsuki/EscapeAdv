using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

/// <summary>
/// プレイヤーの制御を行うコンポーネント
/// </summary>
public class PlayerController : MonoBehaviour
{
	public void SetActive(bool flag)
	{
		gameObject.SetActive(flag);
	}

	public void SetAnimationTrigger(string trigger)
	{
		_animator.SetTrigger(trigger);
	}

	public void SetTargetTransform(Transform target)
	{
		_startPos = transform.position;
		_startRot = transform.rotation;
		_startScale = transform.localScale;

		_endPos = target.position;
		_endRot = target.rotation;
		_endScale = target.localScale;

		_isMoving = true;
		_moveTime = 0;
	}

	public void Update()
	{
		if(_isMoving)
		{
			_moveTime += Time.deltaTime;
			var rate = _moveTime / MOVE_TIME;
			if(rate > 1.0f)
			{
				rate = 1.0f;
				_isMoving = false;
			}
			transform.localScale = Vector3.Slerp(_startScale, _endScale, rate);
			transform.position = Vector3.Slerp(_startPos, _endPos, rate);
			transform.rotation = Quaternion.Slerp(_startRot, _endRot, rate);
		}
	}

	[SerializeField]
	private Animator _animator;

	Vector3 _startPos, _endPos;
	Vector3 _startScale, _endScale;
	Quaternion _startRot, _endRot;
	float _moveTime;
	bool _isMoving;

	const float MOVE_TIME = 0.2f;
}