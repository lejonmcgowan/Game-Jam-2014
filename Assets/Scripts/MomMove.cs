using UnityEngine;
using System.Collections;

public class MomMove : MonoBehaviour {
	public float moveSpeed = 0.01f;
	private Vector3 moveVector;
	//public Transform flashLight;
	// Use this for initialization
	void Start () {
		
	}

	void Awake()
	{

	}
	// Update is called once per frame
	void Update () {
		RaycastHit mouseInfo;
		Ray mouseRay = (Camera.main.ScreenPointToRay (Input.mousePosition));
		if (Physics.Raycast (mouseRay,out mouseInfo)) {
			moveVector = new Vector3(mouseInfo.point.x,mouseInfo.point.y,0);
			moveVector-=transform.position;
			moveVector.Normalize ();

		}
		if(Input.GetMouseButton(0))
		{
				
			if(moveVector.magnitude > 0.25f)
			{
				moveVector *= moveSpeed;
				Debug.DrawRay(transform.position,new Vector3(moveVector.x,moveVector.y,0));
				transform.Translate(new Vector3(moveVector.x,moveVector.y,0));
			}
		}
		//flashLight.LookAt (new Vector3(mouseVector.x,mouseVector.y,0));

		

	}
}
