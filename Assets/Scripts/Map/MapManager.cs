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
		
		_playerMarker = GameUtil.FindChild(_mapCanvas, "Player");
		_targetMarker = GameUtil.FindChild(_mapCanvas, "Target");

		var allMap = _mapCanvas.GetComponentsInChildren<MapData>();
		foreach(var map in allMap)
		{
			var name = map.name;
			_mapDataDict.Add(name.GetHashCode(), map);
		}

		ChangeMap("TestMap");
	}

	public override void OnUpdate()
	{
		if(GameUtil.GetManager<ScenarioManager>().RunningScenario())
		{
			return;
		}

		if(Input.GetMouseButton(0))
		{
			var targetPos = Input.mousePosition;
			_targetMarker.transform.position = targetPos;

			var playerPos = _playerMarker.transform.position;

			_route = _currentMap.GetRoute(playerPos, targetPos);
		}

		if(_route != null && _route.Count > 0)
		{
			var playerPos = _playerMarker.transform.position;

			var r = _route[0];

			var diff = r - (Vector2)playerPos;
			var sqrMag = diff.sqrMagnitude;

			if (sqrMag < 5 * 5)
			{
				_route.RemoveAt(0);
			}
			else
			{
				int spd = 200;

				var dir = diff.normalized * spd;
				playerPos.x += dir.x * Time.deltaTime;
				playerPos.y += dir.y * Time.deltaTime;

				_playerMarker.transform.position = playerPos;
			}
		}
	}

	public void ChangeMap(string name)
	{
		var hash = name.GetHashCode();
		_currentMap = null;
		foreach(var mapPair in _mapDataDict)
		{
			var data = mapPair.Value;
			if(mapPair.Key == hash)
			{
				data.gameObject.SetActive(true);
				_currentMap = data;
			}
			else
			{
				data.gameObject.SetActive(false);
			}
		}
	}

	public void SetPlayer(int x, int y)
	{
		var chip = _currentMap.GetMapChipData(x, y);
		_playerMarker.transform.position = chip.transform.position;
	}

	private GameObject _mapCanvas = null;
	private Dictionary<int, MapData> _mapDataDict = new Dictionary<int, MapData>();

	private GameObject _playerMarker = null;
	private GameObject _targetMarker = null;


	private MapData _currentMap = null;

	private List<Vector2> _route = null;
}
