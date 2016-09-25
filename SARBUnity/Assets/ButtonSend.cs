using UnityEngine;
using System.Collections;

public class ButtonSend : MonoBehaviour {

	public void sendStuff()
	{
		NetworkClient.instance.sendStuff ();
	}
}
