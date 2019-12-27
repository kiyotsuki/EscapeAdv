using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkWindow : MonoBehaviour
{
	private void Start()
	{
		//SetText("わーいとりあえず\nやっていみよう");
	}
	
	void Update()
	{
		if(_isTalking == false)
		{
			return;
		}
		if(isViewEnd())
		{
			return;
		}
		
		_timer += Time.deltaTime;
		if(_timer > 0.1f)
		{
			_timer = 0;
			_talkText.text += _sourceText[_viewLength];

			_viewLength++;
			if(isViewEnd())
			{
				_nextIcon.SetActive(true);
			}
		}
	}

	/// <summary>
	/// 会話中か判定
	/// 主にWaitのために利用する
	/// </summary>
	/// <returns></returns>
	public bool IsTalking()
	{
		return _isTalking;
	}

	/// <summary>
	/// 会話が表示終了したかどうか
	/// </summary>
	/// <returns></returns>
	private bool isViewEnd()
	{
		return _sourceText.Length <= _viewLength;
	}

	/// <summary>
	/// クリックコールバック
	/// 文字が表示しきっていたら会話終了
	/// 表示中なら最後まで表示
	/// </summary>
	public void OnClick()
	{
		if (_isTalking == false)
		{
			return;
		}
		if (isViewEnd() == false)
		{
			SkipTalk();
			return;
		}
		_isTalking = false;
	}
	
	/// <summary>
	/// 会話の表示を開始する
	/// テキスト欄をリセットしてメッセージの表示を開始する
	/// </summary>
	/// <param name="name"></param>
	/// <param name="text"></param>
	/// <returns></returns>
	public void SetText(string text)
	{
		// 会話テキストクリア
		_talkText.text = "";
		_sourceText = "";
		_viewLength = 0;

		// テキスト追加
		AddText(text);
	}

	/// <summary>
	/// 現在のテキストを消さずにテキストを追加する
	/// </summary>
	/// <param name="text"></param>
	/// <returns></returns>
	public void AddText(string text)
	{
		_timer = 0;
		_isTalking = true;
		_sourceText += text;
		_nextIcon.SetActive(false);

		gameObject.SetActive(true);
	}

	/// <summary>
	/// 現在表示中のトークを最後まで表示する
	/// </summary>
	public void SkipTalk()
	{
		_talkText.text = _sourceText;
		_viewLength = _sourceText.Length;
		_nextIcon.SetActive(true);
	}

	/// <summary>
	/// 非表示にする
	/// </summary>
	public void Hide()
	{
		gameObject.SetActive(false);
	}
	

	bool _isTalking = false;
	float _timer = 0;
	string _sourceText = "";
	int _viewLength = 0;

	[SerializeField]
	Text _talkText;

	[SerializeField]
	GameObject _nextIcon;
}
