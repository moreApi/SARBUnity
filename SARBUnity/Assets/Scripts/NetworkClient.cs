using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
public class NetworkClient : MonoBehaviour
{

    // String and Host name
    public String hostname;
    public Int32 port;

    internal Boolean socket_ready = false;
    internal String input_buffer = "";

    TcpClient tcp_socket;
    NetworkStream net_stream;
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



    public void updateHostName(string hostname)
    {
        this.hostname = hostname;
    }



    public void updatePort(Int32 port)
    {
        this.port = port;
    }



    public void initTcpClient()
    {
        try
        {
            Debug.Log(this.hostname + " " + this.port);
            this.tcp_socket = new TcpClient(this.hostname, this.port);
            this.net_stream = this.tcp_socket.GetStream();
            this.streamWriter = new StreamWriter(this.net_stream);
            this.streamReader = new StreamReader(this.net_stream);
            this.socket_ready = true;
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
        if (!this.socket_ready)
            return;

        // Consider a string builder for better performance here
        line = line + "\r\n";
        this.streamWriter.Write(line);
        this.streamWriter.Flush();
        
    }

    public string ReceiveSocket()
    {
        if (!this.socket_ready)
            return "";

        if (this.net_stream.DataAvailable)
            return this.streamReader.ReadLine();

        return "";
    }


    // Close the sockets and readers
    public void closeSocket()
    {
        if (!this.socket_ready)
            return;

        this.streamWriter.Close();
        this.streamReader.Close();
        this.tcp_socket.Close();
        this.socket_ready = false;

    }
}
