using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
public class NetworkClient : MonoBehaviour {
	public static NetworkClient instance = null;
	TcpClient clientSocket;
	public NetworkStream networkStream;
	StreamWriter streamWriter;
	StreamReader streamReader;
	[HideInInspector]
	public bool networkRunning = false;
	bool receiving = false;
	string hostname = "";
	int port = 0;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	public void updateHostName(string hostname)
	{
		this.hostname = hostname;
	}

	public void updatePort(int port)
	{
		this.port = port;
	}

	public void initTcpClient()
	{
		try
		{
			clientSocket = new TcpClient (hostname, port);
			networkStream = clientSocket.GetStream();
			streamWriter = new StreamWriter(networkStream);
			streamReader = new StreamReader(networkStream);
			networkRunning = true;
			Debug.Log("Connected to: " + hostname + " Port: " + port);
		}
		catch(Exception e) 
		{
			Debug.Log ("Socket Error: " + e);
		}
	}

	public void initTcpClient(string hostname, int port)
	{
		try
		{
			clientSocket = new TcpClient (hostname, port);
			networkStream = clientSocket.GetStream();
			networkStream.ReadTimeout = 1;
			streamWriter = new StreamWriter(networkStream);
			streamReader = new StreamReader(networkStream);
		}
		catch(Exception e) 
		{
			Debug.Log ("Socket Error: " + e);
		}
	}

	public void sendStuff()
	{
		streamWriter.Write ("I am cool");
		streamWriter.Flush ();
	}

	public string receiveStuff()
	{
		string tempString = "";
		char[] tempChar = new char[256];
		int index = 0;
		int count = 256;
		if (networkStream != null)
		{
			Debug.Log ("0");
			if (networkStream.DataAvailable)
			{
				Debug.Log ("1");
				if (receiving == false)
				{
					Debug.Log ("receiving ... ");
					receiving = true;
					//tempString = streamReader.ReadToEnd ();
					try
					{
					tempString = streamReader.ReadLine ();
					}
					catch(Exception e)
					{
						Debug.Log ("Receive error: " + e);
					}
					//Debug.Log (tempString);
				}
			}
			if (receiving == true && !networkStream.DataAvailable)
			{
				receiving = false;
			}
		}
		return tempString;
	}
}
