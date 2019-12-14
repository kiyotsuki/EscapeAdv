using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		_mapList = _mapCanvas.GetComponentsInChildren<MapData>();
		_currentMap = _mapList[0];
		_searchIcon.enabled = false;
		yield break;
	}

	public override void OnUpdateGame()
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

	[SerializeField]
	GameObject _mapCanvas = null;

	[SerializeField]
	MapData[] _mapList;

	[SerializeField]
	Image _searchIcon;


	MapData _currentMap = null;
	float _searchTime = 0;
}
