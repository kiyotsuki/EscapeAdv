using UnityEngine;
using System;


public class DialogContentBase : MonoBehaviour
{
	public void SetFrame(DialogFrame frame)
	{
		_frame = frame;
	}

	public virtual void OnOpen()
	{

	}

	public virtual void OnClose()
	{

	}

	protected void Close(Action onFinish)
	{
		_frame.Close(onFinish);
	}
	
	private DialogFrame _frame;
}
