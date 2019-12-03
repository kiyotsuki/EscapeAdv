using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapEvent : MonoBehaviour
{
	public bool IsEnter(Vector2 pos)
	{
		var rectTrans = (RectTransform)this.transform;
		var w = rectTrans.rect.width / 2.0f;
		var h = rectTrans.rect.height / 2.0f;

		var maxPos = rectTrans.rect.max;
		var minPos = rectTrans.rect.min;
		/*
		Vector2 myPos = transform.position;
		var minPos = myPos - new Vector2(w, h);
		var maxPos = myPos + new Vector2(w, h);
		*/
		if (minPos.x > pos.x || maxPos.x < pos.x)
		{
			return false;
		}
		if (minPos.y > pos.y || maxPos.y < pos.y)
		{
			return false;
		}
		return true;
	}

	public string EventName;
}
