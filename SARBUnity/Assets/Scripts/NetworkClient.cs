using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
public class NetworkClient : MonoBehaviour {
	public static NetworkClient instance = null;
	TcpClient clientSocket;
	NetworkStream networkStream;
	StreamWriter streamWriter;
	StreamReader streamReader;

	bool receiving = true;
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
		if (networkStream != null)
		{
			if (networkStream.DataAvailable)
			{
				if (receiving == false)
				{
					receiving = true;
					tempString = streamReader.ReadLine ();

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
