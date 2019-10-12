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


	[SerializeField]
	private int _index;

	[SerializeField]
	private Image _image;

	[SerializeField]
	private bool _colision;
}
