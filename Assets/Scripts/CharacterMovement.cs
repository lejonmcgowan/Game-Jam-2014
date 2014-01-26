using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    
    

    KeyCode frwd = KeyCode.W;
    KeyCode bkwd = KeyCode.S;
    KeyCode rgt = KeyCode.D;
    KeyCode lft = KeyCode.A;
    float speed = 3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

     void Update ()
    {
        Vector3 movDir = new Vector3(0, 0, 0);
        if (Input.GetKey(frwd))
        {
            movDir.y += 1;

        }
        if (Input.GetKey(bkwd))
        {
            movDir.y -= 1;

        }
        if (Input.GetKey(rgt))
        {
            movDir.x += 1;

        }
        if (Input.GetKey(lft))
        {
            movDir.x -= 1;
        }

        transform.Translate((movDir.normalized) * speed * Time.deltaTime, Space.World);
    }




}
