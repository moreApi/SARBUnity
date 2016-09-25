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
	public string hostname = "";
	public int port = 0;

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

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
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

	public void initTcpClient(string host, int port)
	{
		try
		{
			clientSocket = new TcpClient (host, port);
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
	}
}
