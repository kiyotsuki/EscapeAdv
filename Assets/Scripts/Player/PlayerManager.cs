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

			var button = icon.GetComponent<Button>();
			button.onClick.AddListener(() => {
				ChangePlayer(data.Id);
			});
			_playerControllers.Add(data.Id, new PlayerController(data, icon));
		}
	}
	
	public override void OnUpdate()
	{
		foreach(var v in _playerControllers)
		{
			v.Value.OnUpdate();
		}

		// プレイヤーがいる場合、操作を行う
		if (_currentPlayerController != null && ScenarioUtil.IsInScenario() == false)
		{
			if (Input.GetMouseButton(0))
			{
				var targetPos = (Vector2)Input.mousePosition;				
				_currentPlayerController.RequestMove(targetPos);
			}
		}
	}
	

	/// <summary>
	/// シナリオ開始通知
	/// 移動などをキャンセルする
	/// </summary>
	public void OnStartScenario()
	{
		foreach (var v in _playerControllers)
		{
			v.Value.CancelMove();
		}
	}

	/// <summary>
	/// マップ変更通知
	/// マップ切り替え後は全プレイヤーを非表示にする
	/// </summary>
	public void OnChangeMap(MapData map)
	{
		var playerRoot = map.GetPlayerRoot();
		foreach (var v in _playerControllers)
		{
			var icon = v.Value.GetIcon();
			icon.transform.SetParent(playerRoot.transform);
			icon.SetActive(false);
		}
		_currentPlayerController = null;
	}

	/// <summary>
	/// プレイヤー配置
	/// この時プレイヤーをアクティブ化する
	/// </summary>
	public void SetPlayer(ParamPlayer.ID id, int x, int y)
	{
		var player = _playerControllers[id];
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
		var player = _playerControllers[id];
		_currentPlayerController = player;
	}
	
	public PlayerController GetPlayerController(ParamPlayer.ID id = ParamPlayer.ID.Invalid)
	{
		if(id == ParamPlayer.ID.Invalid)
		{
			return _currentPlayerController;
		}
		return _playerControllers[id];
	}


	PlayerController _currentPlayerController = null;
	Dictionary<ParamPlayer.ID, PlayerController> _playerControllers = new Dictionary<ParamPlayer.ID, PlayerController>();
}
