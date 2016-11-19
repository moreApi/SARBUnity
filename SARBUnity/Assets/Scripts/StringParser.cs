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
		Debug.Log (myStrings.Count + " 1 " + myStrings [0].Length);
	}

	public void updateString(string heightData)
	{
		toParse = heightData;
		myStrings = parse2 ();
		Debug.Log (myStrings.Count + " 2 " + myStrings [479].Length);
	}

	List<string[]> parse()
	{
		Debug.Log(toParse.Length);
		string[] test = toParse.Split ('\n');
		Debug.Log ("test size: " + test.Length);
		List<string[]> test2List = new List<string[]> ();
		for (int i = 0; i < test.Length; i++)
		{
			test2List.Add(test [i].Split (' '));
		}
		Debug.Log (test2List.Count);
		return test2List;
	}
	List<string[]> parse2()
	{
		Debug.Log(toParse.Length);
		string[] test = toParse.Split (' ');
		Debug.Log ("test size: " + test.Length);
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
		Debug.Log (test2List.Count);
		System.IO.File.WriteAllLines("C:\\Temp\\heightmap.txt",test);
		return test2List;
	}
}
