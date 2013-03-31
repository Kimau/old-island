using UnityEngine;
using System.Collections;

public class DoomClient : MonoBehaviour 
{
	public GameObject puppetPrefab;
	GameObject playerController;
	GameObject currentPuppet;
		
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(currentPuppet)
		{
			currentPuppet.transform.position = playerController.transform.position;
			currentPuppet.transform.rotation = playerController.transform.rotation;
		}
	}
	
	void OnEnable() 
	{
		playerController = GameObject.FindWithTag("Player");
		DoomLog.Log("Attempting to connect to server....");
		Network.Connect("127.0.0.1", 7777, DoomServer.srvPassword);
	}
	
	void OnDisable()
	{
		Network.Disconnect();
		Destroy(currentPuppet);
		currentPuppet = null;
	}
	
	void OnConnectedToServer() 
	{
        DoomLog.Log("Client: Connected to server");
		
		currentPuppet = Network.Instantiate(puppetPrefab, playerController.transform.position, playerController.transform.rotation, 0) as GameObject;
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
