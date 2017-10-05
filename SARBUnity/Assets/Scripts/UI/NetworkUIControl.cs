using UnityEngine;
using System.Collections;

public class NetworkUIControl : MonoBehaviour {

    public void OpenConnection()
    {
        NetworkClient.instance.initTcpClient();
    }

    public void CloseConnection()
    {
        NetworkClient.instance.closeSocket();
    }

    public void RequestHeightMap()
    {
        NetworkClient.instance.writeSocket("", 2);
    }
}
