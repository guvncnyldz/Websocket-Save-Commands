using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class Client : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    void OnEnable()
    {
        try
        {
            client = new TcpClient("192.168.1.61", 60852);
            stream = client.GetStream();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void SendData(JObject data)
    {
        string textData = JsonConvert.SerializeObject(data);
        byte[] buffer = Encoding.ASCII.GetBytes(textData);
        stream.Write(buffer, 0, buffer.Length);
    }
}