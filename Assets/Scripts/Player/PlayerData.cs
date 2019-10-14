using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData
{
	public PlayerData(ParamPlayer.ID id, GameObject icon)
	{
		_id = id;
		_icon = icon;
		
		// アイコンをクリックしたらそのキャラに切り替える
		_button = icon.GetComponent<Button>();
		_button.onClick.AddListener(() =>
		{
			var manager = GameUtil.GetManager<PlayerManager>();
			manager.ChangePlayer(_id);
		});
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

	ParamPlayer.ID _id;
	GameObject _icon;
	Button _button;
}
