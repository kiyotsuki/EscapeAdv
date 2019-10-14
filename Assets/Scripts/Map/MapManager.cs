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
		
		var allMap = _mapCanvas.GetComponentsInChildren<MapData>();
		foreach(var map in allMap)
		{
			var name = map.name;
			_mapDataDict.Add(name.GetHashCode(), map);
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
		
		var playerManager = GameUtil.GetManager<PlayerManager>();
		playerManager.OnChangeMap(_currentMap);
	}

	public MapData GetCurrentMap()
	{
		return _currentMap;
	}

	GameObject _mapCanvas = null;
	Dictionary<int, MapData> _mapDataDict = new Dictionary<int, MapData>();

	MapData _currentMap = null;
}
