using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
	public void Awake()
	{
		_closeButton.onClick.AddListener(() =>
		{
			this.gameObject.SetActive(false);
		});
	}

	public void Open(List<ParamItem.ID> itemList)
	{
		foreach(var go in _elementObjects)
		{
			GameObject.Destroy(go);
		}

		foreach(var item in itemList)
		{
			var data = ParamItem.Get(item);
			var go = GameObject.Instantiate(_menuElement);

			go.SetActive(true);
			go.transform.SetParent(_viewContent.transform, false);

			var element = go.GetComponent<ItemMenuElement>();
			element.Setup(data.Name, "ItemCheck_" + item, "ItemUse_" + item);
			
			_elementObjects.Add(go);
		}

		this.gameObject.SetActive(true);
	}



	[SerializeField]
	private Button _closeButton;

	[SerializeField]
	private GameObject _menuElement;

	[SerializeField]
	private GameObject _viewContent;


	private List<GameObject> _elementObjects = new List<GameObject>();
}
