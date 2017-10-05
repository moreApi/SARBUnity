using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public class StringParser : MonoBehaviour {

	[HideInInspector]
	public string toParse;

	[HideInInspector]
	public List<float[]> myStrings;

	// Use this for initialization
	void Start () {
	}

	public void updateString(string heightData)
	{
		toParse = heightData;
		myStrings = parse2 ();
	}
	

	
	List<float[]> parse2()
	{
        Debug.Log("starting parsing");
		string[] test = toParse.Split (' ');
        //Debug.Log("map string: " + toParse.Substring(500 * 240, 240));
        //Debug.Log("entry 300: " + test[300]);
        //Debug.Log("String length: "+toParse.Length+ " array length: "+test.Length);
		List<float[]> test2List = new List<float[]> ();
		float[] tempStrArray;
        int i = 0;
        int j = 0;
        try
        {
            for (i = 0; i < 640; i++)
            {
                tempStrArray = new float[480];
                for (j = 0; j < 480; j++)
                {
                    //Debug.Log("i: " + i + " j: " + j);
                    tempStrArray[j] = float.Parse(test[(i * 480) + j]);
                }
                //Debug.Log("Line "+i+": "+tempStrArray[240]);
                test2List.Add(tempStrArray);
            }
        }catch {
            Debug.Log("got an exception in parsing");
        }
        Debug.Log("end parsing");
        return test2List;
	}
}
