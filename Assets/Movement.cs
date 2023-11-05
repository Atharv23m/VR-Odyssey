using System;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class Movement : MonoBehaviour
{
    public SocketIOUnity socket;
    public string serverUrlLink = "https://56ec-2409-40e3-1c-3096-1ed6-2139-34f3-6a16.ngrok-free.app/";
    public SpeechRecognizerDemo demo;

    public float acceleration = 50; // how fast you accelerate
    public float dampingCoefficient = 5;

    Vector3 velocity; // current velocity

    Vector3 moveInput = default;

    void AddMovement(Vector3 dir)
    {
        moveInput = dir;
    }

    void Start()
    {
        var uri = new Uri(serverUrlLink);
        socket = new SocketIOUnity(uri);

        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("socket.OnConnected");
        };

        socket.Connect();
    }
    void Update()
    {
        socket.On("keyPress", response =>
        {
            string key = response.GetValue<string>();

            // Check the received key and update the movement direction accordingly
            switch (key)
            {
                case "w":
                    AddMovement(Vector3.forward);
                    break;
                case "s":
                    AddMovement(Vector3.back);
                    break;
                case "a":
                    AddMovement(Vector3.left);
                    break;
                case "d":
                    AddMovement(Vector3.right);
                    break;
                case "q":
                    AddMovement(Vector3.up);
                    break;
                case "e":
                    demo.StartListening();
                    AddMovement(Vector3.down);
                    break;
                case "x":
                    demo.StartListening();
                    break;
                case "z":
                    break;
                default:
                    break;
            }
        }
        );
        Vector3 direction = transform.TransformVector(moveInput.normalized);
        Vector3 temp = direction * acceleration; // "walking"
        // Physics
        velocity += temp * Time.deltaTime;
        velocity = Vector3.Lerp(velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;
    }

    void OnDestroy()
    {
        socket.Dispose();
    }
}
