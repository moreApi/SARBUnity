using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class StringParser : MonoBehaviour {

	string toParse;
	[HideInInspector]
	public List<string[]> myStrings;

	// Use this for initialization
	void Start () {

		TextAsset bindata= Resources.Load("heightmapData") as TextAsset;
		toParse = bindata.text;
		myStrings = parse ();
	}

	List<string[]> parse()
	{
		string[] test = toParse.Split ('\n');
		Debug.Log ("test size: " + test.Length);
		List<string[]> test2List = new List<string[]> ();
		for (int i = 0; i < test.Length; i++)
		{
			test2List.Add(test [i].Split (' '));
		}
		return test2List;
	}
}
