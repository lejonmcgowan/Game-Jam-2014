using UnityEngine;
using System.Collections;

public class DialougeManager : MonoBehaviour {
	public GUIText text;
	string[] dialouge;
	bool isInDialouge;
	bool updateDelay;
	int currDialouge;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (updateDelay) {
			text.enabled=true;
			if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
			{
				if(++currDialouge < dialouge.Length)
					text.text = dialouge[currDialouge];
				else
				{
					text.enabled = false;
					isInDialouge = false;
				}
			}	
		}
		updateDelay = isInDialouge;
	}

	public bool currentlyInDialouge()
	{
		return isInDialouge;
	}

	public void setDialouge(string[] arr)
	{
		dialouge = arr;
		currDialouge = 0;
		isInDialouge = true;
		text.text = arr [0];
	}


}
