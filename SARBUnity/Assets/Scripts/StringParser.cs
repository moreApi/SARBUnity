using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class StringParser : MonoBehaviour {

	[HideInInspector]
	public string toParse;

	[HideInInspector]
	public List<string[]> myStrings;

	// Use this for initialization
	void Start () {
		TextAsset bindata= Resources.Load("heightmapData") as TextAsset;
		toParse = bindata.text;
		myStrings = parse ();
	}

	public void updateString(string heightData)
	{
		toParse = heightData;
		myStrings = parse2 ();
	}

	List<string[]> parse()
	{
		string[] test = toParse.Split ('\n');
		List<string[]> test2List = new List<string[]> ();
		for (int i = 0; i < test.Length; i++)
		{
			test2List.Add(test [i].Split (' '));
		}
		return test2List;
	}
	List<string[]> parse2()
	{
		string[] test = toParse.Split (' ');
		List<string[]> test2List = new List<string[]> ();
		string[] tempStrArray;
		for (int i = 0; i < 640; i++)
		{
			tempStrArray = new string[480];
			for (int j = 0; j < 480; j++)
			{
				tempStrArray [j] = test [(i * 480) + j];
			}
			test2List.Add (tempStrArray);
		}
		return test2List;
	}
}
