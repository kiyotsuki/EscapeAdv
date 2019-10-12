using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

/// <summary>
/// マップジェネレータ
/// Stringからマップを生成する
/// </summary>
public class MapGenerator
{
	[MenuItem("Extend/MapGenerate")]
	public static void AllMapGenerate()
	{
		var canvas = GameObject.Find("MapCanvas");
		var chipSource = canvas.transform.Find("MapChip").gameObject;

		var generator = new MapGenerator(canvas, chipSource);
		generator.CreateMap("TestMap", @"
$A(*)
$P( )

[#][#][/][#][#][#][#][#][#][#][/][/][#][#][#]
[#][*][ ][*][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][#]
[#][*][ ][ ][ ][*][ ][ ][ ][ ][ ][ ][*][ ][#]
[#][*][*][*][*][*][ ][ ][ ][ ][ ][ ][ ][ ][#]
[#][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][#]
[#][ ][ ][ ][ ][ ][ ][ ][*][*][ ][*][*][ ][#]
[#][ ][ ][*][*][*][ ][ ][ ][ ][ ][ ][ ][ ][#]
[#][ ][ ][*][/][*][ ][ ][*][*][ ][*][*][ ][#]
[/][ ][ ][*][*][*][ ][ ][ ][ ][ ][ ][ ][ ][#]
[/][ ][ ][ ][ ][ ][ ][ ][*][*][ ][*][*][ ][#]
[#][ ][ ][ ][ ][ ][ ][P][ ][ ][ ][ ][ ][ ][#]
[#][#][#][#][#][#][/][/][#][#][#][#][#][#][#]
");
	}

	public MapGenerator(GameObject canvas, GameObject chipSource)
	{
		_mapCanavs = canvas;
		_mapChipSource = chipSource;
	}

	/// <summary>
	/// すべてのパラメータをビルドする
	/// </summary>
	private void CreateMap(string name, string mapCode)
	{
		var mapRoot = new GameObject(name);
		mapRoot.transform.SetParent(_mapCanavs.transform);
		var mapData = mapRoot.AddComponent<MapData>();

		List<MapChipData> mapChipDataList = new List<MapChipData>();


		// []と\rを除去して改行で分割
		var regex = new System.Text.RegularExpressions.Regex("\\[|\\]|\r");
		var codes = regex.Replace(mapCode, "").Split('\n');

		var map = new List<string>();
		var dict = new Dictionary<char, char>();
		foreach (var code in codes)
		{
			if (code.Length == 0)
			{
				continue;
			}
			if (code[0] == '$')
			{
				// $A(*) のような形式 Index1を3に変換する
				dict.Add(code[1], code[3]);
				continue;
			}
			map.Add(code);
		}

		var mapH = map.Count;
		var mapW = map[0].Length;

		int index = 0;
		for (int y = 0; y < mapH; y++)
		{
			for (int x = 0; x < mapW; x++)
			{
				var chip = GameObject.Instantiate(_mapChipSource);
				chip.name = $"({x},{y})";
				var chipData = chip.GetComponent<MapChipData>();

				chip.transform.SetParent(mapRoot.transform);
				mapChipDataList.Add(chipData);

				chip.transform.position = new Vector3(x * 100, y * -100, 0) + new Vector3((mapW-1) * -50, (mapH-1) * 50);
				
				var mapChip = map[y][x];
				if (dict.ContainsKey(mapChip))
				{
					mapChip = dict[mapChip];
				}
				switch (mapChip)
				{
					case ' ':
						chipData.Setup(index, new Color(0.8f, 0.8f, 0.8f), false);
						break;

					case '#':
						chipData.Setup(index, new Color(0, 0, 0), true);
						break;

					case '*':
						chipData.Setup(index, new Color(0.5f, 0.5f, 0.5f), true);
						break;

					case '/':
						chipData.Setup(index, new Color(0.3f, 0.3f, 0.3f), true);
						break;
				}

				index++;
			}
		}

		mapData.SetMapChips(mapW, mapH, mapChipDataList.ToArray());

		mapRoot.transform.localScale = new Vector3(0.4f, 0.4f, 1);
		mapRoot.transform.localPosition = new Vector3(0, 300, 0);
	}

	GameObject _mapCanavs = null;
	GameObject _mapChipSource = null;
}
