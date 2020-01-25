using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWindow : MonoBehaviour
{
	public void Start()
	{
		_itemSource.SetActive(false);
	}

	public void AddItem(ParamItem.ID item)
	{
		var go = Instantiate(_itemSource);
		go.SetActive(true);
		go.transform.SetParent(_viewContent.transform, false);

		var button = go.GetComponent<Button>();
		button.onClick.AddListener(() =>
		{
			onClickItem(item);
		});

		_items.Add(go);
	}

	private void onClickItem(ParamItem.ID item)
	{
		var menuManager = GameUtil.GetManager<MenuManager>();
		menuManager.OpenItemUseDialog(item);
	}

	public void SetAnimationTrigger(string trigger)
	{
		_animator.SetTrigger(trigger);
	}

	public void ResetItem()
	{
		foreach(var go in _items)
		{
			GameObject.Destroy(go);
		}
		_items.Clear();
	}

	[SerializeField]
	GameObject _itemSource;

	[SerializeField]
	Animator _animator;

	[SerializeField]
	GameObject _viewContent;

	List<GameObject> _items = new List<GameObject>();
}
