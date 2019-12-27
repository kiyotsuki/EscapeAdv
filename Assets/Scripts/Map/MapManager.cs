using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		_currentMap = _mapList[0];

		_touchPanel.Setup(() =>
		{
			_isSearchStart = true;
			_searchIcon.SetActive(true);
			_searchIcon.transform.position = _touchPanel.LastTouchPos;

		}, () =>
		{
			_isSearchStart = false;
			_searchIcon.SetActive(false);
		});

		_searchIcon.SetActive(false);
		_missIcon.SetActive(false);
		yield break;
	}

	public override void OnUpdateGame()
	{
		if(_isSearchStart)
		{
			if (_touchPanel.PressTime > 3.0)
			{
				_isSearchStart = false;
				_searchIcon.SetActive(false);
				searchFinished(_searchIcon.transform.position);
			}
		}
	}


	private void searchFinished(Vector2 pos)
	{
		var scenarioManager = GameUtil.GetManager<ScenarioManager>();
		var ev = _currentMap.GetMapEvent(pos);
		if (ev != null)
		{
			scenarioManager.ExecuteScenario(ev.EventName);
		}
		else
		{
			_missIcon.SetActive(false);
			_missIcon.SetActive(true);
			_missIcon.transform.position = _searchIcon.transform.position;
		}
	}

	public void ChangeMap(string mapName)
	{
		MapData nextMap = null;
		foreach(var map in _mapList)
		{
			if (map.name == mapName)
			{
				nextMap = map;
				nextMap.gameObject.SetActive(true);
			}
			else if (map.gameObject.activeSelf)
			{
				map.gameObject.SetActive(false);
			}
		}
		if(nextMap == null)
		{
			Debug.Log($"指定されたマップがありません MapName={mapName}");
			_currentMap.gameObject.SetActive(true);
			return;
		}
		_currentMap = nextMap;
	}


	[SerializeField]
	TouchPanel _touchPanel;
	
	[SerializeField]
	MapData[] _mapList;

	[SerializeField]
	GameItem _searchIcon;

	[SerializeField]
	GameItem _missIcon;

	MapData _currentMap = null;
	bool _isSearchStart = false;
}
