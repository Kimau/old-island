using UnityEngine;
using System.Collections;

public class JoinServerButton : MonoBehaviour 
{
	public DoomClient clientNetPrefab;
	public GameObject[] killOthers;
	TextMesh tMesh;
	bool flipping = false;

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
		}
		else
		{
			Instantiate(clientNetPrefab.gameObject);
		}
    }
	
	IEnumerator FlipMe()
	{
		flipping = true;
		
		foreach(GameObject go in killOthers)
			Destroy(go);
		
		float r = 0.0f;
		while(r <= 360.0f)
		{
			if(((r+36.0f) > 180.0f) != (r > 180.0f))
				tMesh.text = clientNetPrefab.hostStr + ":" + clientNetPrefab.hostPort;
					
			transform.localRotation = Quaternion.AngleAxis(r, Vector3.right);
			r += 36.0f;
			yield return new WaitForSeconds(0.1f);
		}		
		
		// flipping = false;
	}
}
