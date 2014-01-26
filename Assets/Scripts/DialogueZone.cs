using UnityEngine;
using System.Collections;

public class DialogueZone : MonoBehaviour {

    public bool test = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        TextAsset text;
        StartMenu sm;
        test = true;
        sm = other.GetComponent<StartMenu>();
        /* sm.displayDialogue(text); */
    }
}
