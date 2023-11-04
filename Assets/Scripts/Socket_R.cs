using System;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class SocketIOClientExample : MonoBehaviour
{
    public SocketIOUnity socket;
    public string serverUrlLink = "https://56ec-2409-40e3-1c-3096-1ed6-2139-34f3-6a16.ngrok-free.app/";
    void Start()
    {
        var uri = new Uri(serverUrlLink);
        socket = new SocketIOUnity(uri);


        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("socket.OnConnected");
        };


        socket.On("keyPress", response =>
        {
            Debug.Log(response.ToString());
            Debug.Log(response.GetValue<string>());
        });


        socket.Connect();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            socket.EmitAsync("message", "Hello, server!"); // replace with your message
        }
    }

    void OnDestroy()
    {
        socket.Dispose();
    }
}