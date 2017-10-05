using UnityEngine;
using System.Collections;

public class ButtonManageConnection : MonoBehaviour {

    public void OpenConnection()
    {
        NetworkClient.instance.initTcpClient();
    }

    public void CloseConnection()
    {
        NetworkClient.instance.closeSocket();
    }
}
