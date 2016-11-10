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
	
	// Update is called once per frame
	void Update () {
	
	}

	List<string[]> parse()
	{
		/*List<string> parsed = new List<string> ();
		int position = 0;
		int start = 0;
		// Extract sentences from the string.
		do {
			position = toParse.IndexOf(" ", start);
			if (position >= 0) {
				parsed.Add(toParse.Substring(start, position - start + 1).Trim());
				start = position + 1;
			}
		} while (position > 0);*/
		/*Regex regex = new Regex (@"\n");
		string[] parsed = regex.Split (toParse);
		regex = new Regex (@" ");
		List<string[]> moreParsed = new List<string[]> ();
		for (int i = 0; i < parsed.Length; i++)
		{
			string[] parsedx = regex.Split (parsed [i]);
			moreParsed.Add (parsedx);
		}
		*/

		string[] test = toParse.Split ('\n');
		Debug.Log ("test size: " + test.Length);
		List<string[]> test2List = new List<string[]> ();
		for (int i = 0; i < test.Length; i++)
		{
			test2List.Add(test [i].Split (' '));
		}

		/*int tempint = test2List [0].Length;
		string[] array = new string[480];

		List<string[]> newstrings = new List<string[]> ();
		for (int i = 0; i < test2List.Count-1; i++)
		{
			for (int j = 0; j < array.Length; j++)
			{
				array [j] = test2List [i] [j];
			}
			newstrings.Add (array);
		}
		for (int i = newstrings.Count; i < 640; i++)
		{
			newstrings.Add (array);
		}*/

		return test2List;
	}
}
