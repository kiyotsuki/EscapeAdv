using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

/// <summary>
/// パラメータジェネレータ
/// CSVファイルを読み取ってCSファイルを作成する
/// </summary>
public class ParamGenerator
{
	[MenuItem("Extend/ParamGenerate")]
	public static void AllParamGenerate()
	{
		var generator = new ParamGenerator();
		generator.GenerateAll();
	}

	/// <summary>
	/// すべてのパラメータをビルドする
	/// </summary>
	private void GenerateAll()
	{
		Debug.Log("[ParamGenerator] Sart");

		// 読み込みパスと書き出しパス
		readPath = Application.dataPath + "/Editor/Param/";
		writePath = Application.dataPath + "/Scripts/Param/";

		// CSVが格納されているディレクトリからファイルの一覧を取得
		var csvDirectory = new System.IO.DirectoryInfo(readPath);
		var fileInfos = csvDirectory.GetFiles();

		// 作成すべきパラメータの一覧を取得
		var paramNames = new List<string>();
		foreach (var info in fileInfos)
		{
			var name = info.Name;
			if (name.EndsWith(".csv"))
			{
				name = name.Substring(0, name.Length - 4);
				paramNames.Add(name);
			}
		}
		Debug.Log("[ParamGenerator] Params : " + paramNames);

		//　それぞれのパラメータをジェネレート
		for (int i = 0; i < paramNames.Count; i++)
		{
			Generate(paramNames[i]);
		}

		Debug.Log("[ParamGenerator] Complete");
	}

	/// <summary>
	/// パラメータ名のCSVからクラスを生成する
	/// </summary>
	/// <param name="paramName"></param>
	private void Generate(string paramName)
	{
		var scriptName = "Param" + paramName;
		var fs = new FileStream(readPath + paramName + ".csv", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		var sr = new StreamReader(fs, Encoding.GetEncoding("shift-jis"));

		// ４行はクラス定義
		var headers = new string[4][];
		for (int i = 0; i < headers.Length; i++)
		{
			var line = sr.ReadLine();
			headers[i] = line.Split(',');
		}

		// １行無駄な行が入っているので読み飛ばし
		sr.ReadLine();

		// 以降はデータ行なのですべて取得
		dataSources.Clear();
		while (true)
		{
			var line = sr.ReadLine();
			if (line == null)
			{
				break;
			}
			dataSources.Add(line.Split(','));
		}
		sr.Close();
		Debug.Log($"[ParamGenerator] Read Success : {paramName}");

		//	ヘッダ情報から変数定義リストを作成
		valueDefines.Clear();
		var valueNames = headers[0];
		var valueTypes = headers[2];
		var valueInits = headers[3];
		for (int i = 1; i < valueNames.Length; i++)
		{
			var name = valueNames[i];
			var type = valueTypes[i];
			var init = valueInits[i];
			if (name == "" || type == "" || init == "")
			{
				// 変数名、型、初期値のどれかが空ならエラーを出力してスキップ
				Debug.LogError($"[ParamGenerator] Invalid Header (Name={name}, Type={type}, Init={init})");
				continue;
			}
			var def = new ParamValueDefine(i, name, type, init);
			valueDefines.Add(def);
		}

		var scriptBuilder = new ParamScriptBuilder(scriptName, valueDefines, dataSources);
		var code = scriptBuilder.Build();

		// ビルドしたソースコード書き出し
		var sw = new System.IO.StreamWriter(writePath + scriptName + ".cs", false);
		sw.Write(code);
		sw.Close();
		Debug.Log($"[ParamGenerator] Build Success : {paramName}");
	}

	private List<ParamValueDefine> valueDefines = new List<ParamValueDefine>();
	private List<string[]> dataSources = new List<string[]>();

	private string readPath = null;
	private string writePath = null;
}
