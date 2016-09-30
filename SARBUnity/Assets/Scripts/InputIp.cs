using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputIp : MonoBehaviour {

	public void onEdit()
	{
		NetworkClient.instance.UpdateHostName(gameObject.transform.FindChild ("Text").GetComponent<Text>().text);
	}
}
