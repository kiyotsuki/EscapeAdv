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
		yield break;
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
			var go = _playerLocationA[i];
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

	public List<ParamItem.ID> GetPlayerItems(ParamCharacter.ID player = ParamCharacter.ID.Invalid)
	{
		if(player == ParamCharacter.ID.Invalid)
		{
			player = _currentPlayer;
		}
		return _playerItems[(int)player];
	}

	public void AddPlayerItem(ParamItem.ID item, ParamCharacter.ID player = ParamCharacter.ID.Invalid)
	{
		if (player == ParamCharacter.ID.Invalid)
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
	GameObject[] _playerLocationA, playerLocationB;

	[SerializeField]
	ItemWindow _itemWindow;


	bool[] _playerActives = new bool[ParamCharacter.Count];
	List<ParamItem.ID>[] _playerItems = new List<ParamItem.ID>[ParamCharacter.Count];
	
	ParamCharacter.ID _currentPlayer = ParamCharacter.ID.Momoka;
}
