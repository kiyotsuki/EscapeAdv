using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		_charaChangeButtonL.onClick.AddListener(() =>
		{
			setNextPlayer(1);
		});
		_charaChangeButtonR.onClick.AddListener(() =>
		{
			setNextPlayer(-1);
		});

		_saveButton.onClick.AddListener(() =>
		{
			GameUtil.GetManager<MenuManager>().OpenSaveLoadDialog();
		});

		_optionButton.onClick.AddListener(() =>
		{
			GameUtil.GetManager<MenuManager>().OpenOkDialog("工事中");
		});

		for (int i = 0; i < ParamCharacter.Count; i++)
		{
			_playerActives[i] = true;
			_playerItems[i] = new List<ParamItem.ID>();
			_players[i].SetActive(true);
		}


		// プレイヤーの初期アイテム
		_playerItems[0].Add(ParamItem.ID.Diary);
		_playerItems[0].Add(ParamItem.ID.Dummy);

		_playerItems[1].Add(ParamItem.ID.Key);

		_playerItems[2].Add(ParamItem.ID.Light);


		SetCurrentPlayer(ParamCharacter.ID.Momoka);

		ChangeMap(ParamMap.ID.WAITING_ROOM);
		_mapItemButtonSource.gameObject.SetActive(false);
		yield break;
	}

	public void ChangeMap(ParamMap.ID map)
	{
		var data = ParamMap.Get(map);
		_mapNameText.text = data.Name;

		foreach (var item in _mapItemList)
		{
			Destroy(item.gameObject);
		}

		var itemDataList = ParamMapItem.GetList(data.ItemList);
		foreach (var itemData in itemDataList)
		{
			var instance = GameUtil.CreateInstance(_mapItemButtonSource);
			instance.Setup(itemData.Name, itemData.IconIndex, () =>
			{
				GameUtil.GetManager<ScenarioManager>().ExecuteScenario("Opening");
			});
			_mapItemList.Add(instance);
		}
	}

	public void SetMapName(string name)
	{
		_mapNameText.text = name;
	}

	public void AddMapItem(string name, int iconIndex, Func<IEnumerator> scenario)
	{
		var instance = GameUtil.CreateInstance(_mapItemButtonSource);
		instance.Setup(name, iconIndex, () =>
		{
			GameUtil.GetManager<ScenarioManager>().ExecuteScenario(scenario);
		});
		_mapItemList.Add(instance);
	}

	public void ClearMapItems()
	{
		foreach (var item in _mapItemList)
		{
			Destroy(item.gameObject);
		}
	}

	private void setNextPlayer(int dir)
	{
		var current = (int)_currentPlayer;
		for (int i = 0; i < ParamCharacter.Count - 1; i++)
		{
			current = (current + dir) % ParamCharacter.Count;
			if (current < 0) current += ParamCharacter.Count;
			if (_playerActives[current])
			{
				SetCurrentPlayer((ParamCharacter.ID)current);
				return;
			}
		}
	}

	public void SetCurrentPlayer(ParamCharacter.ID id)
	{
		_currentPlayer = id;
		var data = ParamCharacter.Get(id);
		_playerName.text = data.Name;

		for (int i = 0; i < ParamCharacter.Count; i++)
		{
			var go = _playerLocations[i];
			_players[((int)_currentPlayer + i) % ParamCharacter.Count].SetTargetTransform(go.transform);
		}

		_itemWindow.ResetItem();
		foreach(var item in _playerItems[(int)id])
		{
			_itemWindow.AddItem(item);
		}
		_itemWindow.SetAnimationTrigger("In");
	}

	public override void OnStartGame()
	{

	}
	
	public void SetPlayerActive(ParamCharacter.ID id, bool active)
	{
		_playerActives[(int)id] = active;
	}

	public List<ParamItem.ID> GetPlayerItems(ParamCharacter.ID player = ParamCharacter.ID.NONE)
	{
		if(player == ParamCharacter.ID.NONE)
		{
			player = _currentPlayer;
		}
		return _playerItems[(int)player];
	}

	public void AddPlayerItem(ParamItem.ID item, ParamCharacter.ID player = ParamCharacter.ID.NONE)
	{
		if (player == ParamCharacter.ID.NONE)
		{
			player = _currentPlayer;
		}
		_playerItems[(int)player].Add(item);
	}

	public void OnSave(SaveData saveData)
	{

	}

	public void OnLoad(SaveData saveData)
	{

	}

	[SerializeField]
	GameObject _hudCanvas;
	
	[SerializeField]
	Button _saveButton, _optionButton;

	[SerializeField]
	Button _charaChangeButtonL;

	[SerializeField]
	Button _charaChangeButtonR;

	[SerializeField]
	Text _playerName;

	[SerializeField]
	PlayerController[] _players;

	[SerializeField]
	GameObject[] _playerLocations;

	[SerializeField]
	ItemWindow _itemWindow;

	[SerializeField]
	MapItemButton _mapItemButtonSource;

	[SerializeField]
	Text _mapNameText;

	bool[] _playerActives = new bool[ParamCharacter.Count];
	List<ParamItem.ID>[] _playerItems = new List<ParamItem.ID>[ParamCharacter.Count];
	
	ParamCharacter.ID _currentPlayer = ParamCharacter.ID.Momoka;

	List<MapItemButton> _mapItemList = new List<MapItemButton>();
}
