using UnityEngine;
using System.Collections;

public class DoomClient : MonoBehaviour 
{		
	public string hostStr = "127.0.0.1";
	public int hostPort = 7777;
	
	// Use this for initialization
	void Start () 
	{
		Application.runInBackground = true;	
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void OnEnable() 
	{
		DoomLog.Log("Attempting to connect to server....");
		Network.Connect(hostStr, hostPort, DoomServer.srvPassword);
	}
	
	void OnDisable()
	{
		Network.Disconnect();
	}
	
	void OnConnectedToServer() 
	{
        DoomLog.Log("Client: Connected to server");
		Application.LoadLevel("TestSection");
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) 
	{
        if (Network.isServer)
            DoomLog.Log("Client: Local server connection disconnected");
        else
            if (info == NetworkDisconnection.LostConnection)
                DoomLog.Log("Client: Lost connection to the server");
            else
                DoomLog.Log("Client: Successfully diconnected from the server");
		
		Application.LoadLevel("Lobby");
    }
	
	void OnFailedToConnect(NetworkConnectionError error)
	{
		DoomLog.Log("Client: Could not connect to server: " + error);
	}
	
	void OnNetworkInstantiate(NetworkMessageInfo info) 
	{
        DoomLog.Log("Client: New object instantiated by " + info.sender);
    }
}
