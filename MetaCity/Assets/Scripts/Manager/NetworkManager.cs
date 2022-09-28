using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using Gogs;
using Model;

public class NetworkManager : MonoBehaviour
{

    private static NetworkManager instance = null;
    private WebSocket websocket;

    private UpdateUserInWorld userInfo = new UpdateUserInWorld();

    public static NetworkManager Instance()
    {
        if (instance == null)
        {
            GameObject mgr = new GameObject("NetworkMgr");
            DontDestroyOnLoad(mgr);
            instance = mgr.AddComponent<NetworkManager>();
            GInit.Init();
        }
        return instance;
    }

    public async void Init()
    {
        Debug.Log("network manager init");


        userInfo.Postion = new Vecotr3();
        userInfo.Rotation = new Rotation();

        websocket = new WebSocket("wss://service-33nxwfbd-1300434835.sh.apigw.tencentcs.com/release/base");
        websocket.OnOpen += () =>
        {
            Debug.Log("connection success");
            JoinWorld joinWorld = new JoinWorld();
            NetworkManager.Instance().SendByte(joinWorld.ToPacketData());
        };
        websocket.OnMessage += (bytes) =>
        {
            GMessages.Message("base_connection", bytes);
        };
        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!" + e);
        };

        GEvents.OnPing((name, obj) =>
        {
            SendByte(GMessages.GetPong());
        });

        await websocket.Connect();
    }

    public void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (websocket != null)
        {
            websocket.DispatchMessageQueue();
        }
#endif
    }

    public async void SendByte(byte[] data)
    {
        if (websocket != null)
        {
            if (websocket.State == WebSocketState.Open)
            {
                await websocket.Send(data);
            }
        }
    }

    public void SendInfo(UnityEngine.Vector3 position, Quaternion rotation)
    {
        userInfo.Postion.X = position.x;
        userInfo.Postion.Y = position.y;
        userInfo.Postion.Z = position.z;

        userInfo.Rotation.X = rotation.x;
        userInfo.Rotation.Y = rotation.y;
        userInfo.Rotation.Z = rotation.z;
        userInfo.Rotation.W = rotation.w;

        SendByte(userInfo.ToPacketData());

    }

}