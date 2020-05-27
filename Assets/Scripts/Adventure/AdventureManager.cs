using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class AdventureManager : ManagerBase
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

		ChangeMap(GameDefine.MapId.Room01);
		_mapItemButtonSource.gameObject.SetActive(false);

		MoveCamera("Default", 0.3f);
		yield break;
	}

	public void MoveCamera(string location, float time)
	{
		_cameraController.MoveCamera(location, time);
	}

	public void OnStartEvent()
	{
		MoveCamera("Event", 0.3f);
		_mapItemRoot.SetActive(false);
	}

	public void OnEndEvent()
	{
		MoveCamera("Default", 0.3f);
		_mapItemRoot.SetActive(true);

		var map = ScenarioUtil.GetCurrentMap();
		SetupMap(map);
	}

	public void ChangeMap(GameDefine.MapId map)
	{
		var saveData = GameUtil.GetSaveData();
		saveData.SetCurrentMap(map);

		SetupMap(map);
	}

	public void AddMapItem(string name, string iconName, Func<IEnumerator> scenario)
	{
		var icon = _iconSprites[0];
		foreach(var sprite in _iconSprites)
		{
			if(sprite.name == iconName)
			{
				icon = sprite;
			}
		}

		MapItemButton itemButton = null;
		foreach (var item in _mapItemList)
		{
			if(item.gameObject.activeSelf == false)
			{
				item.gameObject.SetActive(true);
				itemButton = item;
				break;
			}
		}

		if(itemButton == null)
		{
			itemButton = GameUtil.CreateInstance(_mapItemButtonSource);
			_mapItemList.Add(itemButton);
		}

		itemButton.Setup(name, icon, () =>
		{
			GameUtil.GetManager<ScenarioManager>().ExecuteScenario(scenario);
		});
	}

	public void ClearMapItems()
	{
		foreach (var item in _mapItemList)
		{
			item.gameObject.SetActive(false);
		}
	}

	public void SetMapName(string name)
	{
		_mapNameText.text = name;
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
	GameObject _mapItemRoot;

	[SerializeField]
	Text _mapNameText;

	bool[] _playerActives = new bool[ParamCharacter.Count];
	List<ParamItem.ID>[] _playerItems = new List<ParamItem.ID>[ParamCharacter.Count];
	
	ParamCharacter.ID _currentPlayer = ParamCharacter.ID.Momoka;

	List<MapItemButton> _mapItemList = new List<MapItemButton>();

	[SerializeField]
	CameraController _cameraController;

	[SerializeField]
	Sprite[] _iconSprites;
}
