using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

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
        Debug.Log("starting parsing");
		string[] test = toParse.Split (' ');
        Debug.Log("map string: " + toParse.Substring(500 * 240, 240));
        Debug.Log("line 300: " + test[300]);
		List<string[]> test2List = new List<string[]> ();
		string[] tempStrArray;
        int i = 0;
        int j = 0;
        try
        {
            for (; i < 640; i++)
            {
                tempStrArray = new string[480];
                for (; j < 480; j++)
                {
                    //Debug.Log("i: " + i + " j: " + j);
                    tempStrArray[j] = test[(i * 480) + j];
                }
                Debug.Log("Line "+i+": "+tempStrArray[240]);
                test2List.Add(tempStrArray);
            }
        }catch (Exception e){
            Debug.Log("got an exception in parsing");
            Debug.Log(e);
        }
        return test2List;
	}
}
