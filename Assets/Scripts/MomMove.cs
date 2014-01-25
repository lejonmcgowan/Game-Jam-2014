using UnityEngine;
using System.Collections;

public class MomMove : MonoBehaviour {
	public float moveSpeed = 0.01f;
	public Transform flashLight;
	// Use this for initialization
	void Start () {
		
	}

	void Awake()
	{

	}
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
		{
			Vector3 mouseVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
			Vector2 moveVector = new Vector2(mouseVector.x - transform.position.x,
			                                 mouseVector.y-transform.position.y);
			if(moveVector.sqrMagnitude > 1)
			{
				Debug.DrawRay(transform.position,new Vector3(moveVector.x,moveVector.y,0));
				moveVector.Normalize();
				moveVector*= moveSpeed;
				transform.Translate(new Vector3(moveVector.x,moveVector.y,0));
			}

		}
	}
}
