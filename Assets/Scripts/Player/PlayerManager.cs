using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : ManagerBase
{
	public override void Initialize()
	{
		// マスターデータからプレイヤーを作成
		for (int i = 0; i < ParamPlayer.Count; i++)
		{
			var data = ParamPlayer.Get(i);
			var icon = GameUtil.GetNamedSceneObject(data.IconName);

			_players.Add(new PlayerData(data.Id, icon));
		}
	}

	public override void OnUpdate()
	{
		// プレイヤーがいる場合、操作を行う
		if (_currentPlayer != null)
		{
			if (Input.GetMouseButton(0))
			{
				var targetPos = (Vector2)Input.mousePosition;
				var playerPos = _currentPlayer.GetPos();

				var map = GameUtil.GetCurrentMap();
				_moveRoute = map.GetRoute(playerPos, targetPos);
			}

			// リクエストされた移動ルートがある場合移動
			if (_moveRoute != null && _moveRoute.Count > 0)
			{
				var playerPos = _currentPlayer.GetPos();

				var r = _moveRoute[0];

				var diff = r - (Vector2)playerPos;
				var sqrMag = diff.sqrMagnitude;

				if (sqrMag < 5 * 5)
				{
					_moveRoute.RemoveAt(0);
				}
				else
				{
					int spd = 200;

					var dir = diff.normalized * spd;
					playerPos.x += dir.x * Time.deltaTime;
					playerPos.y += dir.y * Time.deltaTime;

					_currentPlayer.SetPos(playerPos);
				}
			}
		}
	}

	/// <summary>
	/// マップ変更通知
	/// マップ切り替え後は全プレイヤーを非表示にする
	/// </summary>
	public void OnChangeMap(MapData map)
	{
		var playerRoot = map.GetPlayerRoot();
		foreach (var player in _players)
		{
			var icon = player.GetIcon();
			icon.transform.SetParent(playerRoot.transform);
			icon.SetActive(false);
		}
		_currentPlayer = null;
		_moveRoute = null;
	}

	/// <summary>
	/// プレイヤー配置
	/// この時プレイヤーをアクティブ化する
	/// </summary>
	public void SetPlayer(ParamPlayer.ID id, int x, int y)
	{
		var player = _players[(int)id];
		player.SetActive(true);

		var mapData = GameUtil.GetCurrentMap();
		var chipData = mapData.GetMapChipData(x, y);

		player.SetPos(chipData.GetPos());
		ChangePlayer(id);
	}

	/// <summary>
	/// カレントプレイヤー変更
	/// 操作するプレイヤーを指定したものに切り替える
	/// </summary>
	public void ChangePlayer(ParamPlayer.ID id)
	{
		var player = _players[(int)id];
		_currentPlayer = player;

		_moveRoute = null;
	}

	public Vector2 GetPlayerPos()
	{
		if(_currentPlayer == null)
		{
			return Vector2.zero;
		}
		return _currentPlayer.GetPos();
	}

	public bool IsPlayerMoving()
	{
		if(_moveRoute == null)
		{
			return false;
		}
		return _moveRoute.Count > 0;
	}

	PlayerData _currentPlayer = null;
	List<PlayerData> _players = new List<PlayerData>();
	List<PlayerData> _activePlayers = new List<PlayerData>();

	List<Vector2> _moveRoute = null;
}
