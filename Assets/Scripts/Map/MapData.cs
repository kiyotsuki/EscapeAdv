using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapData : MonoBehaviour
{	
	public MapEvent GetMapEvent(Vector2 pos)
	{
		foreach(var ev in _events)
		{
			if(ev.IsEnter(pos))
			{
				return ev;
			}
		}
		return null;
	}

	[SerializeField]
	private Image _image;

	[SerializeField]
	private string _mapName;

	[SerializeField]
	private MapEvent[] _events;
}
