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
			_playerIndex -= 1;
			if (_playerIndex < 0) _playerIndex = 2;
			requestMove();
		});
		_charaChangeButtonR.onClick.AddListener(() =>
		{
			_playerIndex += 1;
			if (_playerIndex > 2) _playerIndex = 0;
			requestMove();
		});
		requestMove();
		yield break;
	}

	private void requestMove()
	{
		_originRotation = _playerStage.transform.rotation;
		_targetRotation = Quaternion.Euler(0, _playerIndex * 120 + 180, 0);
		_rotationTime = 0;
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

	public ParamItem.ID GetUseItemId()
	{
		return _useItemId;
	}

	public override void OnUpdateGame()
	{
		if (_rotationTime >= 0)
		{
			_rotationTime += Time.deltaTime;
			var rate = _rotationTime / 0.3f;
			_playerStage.transform.rotation = Quaternion.Slerp(_originRotation, _targetRotation, rate);
			if(rate > 1)
			{
				_rotationTime = -1;
			}
		}
	}

	[SerializeField]
	GameObject _hudCanvas;

	[SerializeField]
	Button _itemMenuButton;

	[SerializeField]
	Button _charaChangeButtonL;

	[SerializeField]
	Button _charaChangeButtonR;

	[SerializeField]
	GameObject _playerStage;


	[SerializeField]
	GameItem _useItemDisplay;

	float _rotationTime = 0;
	Quaternion _originRotation, _targetRotation;

	int _playerIndex = 0;
	float _rot;
	ParamItem.ID _useItemId = ParamItem.ID.Invalid;
}
