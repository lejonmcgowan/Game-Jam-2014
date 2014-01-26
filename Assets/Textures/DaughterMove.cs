using UnityEngine;
using System.Collections;

public class DaughterMove : MonoBehaviour {

	public Vector2 speed = new Vector2(25,25);
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		Vector3 move = new Vector3 (speed.x * inputX, speed.y * inputY, 0);

		move *= Time.deltaTime;

		transform.Translate (move);
	}
}
