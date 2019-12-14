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
		_searchIcon = GameObject.Instantiate(_searchIconPref);
		_searchIcon.transform.SetParent(_mapCanvas.transform, false);
		_searchIcon.SetActive(false);

		_missIcon = GameObject.Instantiate(_missIconPref);
		_missIcon.transform.SetParent(_mapCanvas.transform, false);
		_missIcon.SetActive(false);

		yield break;
	}

	public override void OnUpdateGame()
	{
		if(_searchIcon.activeSelf)
		{
			_searchTime += Time.deltaTime;
			if (_searchTime > 1.0f)
			{
				_searchIcon.SetActive(false);
				searchFinished(_searchIcon.transform.position);
			}
		}
	}

	public void OnMapTouch(Vector2 pos, bool on)
	{
		if (on)
		{
			_searchIcon.SetActive(true);
			_searchIcon.transform.position = pos;
			_searchTime = 0;
		}
		else
		{
			_searchIcon.SetActive(false);
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

	[SerializeField]
	GameObject _mapCanvas = null;

	[SerializeField]
	MapData[] _mapList;

	[SerializeField]
	GameObject _searchIconPref;

	[SerializeField]
	GameObject _missIconPref;


	GameObject _searchIcon;
	GameObject _missIcon;

	MapData _currentMap = null;
	float _searchTime = 0;
}
