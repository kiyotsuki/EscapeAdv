using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		_useItemDisplay.AddButtonListener(() =>
		{
			SetUseItem(null);
		});
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
		SetUseItem(null);

		for (int i = 0; i < PLAYER_NUM; i++)
		{
			var go = _playerLocationA[i];
			_players[((int)_currentPlayer + i) % PLAYER_NUM].SetTargetTransform(go.transform);
		}
	}

	public override void OnStartGame()
	{
		_itemMenuButton.onClick.AddListener(() =>
		{
			var menuManager = GameUtil.GetManager<MenuManager>();
			menuManager.OpenInventoryMenu();
		});
	}

	public void SetUseItem(ParamItem.Data data)
	{
		if (data == null)
		{
			_useItemId = ParamItem.ID.Invalid;
			_useItemDisplay.SetAnimationTrigger("Out");
			return;
		}
		_useItemId = data.Id;
		_useItemDisplay.SetLabelText(data.Name);
		_useItemDisplay.SetAnimationTrigger("In");
	}

	public ParamItem.ID GetUseItem()
	{
		return _useItemId;
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
	GameItem _useItemDisplay;

	[SerializeField]
	PlayerController[] _players;

	[SerializeField]
	GameObject[] _playerLocationA, playerLocationB;


	const int PLAYER_NUM = 3;

	bool[] _playerActives = new bool[PLAYER_NUM];
	List<ParamItem.ID>[] _playerItems = new List<ParamItem.ID>[PLAYER_NUM];

	ParamItem.ID _useItemId = ParamItem.ID.Invalid;
	ParamPlayer.ID _currentPlayer = ParamPlayer.ID.Momoka;
}
