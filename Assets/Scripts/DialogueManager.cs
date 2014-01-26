using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class DialogueManager : MonoBehaviour {

    /* Class assumes text input is in following format 
     * "DIALOGUE LINE."
     * "DIALOGUE LINE 2 ."
     * */
    
    public TextAsset dialogueMom;    
    public TextAsset dialogueDaughter;
    public bool test = false;
    string[] dialogueMomArray;
    string[] dialogueDaughterArray;
	// Use this for initialization

    void Start()
    {
        initializeDialogue();
    }


    void Update()
    {
        if (test == true)
        {
            displayDialogue(0, "mom");
        }
        else if (test == false)
        {
            displayDialogue(0, "daughter");
        }
    }


    public void initializeDialogue()
    {
        dialogueMomArray = dialogueMom.text.Split('.');
        dialogueDaughterArray = dialogueDaughter.text.Split('.');
    }


    public void displayDialogue(int dialogueLineNumber, string character)
    {
       TextMesh display = (TextMesh)GetComponent("TextMesh");
        
        if (character == "mom")
        {
            display.text = dialogueMomArray[dialogueLineNumber];
        }
        else if (character == "daughter")
        {
            display.text = dialogueDaughterArray[dialogueLineNumber];
        }
        
    }



}
