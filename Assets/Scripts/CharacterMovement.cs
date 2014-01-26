using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    
	DialougeManager dmgr;
	GameObject[] lights;

    KeyCode frwd = KeyCode.W;
    KeyCode bkwd = KeyCode.S;
    KeyCode rgt = KeyCode.D;
    KeyCode lft = KeyCode.A;
    public float speed = 3f;
	Quaternion currAngle;
	// Use this for initialization
	void Start () {
		dmgr = (DialougeManager)(GameObject.FindGameObjectWithTag ("Dialouge").GetComponent<DialougeManager>());

	}
	
	// Update is called once per frame
	void Awake()
	{
		Debug.Log ("Setting up lights...");
		lights=GameObject.FindGameObjectsWithTag("light");
	}
    void Update ()
    {

		Vector3 movDir = new Vector3 (0, 0, 0);
		bool canMove = false;
		if (lights != null) {
				foreach(GameObject light in lights)
				{
						
						if (Vector3.Distance (((Transform)light.GetComponent<Transform> ()).position, transform.position) <
								((Light)light.GetComponent<Light> ()).range)
								canMove = true;
				}
		}
		if (dmgr.currentlyInDialouge ())
						canMove = false;
		if(canMove)
		{
							if (Input.GetKey (frwd)) {
								movDir.y += 1;

						}
						if (Input.GetKey (bkwd)) {
								movDir.y -= 1;

						}
						if (Input.GetKey (rgt)) {
								movDir.x += 1;

						}
						if (Input.GetKey (lft)) {
								movDir.x -= 1;
						}
		}
	
		movDir.Normalize();

		if (!movDir.Equals (Vector3.zero)) {
						float goalAngle = Mathf.Atan2 (movDir.y, movDir.x) * 180 / Mathf.PI;
			Quaternion goal = Quaternion.Euler(0,0,goalAngle);
			currAngle = Quaternion.Slerp(currAngle,goal,0.15f);
		}
			
		transform.rotation = currAngle;
		rigidbody.AddForce (movDir * speed);
    }




}
