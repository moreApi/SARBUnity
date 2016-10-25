using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class StringParser : MonoBehaviour {

	string toParse;

	// Use this for initialization
	void Start () {

		TextAsset bindata= Resources.Load("heightmapData") as TextAsset;
		toParse = bindata.text;
		parse ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void parse()
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
		Regex regex = new Regex (@"\n");
		string[] parsed = regex.Split (toParse);
		regex = new Regex (@" ");
		List<string[]> moreParsed = new List<string[]> ();
		for (int i = 0; i < parsed.Length; i++)
		{
			string[] parsedx = regex.Split (parsed [i]);
			moreParsed.Add (parsedx);
		}
		for (int i = 0; i < moreParsed [0].Length; i++)
		{
			Debug.Log ("1: " + moreParsed [0] [i]);
		}
		for (int i = 0; i < moreParsed [moreParsed.Count - 1].Length; i++)
		{
			Debug.Log ("2: " + moreParsed [moreParsed.Count - 1] [i]);
		}
		string[] test = toParse.Split ('\n');
		Debug.Log ("test size: " + test.Length);
		List<string[]> test2List = new List<string[]> ();
		for (int i = 0; i < test.Length; i++)
		{
			test2List.Add(test [i].Split (' '));
		}
		Debug.Log ("test2 first size: " + test2List[0].Length);
		Debug.Log ("test2 last size: " + test2List[test2List.Count-1].Length);

	}
}
