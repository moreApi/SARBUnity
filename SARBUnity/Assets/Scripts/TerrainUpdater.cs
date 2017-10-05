using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainUpdater : MonoBehaviour {
    Terrain terr; // terrain to modify 
    int hmWidth; // heightmap width 
    int hmHeight; // heightmap height

    public float[,] targetHeightMap;

    void Start()
    {
        terr = Terrain.activeTerrain;
        hmWidth = terr.terrainData.heightmapWidth;
        hmHeight = terr.terrainData.heightmapHeight;
    }


    void Update()
    {
        if (targetHeightMap == null)
            return;

        /*
        TODO: smooth change between heights
        // get the heights of the terrain under this game object
        float[,] heights = terr.terrainData.GetHeights(0,0,hmWidth,hmWidth);
        */

        // set the new height
        terr.terrainData.SetHeights(0,0,targetHeightMap);

    }

}
