using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class updateText : MonoBehaviour {

	//HACKY SOLUTION
	bool runningCoroutine = false;
	string checkStr = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!runningCoroutine)
		{
			//May need its own thread
			StartCoroutine (getTextFromNetwork ());
			runningCoroutine = true;
		}
		if (runningCoroutine && checkStr != "")
		{
			runningCoroutine = false;
			checkStr = "";
		}
	}

	IEnumerator getTextFromNetwork()
	{
		string tempStr = NetworkClient.instance.receiveStuff ();
		yield return null;
		if (tempStr != "")
		{
			gameObject.transform.GetComponent<Text> ().text = tempStr;
			checkStr = tempStr;
		}
		yield return null;
	}
}
