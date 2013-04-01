using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoomServer : MonoBehaviour 
{
	public static string srvPassword = "DoomIsland";
	public int listenPort = 7777;
	public int players = 8;
	private int playerCount = 0;
	List<NetworkPlayer> playerList;
	
	// Use this for initialization
	void Start () 
	{
		Application.runInBackground = true;	
		DontDestroyOnLoad(gameObject);
		playerList = new List<NetworkPlayer>();
		Network.incomingPassword = srvPassword;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void StartGame()
	{
		Application.LoadLevel("TestSection");
	}
	
	void OnEnable() 
	{
		DoomLog.Log("Starting Server....");
		bool useNat = !Network.HavePublicAddress();
		Network.InitializeServer(players, listenPort, useNat);
	}
	
	void OnDisable()
	{
		Network.Disconnect();
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) 
	{
		Application.LoadLevel("Lobby");
	}
	
    void OnPlayerConnected(NetworkPlayer player) 
	{
		playerList.Add(player);
		
		DoomLog.Log("Server: Player " + playerCount++ + " connected from " + player.ipAddress + ":" + player.port);
    }
	
	void OnPlayerDisconnected(NetworkPlayer player) 
	{
		playerList.Remove(player);
		
        DoomLog.Log("Server: Clean up after player " + player);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }
	
	void OnServerInitialized() 
	{
        DoomLog.Log("Server: Server initialized and ready");
    }
	
	void OnNetworkInstantiate(NetworkMessageInfo info) 
	{
        DoomLog.Log("Server: New object instantiated by " + info.sender);
    }
}
