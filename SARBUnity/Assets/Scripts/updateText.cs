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
		if (NetworkClient.instance.networkRunning)
		{
			if (NetworkClient.instance.networkStream.DataAvailable)
			{
				if (!runningCoroutine)
				{
					Debug.Log ("Running update");
					//May need its own thread
					getTextFromNetwork ();
					runningCoroutine = true;
				}
				if (runningCoroutine && checkStr != "")
				{
					Debug.Log (" Running second update");
					runningCoroutine = false;
					checkStr = "";
				}
			}
		}
	}

	void getTextFromNetwork()
	{
		string tempStr = NetworkClient.instance.receiveStuff ();
		if (tempStr != "")
		{
			gameObject.transform.GetComponent<Text> ().text = tempStr;
			checkStr = tempStr;
		}
	}
}
