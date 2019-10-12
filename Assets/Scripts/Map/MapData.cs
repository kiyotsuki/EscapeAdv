using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
	public void SetMapChips(int width, int height, MapChipData[] mapChips)
	{
		_width = width;
		_height = height;

		_mapChips = mapChips;
	}

	public List<Vector2> GetRoute(Vector2 start, Vector2 goal)
	{
		var startChip = GetNearMapChipData(start);
		var goalChip = GetNearMapChipData(goal);
		if(startChip == goalChip)
		{
			return null;
		}

		if(_naviMap == null)
		{
			_naviMap = new int[_mapChips.Length];
		}
		for (int i = 0; i < _naviMap.Length; i++)
		{
			_naviMap[i] = int.MaxValue;
		}

		// ナビマップ更新
		setupNaviMap(startChip.GetIndex(), goalChip.GetIndex(), 0);


		int lastIndex = goalChip.GetIndex();
		int nIndex = lastIndex;

		var route = new List<Vector2>();
		route.Add(goalChip.transform.position);

		for (int i = 0; i < 1000; i++)
		{
			var next = getMinValueIndex(nIndex);
			if (checkRoute(lastIndex, next) == false)
			{
				// 先頭に追加していく
				route.Insert(0, _mapChips[nIndex].transform.position);
				lastIndex = nIndex;
			}
			if (next == startChip.GetIndex())
			{
				//スタート地点にたどり着いた
				break;
			}
			nIndex = next;
		}
		
		return route;
	}


	private bool setupNaviMap(int index, int target, int value)
	{
		if (index >= _mapChips.Length || index < 0)
		{
			// マップ外に出たら終了
			return false;
		}
		var chip = _mapChips[index];
		
		if (chip.GetColision())
		{
			// 壁に当たったら終わり
			return false;
		}
		if (_naviMap[index] <= value)
		{
			// 既により小さい値がセットされていたら終了
			return true;
		}
		_naviMap[index] = value;
		if (index == target)
		{
			// ゴールに到達したなら終了
			return true;
		}

		// 上下左右
		int nextValue = value + 10;

		var l = setupNaviMap(index - 1, target, nextValue);
		var r = setupNaviMap(index + 1, target, nextValue);
		var u = setupNaviMap(index - _width, target, nextValue);
		var d = setupNaviMap(index + _width, target, nextValue);

		/*
		// 斜め四方向
		nextValue += 4;

		if (l && u) setupNaviMap(index - 1 - _width, target, nextValue);
		if (r && u) setupNaviMap(index + 1 - _width, target, nextValue);
		if (l && d) setupNaviMap(index - 1 + _width, target, nextValue);
		if (r && d) setupNaviMap(index + 1 + _width, target, nextValue);
		*/
		return true;
	}

	private int getMinValueIndex(int index)
	{
		// 最大8方向
		int[] validIndexs = new int[8];
		int count = 0;

		var l = index - 1;
		if (checkColision(l)) validIndexs[count++] = l;

		var r = index + 1;
		if (checkColision(r)) validIndexs[count++] = r;

		var u = index - _width;
		if (checkColision(u))
		{
			validIndexs[count++] = u;
			if (checkColision(l)) validIndexs[count++] = u - 1;
			if (checkColision(r)) validIndexs[count++] = u + 1;
		}

		var d = index + _width;
		if (checkColision(d))
		{
			validIndexs[count++] = d;
			if (checkColision(l)) validIndexs[count++] = d - 1;
			if (checkColision(r)) validIndexs[count++] = d + 1;
		}

		int min = int.MaxValue;
		int ret = 0;
		for (int i = 0; i < count; i++)
		{
			var value = _naviMap[validIndexs[i]];
			if (min > value)
			{
				min = value;
				ret = validIndexs[i];
			}
		}
		return ret;
	}

	private bool checkRoute(int st, int ed)
	{
		if (checkColision(st) == false || checkColision(ed) == false)
		{
			return false;
		}

		float rad = 0.3f;
		float checkLength = 0.1f;

		var stPos = new Vector2((int)(st % _width), (int)(st / _width)) + new Vector2(0.5f, 0.5f);
		var edPos = new Vector2((int)(ed % _width), (int)(ed / _width)) + new Vector2(0.5f, 0.5f);

		var diff = edPos - stPos;
		var sqrMag = diff.sqrMagnitude;
		var vec = diff.normalized;

		Vector2 checkVec = Quaternion.Euler(0, 0, 90) * (vec * rad);

		var posList = new Vector2[] { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, -1) };
		for (int i = 1; i < 1000; i++)
		{
			var mag = i * checkLength;
			if (sqrMag < mag * mag)
			{
				break;
			}
			var pos = stPos + vec * mag;

			var checkA = pos + checkVec;
			var checkB = pos - checkVec;

			var aIndex = (int)(checkA.y) * _width + (int)(checkA.x);
			var bIndex = (int)(checkB.y) * _width + (int)(checkB.x);

			if (checkColision(aIndex) == false || checkColision(bIndex) == false)
			{
				return false;
			}
		}
		return true;
	}

	private bool checkColision(int index)
	{
		var chip = GetMapChipData(index);
		return chip != null ? chip.GetColision() == false : false;
	}

	public MapChipData GetMapChipData(int index)
	{
		if (index < 0 || index >= _mapChips.Length)
		{
			return null;
		}
		return _mapChips[index];
	}

	public MapChipData GetMapChipData(int x, int y)
	{
		int index = y * _width + x;
		return GetMapChipData(index);
	}

	public MapChipData GetNearMapChipData(Vector3 pos)
	{
		float lengthSqr = float.MaxValue;
		var ret = _mapChips[0];
		foreach (var chip in _mapChips)
		{
			if (chip.GetColision())
			{
				continue;
			}
			var diff = chip.transform.position - pos;
			if (lengthSqr > diff.sqrMagnitude)
			{
				lengthSqr = diff.sqrMagnitude;
				ret = chip;
			}
		}
		return ret;
	}

	[SerializeField]
	private int _width;

	[SerializeField]
	private int _height;

	[SerializeField]
	private MapChipData[] _mapChips;

	private int[] _naviMap = null;
}
