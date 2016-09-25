using UnityEngine;
using System.Collections;

public class ButtonConnect : MonoBehaviour {

	public void onClick()
	{
		NetworkClient.instance.initTcpClient ();
		NetworkClient.instance.sendStuff ();
	}
}
