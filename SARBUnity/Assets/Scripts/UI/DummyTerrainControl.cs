using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTerrainControl : MonoBehaviour {

    Terrain terr;

	// Use this for initialization
	void Start () {
    }
	
    public void SetSinTerrain()
    {
        string heightString = DummyData.GenerateSinString(0, 50, 0.05f);
        //Debug.Log(heightString);
        terr = Terrain.activeTerrain;
        terr.GetComponent<TerrainUpdater>().targetHeightMap = 
            StringParser.parse(heightString);
    }
}
