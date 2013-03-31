using UnityEngine;
using System.Collections;

public class NetManager : MonoBehaviour 
{
	public DoomClient client;
	public DoomServer server;
	
	GameObject myServer = null;
	GameObject myClient = null;
		
	// Use this for initialization
	void Start () 
	{
		// Avoid Nast background upsets
		Application.runInBackground = true;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if((Input.GetKeyUp(KeyCode.O)) && (myServer == null))
		{
			myServer = Instantiate(server.gameObject) as GameObject;
			myServer.transform.parent = transform;
		}
		
		if(Input.GetKeyUp(KeyCode.P))
		{
			if(myClient == null)
			{
				myClient = Instantiate(client.gameObject) as GameObject;
				myClient.transform.parent = transform;
			}
			else
			{
				Destroy(myClient);
			}
		}
	}
}
