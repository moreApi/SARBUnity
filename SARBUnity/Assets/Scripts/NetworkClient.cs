using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
    Thread thread;
    int heightMapStorageSize;
    List<Byte[]> storeHeightMap;





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

		storeHeightMap = new List<byte[]>();
    }



    public void updateHostName(string hostname)
    {
        this.hostname = hostname;
    }



    public void updatePort(Int32 port)
    {
        this.port = port;
    }

    public void updateHeightmapStorageSize(int size)
    {
        this.heightMapStorageSize = size;
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

            this.heightMapStorageSize = 1920;
            this.thread = new Thread(new ThreadStart(receiveSocket));
            this.thread.Start();
            while (!this.thread.IsAlive);
            Debug.Log("Connected to: " + this.hostname + " Port: " + this.port);

            
        }

        catch (Exception e)
        {
            // Something went wrong
            Debug.Log("Socket error: " + e);
        }
    }

    public void writeSocket(string line, int command)
    {
        if (!this.socketReady)
            return;

        // Consider a string builder for better performance here
        if (command == 1)
        {
            // Send the header
            ASCIIEncoding asen = new ASCIIEncoding();
            Byte[] Buffer = asen.GetBytes(sendHeader(line.Length, command));
           this.netStream.Write(Buffer, 0, Buffer.Length);

            // send the data
            Buffer = asen.GetBytes(line);
            this.netStream.Write(Buffer, 0, Buffer.Length);
        }
        if(command == 2)
        {
            // Send the header
            ASCIIEncoding asen = new ASCIIEncoding();
            Byte[] Buffer = asen.GetBytes("000000000|0002");
            netStream.Write(Buffer, 0, Buffer.Length);

            // send the data
            Buffer = asen.GetBytes(line);
            this.netStream.Write(Buffer, 0, Buffer.Length);
        }

    }


    public void receiveSocket()
    {

        string message = "";
        if (!this.socketReady)
            return;

        while (this.thread.IsAlive)
        {
            int packageSize = 0;
            int packageCommand = 0;

            // Read data
            if (this.netStream.CanRead)
            {
                while (readHeader(ref packageSize, ref packageCommand))
                {


                    // Read the Header
                    if (packageCommand == 1)
                    {
                        message = readEcho(packageSize);
                        Debug.Log(message);

                    }

                    // read the heightmap
                    if (packageCommand == 2)
                    {
                        Debug.Log("Receiving Height Map");
                        readHeightMap(packageSize);
                        Debug.Log("Received Height Map1");
                        //for (int i = 50*240; i < 150 * 240/*storeHeightMap.Count*/; i++)
                        //{
                        //    Debug.Log("" + (System.Text.Encoding.UTF8.GetString(storeHeightMap[i])));
                        //}
                    }
                    packageCommand = 0;
                    packageSize = 0;
                }
            }         
        }
    }

	public string getHeightData()
	{
		List<Byte[]> tempHeightMap = storeHeightMap;
		string tempStr = "";
		for (int i = 0; i < tempHeightMap.Count; i++)
		{
			//Debug.Log("" + (System.Text.Encoding.UTF8.GetString(storeHeightMap[i])));
			//for (int j = 0; j < tempHeightMap [i].Length && tempHeightMap[i][j] != null; j++)
			//{
			//	tempStr += (char)(tempHeightMap [i] [j]);
			//}
			tempStr += System.Text.Encoding.ASCII.GetString (tempHeightMap [i]);
		}
		//Debug.Log ("NS " + tempStr.Length);
		return tempStr;
	}

    private Byte[] readData(int size)
    {
        Byte[] buffer = new Byte[size];
        while (size>0)
        {
           int bytes = this.netStream.Read(buffer, 0, size);
           size -= bytes;
        }

        return buffer;
    }

    private bool readHeader(ref int packageSize,  ref int packageCommand)
    {

        // Read header
        byte[] header = new byte[14];
        int size = this.netStream.Read(header, 0, 14);

        if(size < 0)
        {
            Debug.Log("readHeader false");
            return false;
        }

        char[] array = new char[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = Convert.ToChar(header[i]);
        }


        packageSize = Convert.ToInt32(parseheader(array, 0, 9));
        packageCommand = Convert.ToInt32(parseheader(array, 10, 14));

        Debug.Log("PackageSize: " + packageSize + "      " + "PackageCommand: " + packageCommand);
        return true;

    }


    private string parseheader(char[] array, int start, int end)
    {
        string temp = "";

       
        for (int i = start; i < end; i++)
        {
            temp = temp + array[i].ToString();
        }

        return temp;
    }

    private List<Byte[]> readHeightMap(int packageSize)
    {
        int listIndex = 0;
		storeHeightMap.Clear ();
        if (packageSize > 0)
        {
            Byte[] storageBuffer = new Byte[heightMapStorageSize];

            // Reading heightmap
            while (packageSize > 0)
            {
                //Debug.Log("Packet size:" + packageSize);
                int readSize = 0;
                readSize = Math.Min(storageBuffer.Length, packageSize);

                storageBuffer = readData(readSize);
				storeHeightMap.Add(storageBuffer);
				packageSize -= readSize;
				Thread.Sleep (1);
            }
            Debug.Log("done Reading socket");
        }
        return null;
    }



    private string readEcho(int packageSize)
    {
        string echo = "";
        if (packageSize > 0)
        {
            int storageSize = 2048;
            Byte[] storageBuffer = new Byte[storageSize];

          while(packageSize > 0)
          { 
                int readSize = 0;
                readSize = Math.Min(storageBuffer.Length, packageSize);

                storageBuffer = readData(readSize);

                for (int i = 0; i < storageBuffer.Length; i++)
                {
                    echo = echo + "" + (Convert.ToChar(storageBuffer[i]).ToString());
                }
                packageSize -= readSize;


            }
        }

        return echo;
    }

    private string sendHeader(int size, int command)
    {
        string header = "000000000|0000";
        char[] tempArray = header.ToCharArray();
        string sizeOfPackage = size.ToString();
        string commandInString = command.ToString();


        int offset = commandInString.Length - 1;
        for (int i = header.Length - 1; i > ((header.Length - 1) - commandInString.Length); i--)
        {
            tempArray[i] = commandInString.ToCharArray()[offset];
            offset--;
        }

        offset = sizeOfPackage.Length - 1;
        for (int i = header.Length - 6; i > ((header.Length - 6) - sizeOfPackage.Length); i--)
        {
            tempArray[i] = sizeOfPackage.ToCharArray()[offset];
            offset--;
        }


        header = new string(tempArray);

        return header;
    }


    // Close the sockets and readers

    public void closeSocket()
    {
        if (!this.socketReady)
            return;
        this.thread.Join();
        this.streamWriter.Close();
        this.streamReader.Close();
        this.tcpSocket.Close();
        this.socketReady = false;
       
    }
}
