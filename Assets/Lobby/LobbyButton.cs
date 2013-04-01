using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LobbyButton : MonoBehaviour 
{
	public DoomServer serverPrefab;
	bool flipping = false;
	TextMesh tMesh;
	
	// Use this for initialization
	void Start () 
	{
		tMesh = GetComponentInChildren<TextMesh>();	
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void OnMouseEnter() 
	{
		transform.localScale = Vector3.one * 1.2f;
    }
	
	void OnMouseExit()
	{
		transform.localScale = Vector3.one;
	}
	
	void OnMouseUpAsButton() 
	{
		if(flipping == false)
		{
			StartCoroutine(FlipMe());
			Instantiate(serverPrefab.gameObject);
		}
		else
		{
			GameObject.FindGameObjectWithTag("GameController").SendMessage("StartGame");
		}
    }
	
	IEnumerator FlipMe()
	{
		flipping = true;
		float r = 0.0f;
		while(r <= 360.0f)
		{
			if(((r+36.0f) > 180.0f) != (r > 180.0f))
				tMesh.text = "Start Game";
				
			transform.localRotation = Quaternion.AngleAxis(r, Vector3.right);
			r += 36.0f;
			yield return new WaitForSeconds(0.1f);
		}
		
		// flipping = false;
	}
}
