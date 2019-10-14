using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapChipData : MonoBehaviour
{
	public void Setup(int index, Color color, bool colision)
	{
		_index = index;
		_image.color = color;
		_colision = colision;
	}

	public int GetIndex()
	{
		return _index;
	}

	public bool GetColision()
	{
		return _colision;
	}

	public Vector2 GetPos()
	{
		return gameObject.transform.position;
	}


	[SerializeField]
	int _index;

	[SerializeField]
	Image _image;

	[SerializeField]
	bool _colision;
}
