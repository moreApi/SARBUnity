using UnityEngine;
using System.Collections;

public class requestHeightMap : MonoBehaviour {


    public void sendStuff()
    {
        // Create some diversity in sending
        int rnd = Random.Range(1, 5);
        NetworkClient.instance.writeSocket("",2);
    }
}
