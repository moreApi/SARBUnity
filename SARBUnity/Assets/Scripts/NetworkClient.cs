using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
public class NetworkClient : MonoBehaviour
{

    // String and Host name
    public String hostname;                     // Ip of the host:      Local or Public ip depends.
    public Int32 port;                          // Port of the host 

    internal Boolean socketReady = false;
    internal String inputBuffer = "";

    TcpClient tcpSocket;
    NetworkStream netStream;

    public static NetworkClient instance = null;
    StreamWriter streamWriter;
    StreamReader streamReader;



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



    public void UpdateHostName(string hostname)
    {
        this.hostname = hostname;
    }



    public void UpdatePort(Int32 port)
    {
        this.port = port;
    }



    public void InitTcpClient()
    {
        try
        {
            Debug.Log(this.hostname + " " + this.port);
            this.tcpSocket = new TcpClient(this.hostname, this.port);
            this.netStream = this.tcpSocket.GetStream();
            this.streamWriter = new StreamWriter(this.netStream);
            this.streamReader = new StreamReader(this.netStream);
            this.socketReady = true;
            Debug.Log("Connected to: " + this.hostname + " Port: " + this.port);
        }

        catch (Exception e)
        {
            // Something went wrong
            Debug.Log("Socket error: " + e);
        }
    }

    public void WriteSocket(string line)
    {
        if (!this.socketReady)
            return;

        // Consider a string builder for better performance here
        line = line + "\r\n";
        this.streamWriter.Write(line);
        this.streamWriter.Flush();
        
    }

    public string ReceiveSocket()
    {
        if (!this.socketReady)
            return "";

        if (this.netStream.DataAvailable)
            return this.streamReader.ReadLine();

        return "";
    }


    // Close the sockets and readers
    
    public void closeSocket()
    {
        if (!this.socketReady)
            return;

        this.streamWriter.Close();
        this.streamReader.Close();
        this.tcpSocket.Close();
        this.socketReady = false;
    }
}
