using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class SaveDialog : MonoBehaviour
{
	public void Start()
	{
		_saveItemSource.SetActive(false);

		for (int i = 0; i < 20; i++)
		{
			var go = Instantiate(_saveItemSource);
			go.SetActive(true);

			go.transform.SetParent(_viewContent.transform, false);

			var saveItem = go.GetComponent<SaveItem>();
			saveItem.Setup(i + 1);

			_saveSlotList.Add(saveItem);
		}
	}

	public void Setup()
	{
	}

	[SerializeField]
	GameObject _saveItemSource;

	[SerializeField]
	GameObject _viewContent;

	List<SaveItem> _saveSlotList = new List<SaveItem>();
}
