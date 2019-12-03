using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : ManagerBase
{
	public override void Initialize()
	{
		_mapCanvas = GameUtil.GetNamedSceneObject("MapCanvas");
		_mapCanvas.SetActive(true);

		_mapList = _mapCanvas.GetComponentsInChildren<MapData>();
		_currentMap = _mapList[0];

		_searchIcon = GameUtil.GetNamedSceneObject("SearchIcon").GetComponent<Image>();
		_searchIcon.enabled = false;
	}

	public override void OnUpdate()
	{
		if(_searchIcon.enabled)
		{
			_searchTime += Time.deltaTime;
			_searchIcon.fillAmount = _searchTime;
			if (_searchTime > 1.0f)
			{
				_searchIcon.enabled = false;
				searchFinished(_searchIcon.transform.position);
			}
		}
	}

	public void OnMapTouch(Vector2 pos, bool on)
	{
		if (on)
		{
			_searchIcon.transform.position = pos;
			_searchIcon.enabled = true;
			_searchIcon.fillAmount = 0;
			_searchTime = 0;
		}
		else
		{
			_searchIcon.enabled = false;
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
			scenarioManager.ExecuteScenario("NotFound");
		}
	}


	GameObject _mapCanvas = null;
	MapData[] _mapList;
	MapData _currentMap = null;

	float _searchTime = 0;
	Image _searchIcon;
}
