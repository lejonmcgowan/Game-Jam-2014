using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    
    
	GameObject[] lights;

    KeyCode frwd = KeyCode.W;
    KeyCode bkwd = KeyCode.S;
    KeyCode rgt = KeyCode.D;
    KeyCode lft = KeyCode.A;
    float speed = 3f;
	// Use this for initialization
	void Start () {
	
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

        transform.Translate((movDir.normalized) * speed * Time.deltaTime, Space.World);
    }




}
