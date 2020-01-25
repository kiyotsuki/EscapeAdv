using UnityEngine;
using System;
using UnityEngine.UI;

public class SimpleDialog : DialogContentBase
{
	public void Start()
	{
		_okButton.onClick.AddListener(() =>
		{
			Close(_onClickOk);
		});
		_yesButton.onClick.AddListener(() =>
		{
			Close(_onClickOk);
		});

		_noButton.onClick.AddListener(() =>
		{
			Close(_onClickNg);
		});
	}

	public void Setup(string text, Action onClickOk)
	{
		_label.text = text;
		_yesButton.gameObject.SetActive(false);
		_noButton.gameObject.SetActive(false);
		_onClickOk = onClickOk;
	}

	public void Setup(string text, Action onClickOk, Action onClickNg)
	{
		_label.text = text;
		_okButton.gameObject.SetActive(false);
		_onClickOk = onClickOk;
		_onClickNg = onClickNg;
	}

	[SerializeField]
	private Text _label;

	[SerializeField]
	private Button _okButton, _yesButton, _noButton;
	
	private Action _onClickOk, _onClickNg;
}
