using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

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

    public void writeSocket(string line)
    {
        if (!this.socketReady)
            return;

        // Consider a string builder for better performance here
        string datasize = "0000000";
        char[] tempArray = datasize.ToCharArray();
        string sizeOfMessage = line.Length.ToString();
        int offset = sizeOfMessage.Length-1;
        for(int i = datasize.Length-1; i > ((datasize.Length-1) - sizeOfMessage.Length); i--)
        {
            tempArray[i] = sizeOfMessage.ToCharArray()[offset];
            offset--;
        }

       
        datasize = new string(tempArray);

        // Send the header
        ASCIIEncoding asen = new ASCIIEncoding();
        Byte[] Buffer = asen.GetBytes(datasize);
        netStream.Write(Buffer, 0, Buffer.Length);

        // send the data
        Buffer = asen.GetBytes(line);
        netStream.Write(Buffer, 0, Buffer.Length);


    }

public string receiveSocket()
{
        string message = "";
        if (!this.socketReady)
            return message;

        // Read data
        if (netStream.DataAvailable)
        {
            // Read header
            byte[] header = new byte[7];
            int size = netStream.Read(header, 0, 7);
            char[] array = new char[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = Convert.ToChar(header[i]);
            }

            Debug.Log(size);
            string tempSize = "";
            
            
            // get the size:
            for (int i = 0; i < size; i++)
            {
                 if(array[i] != '0')
                {
                    tempSize = tempSize + array[i].ToString();
                }
            }
           

            // read the package
            int packageSize = Convert.ToInt32(tempSize);
            Byte[] dataBuffer = new Byte[packageSize];

            if(packageSize > 0)
            {
                if(!readData(dataBuffer))
                {
                    return message;
                }
            }

            for (int i = 0; i < dataBuffer.Length; i++)
            {
                message = message + "" + (Convert.ToChar(dataBuffer[i]).ToString());
            }
            return message;
        }

        return message;
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

    private bool readData(Byte[] buffer)
    {
        int packageSize= buffer.Length;

        netStream.Read(buffer, 0, buffer.Length);
        return true;
    }


}
