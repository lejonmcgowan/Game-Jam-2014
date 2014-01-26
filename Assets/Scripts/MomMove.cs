using UnityEngine;
using System.Collections;

public class MomMove : MonoBehaviour {
	public float moveSpeed = 0.01f;
	public Transform model;
	private Vector3 moveVector;
	private DialougeManager dmgr;
	//public Transform flashLight;
	// Use this for initialization
	void Start () {
		dmgr = (DialougeManager)(GameObject.FindGameObjectWithTag ("Dialouge").GetComponent<DialougeManager>());
	}

	void Awake()
	{

	}
	// Update is called once per frame
	void Update () {
	if (!dmgr.currentlyInDialouge()) {
						RaycastHit mouseInfo;
						Ray mouseRay = (Camera.main.ScreenPointToRay (Input.mousePosition));
						if (Physics.Raycast (mouseRay, out mouseInfo, 25)) {
								moveVector = new Vector3 (mouseInfo.point.x, mouseInfo.point.y, 0);
								moveVector -= transform.position;
								moveVector.Normalize ();
								model.rotation = Quaternion.Euler (180, 0, -180 / Mathf.PI * Mathf.Atan2 (moveVector.y, moveVector.x));

								if (Input.GetMouseButton (0)) {
				
										if (moveVector.magnitude > 0.25f) {
												moveVector *= moveSpeed;
												Debug.DrawRay (transform.position, new Vector3 (moveVector.x, moveVector.y, 0));
					
												rigidbody.AddForce (moveVector);
										}
								}
						}

						//flashLight.LookAt (new Vector3(mouseVector.x,mouseVector.y,0));
				}
		

	}
}
