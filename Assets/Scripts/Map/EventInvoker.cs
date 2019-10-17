using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EventInvoker : MonoBehaviour
{
	public void Start()
	{
		// リスナー登録
		_button.onClick.AddListener(execute);
		_button.gameObject.SetActive(false);
	}

	private void execute()
	{
		// シナリオ実行
		ScenarioUtil.ExecuteScenario(_scenario);
	}
	
	public void Update()
	{
		var playerManager = GameUtil.GetManager<PlayerManager>();
		if(playerManager != null)
		{
			var mpos = (Vector2)this.transform.position;
			var ppos = playerManager.GetPlayerController().GetPos();

			bool enter = false;
			var diff = mpos - ppos;
			if (diff.sqrMagnitude < _distance * _distance)
			{
				enter = true;
			}

			if(_preEnter != enter)
			{
				_button.gameObject.SetActive(enter);
				_preEnter = enter;
			}
		}
	}
	
	[SerializeField]
	Button _button;
	
	[SerializeField]
	string _scenario;

	[SerializeField]
	float _distance;

	bool _preEnter = false;
}
