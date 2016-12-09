using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//This class is very hardcoded at the moment.

public class MeshCreator : MonoBehaviour {

	public int xSize;
	public int ySize;
	
	[HideInInspector]
	public bool running = true;
	
	List<GameObject> gO;

	private Mesh mesh;
	private Vector3[] vertices;
	int sizeOfMesh;


	void Awake ()
	{
		sizeOfMesh = (xSize + 1) * (ySize + 1);
		gO = new List<GameObject>();
		Vector3 pos = new Vector3 (0, 0, 0);
		for (int i = 0; i < 10; i++)
		{
			gO.Add(Generate ());
			gO [i].name = "Grid" + i;
			gO [i].transform.position = pos;
			gO [i].name = "Planegrid " + i;
			pos.y += 47;
		}
		this.transform.Rotate (90f, 0f, 0f);
	}

	void Start()
	{
		forceUpdate ();
		StartCoroutine(UpdateMesh ());
	}

	void Update()
	{
	}

	void forceUpdate()
	{
		List<string[]> myStrings = this.GetComponent<StringParser> ().myStrings;
		Vector3[] positions = gO [0].GetComponent<MeshFilter> ().mesh.vertices;
		Debug.Log(gO [0].GetComponent<MeshFilter> ().mesh.vertices.Length);
		int meshCounter = 0;
		int posCounter = 0;
		for (int j = 0; j < 640; j++)
		{
			for (int i = 0; i < 480; i++)
			{
				positions [posCounter].z = float.Parse (myStrings [j] [i]) / 10.0f;
				posCounter = (i + (j * 480)) % sizeOfMesh;

				if (posCounter == 0 && j != 0)
				{
					gO [meshCounter].GetComponent<MeshFilter> ().mesh.vertices = positions;
					gO [meshCounter].GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
					gO [meshCounter].GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
					gO [meshCounter].GetComponent<MeshCollider> ().sharedMesh = gO [meshCounter].GetComponent<MeshFilter> ().sharedMesh;
					meshCounter++;
					positions = gO [meshCounter].GetComponent<MeshFilter> ().mesh.vertices;
				}
			}
		}
		if (meshCounter == 9)
		{
			//Hacky solution to get the last position in the top mesh to not be 0.
			positions [sizeOfMesh - 1].z = positions [sizeOfMesh - 2].z;

			gO [meshCounter].GetComponent<MeshFilter> ().mesh.vertices = positions;
			gO [meshCounter].GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
			gO [meshCounter].GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
			gO [meshCounter].GetComponent<MeshCollider> ().sharedMesh = gO [meshCounter].GetComponent<MeshFilter> ().sharedMesh;
		}
	}

	IEnumerator UpdateMesh()
	{
		List<string[]> myStrings = this.GetComponent<StringParser> ().myStrings;
		Vector3[] positions = gO [0].GetComponent<MeshFilter> ().mesh.vertices;
		Debug.Log(gO [0].GetComponent<MeshFilter> ().mesh.vertices.Length);
		int meshCounter = 0;
		int posCounter = 0;
		while (running)
		{
			meshCounter = 0;
			posCounter = 0;
			positions = gO [0].GetComponent<MeshFilter> ().mesh.vertices;
			for (int j = 0; j < 640; j++)
			{
				for (int i = 0; i < 480; i++)
				{
					positions [posCounter].z = float.Parse (myStrings [j] [i]) / 10.0f;
					posCounter = (i + (j * 480)) % sizeOfMesh;

					if (posCounter == 0 && j != 0)
					{
						gO [meshCounter].GetComponent<MeshFilter> ().mesh.vertices = positions;
						gO [meshCounter].GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
						gO [meshCounter].GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
						meshCounter++;
						positions = gO [meshCounter].GetComponent<MeshFilter> ().mesh.vertices;
					}
				}
				//This happens because you don't set the mesh to be equal to the positions.
				yield return null;
			}
			if (meshCounter == 9)
			{
				//Hacky solution to get the last position in the top mesh to not be 0.
				positions [sizeOfMesh - 1].z = positions [sizeOfMesh - 2].z;

				gO [meshCounter].GetComponent<MeshFilter> ().mesh.vertices = positions;
				gO [meshCounter].GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
				gO [meshCounter].GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
			}
		}
	}


	//http://catlikecoding.com/unity/tutorials/procedural-grid/
	private GameObject Generate()
	{
		GameObject gameObj = new GameObject ();
		mesh = new Mesh ();
		gameObj.AddComponent<MeshFilter>().mesh = mesh;
		gameObj.AddComponent<MeshRenderer> ();
		gameObj.AddComponent<MeshCollider> ();
		mesh.name = "Grid";
		vertices = new Vector3[(xSize + 1) * (ySize + 1)];
		Vector2[] uv = new Vector2[vertices.Length];
		for (int i = 0, y = 0; y <= ySize; y++) {
			for (int x = 0; x <= xSize; x++, i++) {
				vertices[i] = new Vector3(x, y);
				uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		int[] triangles = new int[xSize * ySize * 6];
		for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
			for (int x = 0; x < xSize; x++, ti += 6, vi++)
			{
				triangles [ti] = vi;
				triangles [ti + 3] = triangles [ti + 2] = vi + 1;
				triangles [ti + 4] = triangles [ti + 1] = vi + xSize + 1;
				triangles [ti + 5] = vi + xSize + 2;
			}
		}
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
		gameObj.GetComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().material;
		gameObj.GetComponent<MeshCollider> ().sharedMesh = gameObj.GetComponent<MeshFilter> ().sharedMesh;
		gameObj.transform.SetParent (this.transform);
		return gameObj;
	}
}
