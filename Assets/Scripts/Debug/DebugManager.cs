using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : ManagerBase
{
	public override void Initialize()
	{
		_debugCanvas = GameUtil.GetNamedSceneObject("DebugCanvas");
		_debugCanvas.SetActive(true);

		_listContent = GameUtil.FindChild(_debugCanvas, "Scroll View/Viewport/Content");

		_buttonSource = GameUtil.FindChild(_debugCanvas, "Button");
		_buttonSource.SetActive(false);

		OpenPage(new DebugMainPage());
	}

	public void AddButton(string label, System.Action action)
	{
		var go = GameObject.Instantiate(_buttonSource);
		_buttonList.Add(go);

		var text = go.GetComponentInChildren<Text>();
		text.text = label;

		go.SetActive(true);
		go.transform.SetParent(_listContent.transform, false);

		var button = go.GetComponent<Button>();
		button.onClick.AddListener(() => { action(); });
	}

	public void OpenPage(DebugPage page)
	{
		// 現在のボタンをすべて削除
		foreach (var go in _buttonList)
		{
			GameObject.Destroy(go);
		}

		page.Open(this);
		AddButton("戻る", backPage);

		if(_page != null)
		{
			_history.Add(_page);
		}
		_page = page;
	}

	private void backPage()
	{
		if(_history.Count == 0)
		{
			HideMenu();
			return;
		}

		// 一つ前のページを取得して再度開く
		var prePage = _history[_history.Count - 1];
		_history.Remove(prePage);

		// 現在のページをnullにして多重でHistory登録されないようにする
		_page = null;
		OpenPage(prePage);
	}

	public void HideMenu()
	{
		_debugCanvas.SetActive(false);
	}

	public override void OnUpdate()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (_debugCanvas.activeSelf == false)
			{
				_debugCanvas.SetActive(true);
			}
		}
	}

	/// <summary>
	/// デバッグページ基礎クラス
	/// </summary>
	public abstract class DebugPage
	{
		public abstract void Open(DebugManager manager);
	}

	private DebugPage _page = null;

	private List<DebugPage> _history = new List<DebugPage>();
	private List<GameObject> _buttonList = new List<GameObject>();

	private GameObject _debugCanvas = null;

	private GameObject _buttonSource = null;
	private GameObject _listContent = null;
}
