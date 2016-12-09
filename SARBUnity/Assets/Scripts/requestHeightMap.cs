using UnityEngine;
using System.Collections;

public class requestHeightMap : MonoBehaviour {

	MeshCreator meshCreator;
	void Start()
	{
		meshCreator = GameObject.Find ("PlaneGrid").GetComponent<MeshCreator> ();
	}

	public void updateHeightData()
	{
		meshCreator.updateHeightData ();
	}

    public void sendStuff()
    {
        // Create some diversity in sending
        NetworkClient.instance.writeSocket("",2);
    }
}
