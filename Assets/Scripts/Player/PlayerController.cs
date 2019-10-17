﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController
{
	public PlayerController(ParamPlayer.Data data, GameObject icon)
	{
		_id = data.Id;
		_icon = icon;
		
		// アイコンをクリックしたらそのキャラに切り替える
		_button = icon.GetComponent<Button>();
		_button.onClick.AddListener(() =>
		{
			var manager = GameUtil.GetManager<PlayerManager>();
			manager.ChangePlayer(_id);
		});

		// 初期ハート設定
		_heart = data.InitHeart;

		_item = ParamItem.ID.Invalid;
	}

	public void OnUpdate()
	{
		if(_icon.activeSelf == false)
		{
			return;
		}

		if (_moveRoute != null && _moveRoute.Count > 0)
		{
			var playerPos = GetPos();

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

				SetPos(playerPos);
			}
		}
	}

	public Vector2 GetPos()
	{
		return _icon.transform.position;
	}

	public void SetPos(Vector2 pos)
	{
		_icon.transform.position = pos;
	}

	public GameObject GetIcon()
	{
		return _icon;
	}

	public void SetActive(bool b)
	{
		_icon.SetActive(b);
	}

	public void SetHeart(ParamHeart.ID heart)
	{
		_heart = heart;
	}

	public ParamHeart.ID GetHeart()
	{
		return _heart;
	}

	public void AddItem(ParamItem.ID item)
	{
		_takeItems.Add(item);
	}

	public ParamItem.ID GetItem()
	{
		return _item;
	}

	public void SetItem(ParamItem.ID id)
	{
		_item = id;
	}

	public void RequestMove(int cx, int cy)
	{
		var crrentMap = GameUtil.GetCurrentMap();
		var chip = crrentMap.GetMapChipData(cx, cy);
		_moveRoute = crrentMap.GetRoute(GetPos(), chip.GetPos());
	}

	public void RequestMove(Vector2 pos)
	{
		var crrentMap = GameUtil.GetCurrentMap();
		_moveRoute = crrentMap.GetRoute(GetPos(), pos);
	}

	public void CancelMove()
	{
		_moveRoute = null;
	}

	public bool IsMoving()
	{
		return _moveRoute != null ? _moveRoute.Count > 0 : false;
	}

	ParamPlayer.ID _id;
	GameObject _icon;
	Button _button;

	List<ParamItem.ID> _takeItems = new List<ParamItem.ID>();
	ParamItem.ID _item = ParamItem.ID.Invalid;
	ParamHeart.ID _heart = ParamHeart.ID.Invalid;

	List<Vector2> _moveRoute;
}
