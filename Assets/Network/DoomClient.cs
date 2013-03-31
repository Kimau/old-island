using UnityEngine;
using System.Collections;

public class DoomClient : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnEnable() 
	{
		Network.Connect("127.0.0.1", 7777, DoomServer.srvPassword);
	}
	
	void OnDisable()
	{
		Network.Disconnect();
	}
	
	void OnConnectedToServer() 
	{
        DoomLog.Log("Connected to server");
    }
	
	void OnDisconnectedFromServer(NetworkDisconnection info) 
	{
        if (Network.isServer)
            DoomLog.Log("Local server connection disconnected");
        else
            if (info == NetworkDisconnection.LostConnection)
                DoomLog.Log("Lost connection to the server");
            else
                DoomLog.Log("Successfully diconnected from the server");
    }
	
	void OnFailedToConnect(NetworkConnectionError error)
	{
		DoomLog.Log("Could not connect to server: " + error);
	}
	
	void OnNetworkInstantiate(NetworkMessageInfo info) 
	{
        DoomLog.Log("New object instantiated by " + info.sender);
    }
}
