using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckEventData : MonoBehaviour
{
	public void Start()
	{
		// リスナー登録
		_button.onClick.AddListener(OnClick);
	}

	public void OnClick()
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
			var ppos = playerManager.GetPlayerPos();

			var diff = mpos - ppos;
			if(diff.sqrMagnitude < 50 * 50)
			{
				_button.gameObject.SetActive(true);
			}
			else
			{
				_button.gameObject.SetActive(false);
			}
		}
	}

	[SerializeField]
	Button _button;
	
	[SerializeField]
	string _scenario;
}
