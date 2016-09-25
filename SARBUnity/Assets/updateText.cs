using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class updateText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		string tempStr = NetworkClient.instance.receiveStuff ();
		if (tempStr != "")
		{
			gameObject.transform.GetComponent<Text> ().text = tempStr;
		}
	}
}
