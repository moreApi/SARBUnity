using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public static class StringParser {

	public static float[,] parse(string toParse)
	{
        Debug.Log("starting parsing");
		string[] test = toParse.Split (' ');
        //Debug.Log("map string: " + toParse.Substring(500 * 240, 240));
        //Debug.Log("entry 300: " + test[300]);
        //Debug.Log("String length: "+toParse.Length+ " array length: "+test.Length);

        int xSize = 640;
        int ySize = 480;

		float[,] heightMap = new float[xSize, ySize];
        try
        {
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    //Debug.Log("i: " + i + " j: " + j);
                    heightMap[i,j] = parseNumber(test[(i * ySize) + j]);
                }
                //Debug.Log("Line "+i+": "+tempStrArray[240]);
            }
        }catch {
            Debug.Log("got an exception in parsing");
        }
        Debug.Log("end parsing");
        return heightMap;
	}

    private static float parseNumber(String toParse)
    {
        return float.Parse(toParse)/1000;
    }
}
