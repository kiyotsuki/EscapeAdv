using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : ManagerBase
{
	public override void Initialize()
	{
	}
	
	public override void OnUpdate()
	{
	}
	

	/// <summary>
	/// シナリオ開始通知
	/// 移動などをキャンセルする
	/// </summary>
	public void OnStartScenario()
	{
	}

	/// <summary>
	/// マップ変更通知
	/// マップ切り替え後は全プレイヤーを非表示にする
	/// </summary>
	public void OnChangeMap(MapData map)
	{
	}

	/// <summary>
	/// プレイヤー配置
	/// この時プレイヤーをアクティブ化する
	/// </summary>
	public void SetPlayer(ParamPlayer.ID id, int x, int y)
	{
	}

	/// <summary>
	/// カレントプレイヤー変更
	/// 操作するプレイヤーを指定したものに切り替える
	/// </summary>
	public void ChangePlayer(ParamPlayer.ID id)
	{
	}
}
