using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// マップマネージャにマップ上で行われた操作を伝える
/// </summary>
public class MapEventTrigger : EventTrigger
{
	public override void OnPointerDown(PointerEventData eventData)
	{
		var manager = GameUtil.GetManager<MapManager>();
		if(manager != null)
		{
			manager.OnMapTouch(eventData.position, true);
		}
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		var manager = GameUtil.GetManager<MapManager>();
		if (manager != null)
		{
			manager.OnMapTouch(eventData.position, false);
		}
	}
}