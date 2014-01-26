using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

    TextAsset dialogueString;
    public Texture dialogueBox;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnGUI()
    {
        Rect UI = new Rect(0,3*Screen.height/4,Screen.width, Screen.height/4);
        /* UI.center = new Vector2(Screen.width/2, Screen.height/2); */
        GUI.Box(UI, dialogueBox);
    }
    public void displayDialogue(TextAsset dialogueString)
    {
              
    }


}
