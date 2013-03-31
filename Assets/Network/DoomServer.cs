using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoomServer : MonoBehaviour 
{
	public static string srvPassword = "DoomIsland";
	private int playerCount = 0;
	List<NetworkPlayer> playerList;
	
	// Use this for initialization
	void Start () 
	{
		playerList = new List<NetworkPlayer>();
		Network.incomingPassword = srvPassword;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable() 
	{
		bool useNat = !Network.HavePublicAddress();
		Network.InitializeServer(32, 7777, useNat);
	}
	
	void OnDisable()
	{
		Network.Disconnect();
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
