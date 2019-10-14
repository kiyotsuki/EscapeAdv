using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : ManagerBase
{
	public override void Initialize()
	{
	}

	public override void OnUpdate()
	{
	}

	public bool GetFlag(FlagId id)
	{
		if(_flags.ContainsKey(id) == false)
		{
			return false;
		}
		return _flags[id];
	}

	public void SetFlag(FlagId id, bool flag)
	{
		if (_flags.ContainsKey(id) == false)
		{
			_flags.Add(id, flag);
		}
		else
		{
			_flags[id] = flag;
		}
	}

	public enum FlagId
	{
		Test = 0,
	}

	Dictionary<FlagId, bool> _flags = new Dictionary<FlagId, bool>();
}
