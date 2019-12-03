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

		// starts bottom left and rotates to top left, then top right, and finally bottom right. Note that bottom left
		var corners = new Vector3[4];
		rectTrans.GetWorldCorners(corners);

		// 最小は左下、最大は右上
		var minPos = (Vector2)corners[0];
		var maxPos = (Vector2)corners[2];

		
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
