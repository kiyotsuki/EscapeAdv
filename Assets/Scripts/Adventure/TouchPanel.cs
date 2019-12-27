using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

/// <summary>
/// マップマネージャにマップ上で行われた操作を伝える
/// </summary>
public class TouchPanel : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
	public void Setup(Action onDown, Action onUp)
	{
		_onDown = onDown;
		_onUp = onUp;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		PressTime = 0;
		LastTouchPos = eventData.position;
		_isPressing = true;
		_onDown();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		PressTime = 0;
		_isPressing = false;
		_onUp();
	}

	public void Update()
	{
		if(_isPressing)
		{
			PressTime += Time.deltaTime;
		}
	}
	
	public Vector2 LastTouchPos { private set; get; }
	public float PressTime { private set; get; }
	bool _isPressing;
	
	Action _onDown, _onUp;
}