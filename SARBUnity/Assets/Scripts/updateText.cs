using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class updateText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
       getTextFromNetwork();
    }

	void getTextFromNetwork()
	{
		string tempStr = NetworkClient.instance.ReceiveSocket();
       
        if (tempStr != "")
		{
            Debug.Log(tempStr);
            gameObject.transform.GetComponent<Text> ().text = tempStr;
		}
	}
}
