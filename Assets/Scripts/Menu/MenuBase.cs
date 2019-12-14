using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBase : MonoBehaviour
{
	public virtual void Open()
	{
		this.gameObject.SetActive(true);
		_animator.SetTrigger("In");
	}

	public virtual void Close()
	{
		_animator.SetTrigger("Out");
	}

	public virtual void Hide()
	{
		this.gameObject.SetActive(false);
	}

	public void Update()
	{
		if(_reqestHide)
		{
			_reqestHide = false;
			Hide();
		}
	}

	[SerializeField]
	bool _reqestHide = false;

	[SerializeField]
	Animator _animator = null;
}
