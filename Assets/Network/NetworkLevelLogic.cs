using UnityEngine;
using System.Collections;

public class NetworkLevelLogic : MonoBehaviour 
{
	public GameObject puppetPrefab;
	
	GameObject myPlayer;
	GameObject myPuppet;
	
	bool requestedPuppets = false;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(myPlayer && myPuppet)
		{
			myPuppet.transform.position = myPlayer.transform.position;
			myPuppet.transform.rotation = myPlayer.transform.rotation;
			
			if(requestedPuppets == false)
			{
				networkView.RPC("RequestPuppets", RPCMode.Server, Network.player);
				requestedPuppets = true;
			}
		}	
	}
	
	void OnLevelWasLoaded(int level)
	{
		DoomLog.Log("Level Loaded");
		
		requestedPuppets = Network.isServer;
		
		myPlayer = GameObject.FindGameObjectWithTag("Player");
		myPuppet = Network.Instantiate(puppetPrefab, myPlayer.transform.position, myPlayer.transform.rotation, 1) as GameObject;
	}
	
	[RPC]
	void RequestPuppets(NetworkPlayer player)
	{
		DoomLog.Log("Sending new player Puppets");
		GameObject[] puppetList = GameObject.FindGameObjectsWithTag("Puppet");
		foreach(GameObject pup in puppetList)
			if(player != pup.networkView.owner)
				networkView.RPC("CreateOtherPlayerPuppets", player, pup.networkView.viewID);
	}
	
	[RPC]
	void CreateOtherPlayerPuppets(NetworkViewID viewID)
	{
		DoomLog.Log("Creating Puppet: " + viewID);
		GameObject otherPlayer = Instantiate(puppetPrefab) as GameObject;
		otherPlayer.networkView.viewID = viewID;
	}
}
