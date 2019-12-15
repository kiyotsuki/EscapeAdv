using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MenuBase
{
	public void Awake()
	{
		// ソースは非表示に
		_menuItemSource.SetActive(false);

		_closeButton.onClick.AddListener(() =>
		{
			Close();
		});
	}
	
	public void Setup(List<ParamItem.ID> itemList)
	{
		foreach(var go in _elementObjects)
		{
			GameObject.Destroy(go);
		}

		foreach(var item in itemList)
		{
			var data = ParamItem.Get(item);
			var go = GameObject.Instantiate(_menuItemSource);

			go.SetActive(true);
			go.transform.SetParent(_viewContent.transform, false);

			var gi = go.GetComponent<GameItem>();
			gi.AddButtonListener(() => { checkItem(data); }, 0);
			gi.AddButtonListener(() => { useItem(data); }, 1);
			gi.SetLabelText(data.Name);
			
			_elementObjects.Add(go);
		}
	}
	
	private void checkItem(ParamItem.Data data)
	{
		var scenarioManager = GameUtil.GetManager<ScenarioManager>();
		scenarioManager.ExecuteScenario("CheckItem_" + data.Id);
		Close();
	}

	private void useItem(ParamItem.Data data)
	{
		var adventureManager = GameUtil.GetManager<AdventureManager>();
		adventureManager.SetUseItem(data);

		Close();
	}


	[SerializeField]
	private Button _closeButton;

	[SerializeField]
	private GameObject _menuItemSource;

	[SerializeField]
	private GameObject _viewContent;


	private List<GameObject> _elementObjects = new List<GameObject>();
}
