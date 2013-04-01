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
				myText.text = "";
				
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
		myText = GetComponent<GUIText>();
	}
	
	void OnDisable()
	{
		if(lastDebugLog == this)
			lastDebugLog = null;
	}
	
	void InternalLog(string msg)
	{
		Debug.Log("Doom: " + msg);
		string[] newLines = msg.Split("\n"[0]);
		string[] lines = myText.text.Split("\n"[0]);
		
		myText.text = "";
		for (int i = newLines.Length - 1; i >= 0; i--)
			myText.text += newLines[i] + "\n";		
		foreach(string l in lines)
			myText.text += l + "\n";
		
		fadeTimer += (newLines.Length+1) * timePerLine;
	}
	
	static public void Log(string msg)
	{
		if(lastDebugLog)
			lastDebugLog.InternalLog(msg);
	}
}
