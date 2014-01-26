using UnityEngine;
using System.Collections;

public class CamFollowMouseScript : MonoBehaviour {
	public Transform focusPoint; //Point that can't leave the camera
	public float minFocusRadius;
	public float minMouseDist = 0.5f;
	public float cameraRadius = 300;
	Vector3 attemptedPos;
	float height = -10;
	Vector3 truePos;
	Vector3 targPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit mouseInfo;
		Ray mouseRay = (Camera.main.ScreenPointToRay (Input.mousePosition));
		if (Physics.Raycast (mouseRay,out mouseInfo)) {
			attemptedPos = new Vector3(mouseInfo.point.x,mouseInfo.point.y,0);
			
		}
				
			targPos = attemptedPos - focusPoint.position;				
			if (targPos.magnitude > cameraRadius + minFocusRadius)
			{
				targPos.Normalize ();
				targPos*=cameraRadius+minFocusRadius;
			}
			/*
			Debug.Log ("Focus point is:" + focusPoint.position.ToString ());
			Debug.Log ("Target point is:" + targPos.ToString ());
			Debug.Log ("Attempted pos is:" + attemptedPos.ToString ());
			*/			
	
		targPos.z = height;
		targPos += focusPoint.position;
		truePos += (targPos - truePos) * 0.05f;
		transform.position = truePos;

	}
}
