using UnityEngine;
using System.Collections;

public class DialougeTrigger : MonoBehaviour {
	public bool autoStart;
	public bool onlyOnce;
	bool used;
	public string[] arr;
	DialougeManager dmgr;

	// Use this for initialization
	void Start () {
		dmgr = (DialougeManager)(
			GameObject.FindGameObjectWithTag ("Dialouge").GetComponent<DialougeManager> ());

	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "daughter") {
						if (autoStart)
			{
								dmgr.setDialouge (arr);
						if (onlyOnce)
								Destroy (this);
			}
				}
	}
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "daughter") {
			Debug.Log("Ready to trigger...");
						if (!used && !autoStart && Input.GetKeyDown(KeyCode.E)) {
								dmgr.setDialouge (arr);		
				used = true;		
						if (onlyOnce) {
								Destroy (this);
						}
			}
				}
	}
	void OnTriggerExit(Collider other)
	{

						used = false;
				
	}
}
