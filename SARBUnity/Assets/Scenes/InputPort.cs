﻿using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class InputPort : MonoBehaviour {

	public void onEdit()
	{
		
		NetworkClient.instance.port = Convert.ToInt32 (gameObject.transform.FindChild ("Text").GetComponent<Text>().text);
	}
}
