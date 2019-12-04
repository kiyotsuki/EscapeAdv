using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuElement : MonoBehaviour
{
	public void Awake()
	{
		_checkButton.onClick.AddListener(onClickCheck);
		_useButton.onClick.AddListener(onClickUse);
	}

	public void Setup(string name, string checkScenario, string useScenario)
	{
		_nameLabel.text = name;
		_checkScenario = checkScenario;
		_useScenario = useScenario;
	}

	private void onClickCheck()
	{
		var scenarioManager = GameUtil.GetManager<ScenarioManager>();
		scenarioManager.ExecuteScenario(_checkScenario);
	}

	private void onClickUse()
	{
		var scenarioManager = GameUtil.GetManager<ScenarioManager>();
		scenarioManager.ExecuteScenario(_useScenario);
	}

	[SerializeField]
	private Button _checkButton, _useButton;

	[SerializeField]
	private Image _iconImage;

	[SerializeField]
	private Text _nameLabel;

	private string _useScenario, _checkScenario;
}
