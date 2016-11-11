using UnityEngine;
using System.Collections;

public class requestHeightMap : MonoBehaviour {


    public void sendStuff()
    {
        // Create some diversity in sending
        NetworkClient.instance.writeSocket("",2);
    }
}
