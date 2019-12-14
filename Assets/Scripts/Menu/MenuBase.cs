using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBase : MonoBehaviour
{
	public virtual void Open()
	{
		_animator.SetTrigger("In");
	}

	public virtual void Close()
	{
		_animator.SetTrigger("Out");
	}
	
	[SerializeField]
	Animator _animator = null;
}
