using UnityEngine;
using System.Collections;

public class DoomLog : MonoBehaviour 
{
	static DoomLog lastDebugLog = null;
	GUIText myText;
	float fadeTimer;
	
	public float timePerLine = 1.2f;
	
	// Use this for initialization
	void Start () 
	{
		fadeTimer = 0.0f;
		myText = GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(fadeTimer > 0)
		{ 
			int oldLineCount = Mathf.FloorToInt(fadeTimer / timePerLine);
			int newLineCount = Mathf.FloorToInt((fadeTimer - Time.deltaTime)/timePerLine);
			
			if(oldLineCount != newLineCount)
			{
				string[] lines = myText.text.Split("\n"[0]);
				myTexwt.text = "";
				
				newLineCount = Mathf.Min(lines.Length, newLineCount);
				
				for (int c = 0; c < newLineCount; c++) 
				{
					myText.text += lines[c] + "\n";
				}				
			}
				
			fadeTimer -= Time.deltaTime;
		}
	}
	
	void OnEnable() 
	{
		lastDebugLog = this;
	}
	
	void OnDisable()
	{
		if(lastDebugLog == this)
			lastDebugLog = null;
	}
	
	void InternalLog(string msg)
	{
		Debug.Log("Doom: " + msg);
		myText.text += "\n" + msg;
		fadeTimer += timePerLine;
	}
	
	static public void Log(string msg)
	{
		if(lastDebugLog)
			lastDebugLog.InternalLog(msg);
	}
}
