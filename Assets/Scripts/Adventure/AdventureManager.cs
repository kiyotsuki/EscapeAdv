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

		for (int i = 0; i < PLAYER_NUM; i++)
		{
			_playerActives[i] = true;
			_playerItems[i] = new List<ParamItem.ID>();
		}


		// プレイヤーの初期アイテム
		_playerItems[0].Add(ParamItem.ID.Diary);
		_playerItems[0].Add(ParamItem.ID.Dummy);

		_playerItems[1].Add(ParamItem.ID.Key);

		_playerItems[2].Add(ParamItem.ID.Light);


		SetCurrentPlayer(ParamPlayer.ID.Momoka);
		yield break;
	}

	private void setNextPlayer(int dir)
	{
		var current = (int)_currentPlayer;
		for (int i = 0; i < PLAYER_NUM - 1; i++)
		{
			current = (current + dir) % PLAYER_NUM;
			if (current < 0) current += PLAYER_NUM;
			if (_playerActives[current])
			{
				SetCurrentPlayer((ParamPlayer.ID)current);
				return;
			}
		}
	}

	public void SetCurrentPlayer(ParamPlayer.ID id)
	{
		_currentPlayer = id;
		var data = ParamPlayer.Get(id);
		_playerName.text = data.Name;

		for (int i = 0; i < PLAYER_NUM; i++)
		{
			var go = _playerLocationA[i];
			_players[((int)_currentPlayer + i) % PLAYER_NUM].SetTargetTransform(go.transform);
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
	
	public void SetPlayerActive(ParamPlayer.ID id, bool active)
	{
		_playerActives[(int)id] = active;
	}

	public List<ParamItem.ID> GetPlayerItems(ParamPlayer.ID player = ParamPlayer.ID.Invalid)
	{
		if(player == ParamPlayer.ID.Invalid)
		{
			player = _currentPlayer;
		}
		return _playerItems[(int)player];
	}

	public void AddPlayerItem(ParamItem.ID item, ParamPlayer.ID player = ParamPlayer.ID.Invalid)
	{
		if (player == ParamPlayer.ID.Invalid)
		{
			player = _currentPlayer;
		}
		_playerItems[(int)player].Add(item);
	}



	[SerializeField]
	GameObject _hudCanvas;

	[SerializeField]
	TouchPanel _mapTouchPanel;

	[SerializeField]
	Button _itemMenuButton;

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

	const int PLAYER_NUM = 3;

	bool[] _playerActives = new bool[PLAYER_NUM];
	List<ParamItem.ID>[] _playerItems = new List<ParamItem.ID>[PLAYER_NUM];
	
	ParamPlayer.ID _currentPlayer = ParamPlayer.ID.Momoka;
}
