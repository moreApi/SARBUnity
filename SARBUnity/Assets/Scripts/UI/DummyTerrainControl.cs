using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTerrainControl : MonoBehaviour {

    Terrain terr;
	
    public void SetSinTerrain()
    {
        string heightString = DummyData.GenerateSinString(0, 50, 0.05f);
        //Debug.Log(heightString);
        terr = Terrain.activeTerrain;
        terr.GetComponent<TerrainUpdater>().SetHeight(heightString);
    }
}
