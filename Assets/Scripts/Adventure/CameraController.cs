using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.Analytics;
using System.Collections.Generic;

/// <summary>
/// カメラの移動などを制御する
/// </summary>
public class CameraController : MonoBehaviour
{
	public void Awake()
	{
		var rootTrans = this.transform;
		for (int i = 0; i < rootTrans.childCount; i++)
		{
			var trans = rootTrans.GetChild(i);
			var name = trans.gameObject.name;
			_locationDict.Add(name, trans);
		}
	}

	public void MoveCamera(string name, float time)
	{
		var trans = _locationDict[name];
		_posTo = trans.position;

		var cameraTrans = Camera.main.transform;
		_posFrom = cameraTrans.position;

		_maxTime = time;
		_time = 0;
		_moving = true;
	}

	public void Update()
	{
		if (_moving)
		{
			_time += Time.deltaTime;
			var rate = _time / _maxTime;
			rate = Math.Min(rate, 1.0f);

			var pos = Vector3.Slerp(_posFrom, _posTo, rate);

			var cameraTrans = Camera.main.transform;
			cameraTrans.position = pos;

			if(rate >= 1.0f)
			{
				_moving = false;
			}
		}
	}

	Dictionary<string, Transform> _locationDict = new Dictionary<string, Transform>();
	Vector3 _posFrom, _posTo;
	float _maxTime, _time;
	bool _moving;
}