using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : ManagerBase
{
	protected override IEnumerator Setup()
	{
		_fadeCanvas.SetActive(true);

		// 最初は真っ暗にしておく
		//FadeOut(0);
		// とりあえずデバッグ用に開ける
		FadeIn(0);
		yield break;
	}

	public override void OnUpdateGame()
	{
		if (_fadeState == FadeState.None)
		{
			return;
		}

		_timer += Time.deltaTime;
		if (_timer >= _fadeTime)
		{
			endFade(_fadeState);
			return;
		}

		var rate = _timer / _fadeTime;
		if (_fadeState == FadeState.FadeOut)
		{
			_fadeColor.a = rate;
		}
		else
		{
			_fadeColor.a = 1.0f - rate;
		}
		_fadeImage.color = _fadeColor;
	}

	private void endFade(FadeState state)
	{
		_fadeTime = 0;
		_timer = 0;
		_fadeState = FadeState.None;

		_fadeColor.a = state == FadeState.FadeOut ? 1 : 0;
		_fadeImage.color = _fadeColor;

		if (state == FadeState.FadeIn)
		{
			// レイキャストブロックを解除
			_graphicRaycaster.enabled = false;
		}
	}

	private void startFade(FadeState state, float time)
	{
		// 全てのオブジェクトへのレイキャストをブロック
		_graphicRaycaster.enabled = true;

		if (time == 0)
		{
			endFade(state);
			return;
		}

		_timer = 0;
		_fadeTime = time;
		_fadeState = state;
	}
	
	public void FadeIn(float time = 0.3f)
	{
		startFade(FadeState.FadeIn, time);
	}

	public void FadeOut(float time = 0.3f)
	{
		startFade(FadeState.FadeOut, time);
	}

	/// <summary>
	/// フェードイン中もしくはフェードアウト中
	/// </summary>
	public bool IsFading()
	{
		return _fadeState != FadeState.None;
	}

	/// <summary>
	/// 完全にブラックアウトした状態を判定
	/// </summary>
	/// <returns></returns>
	public bool IsCoverd()
	{
		if (IsFading())
		{
			return false;
		}
		return _fadeColor.a == 1;
	}

	/// <summary>
	/// 全くフェードのない状態を判定
	/// </summary>
	/// <returns></returns>
	public bool IsClear()
	{
		if(IsFading())
		{
			return false;
		}
		return _fadeColor.a == 0;
	}


	[SerializeField]
	GameObject _fadeCanvas = null;

	[SerializeField]
	Image _fadeImage = null;

	[SerializeField]
	GraphicRaycaster _graphicRaycaster = null;

	enum FadeState
	{
		None,
		FadeIn,
		FadeOut,
	}
	FadeState _fadeState = FadeState.None;

	float _timer = 0;
	float _fadeTime = 0;
	Color _fadeColor = new Color(0, 0, 0, 0);

}
