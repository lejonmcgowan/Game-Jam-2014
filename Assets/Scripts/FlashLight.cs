using UnityEngine;
using System.Collections;

public class FlashLight : MonoBehaviour {
	public float coneRad;
	public float spreadAng;
	// Use this for initialization
	private float phi;
	private Mesh lightMesh;
	private Vector3[] verts = new Vector3[5];
	private Vector3 ray1, ray2, ray1Dot, ray2Dot;
	private int[] tri = new int[9];
	private Vector2[] uv = new Vector2[5];
	private Vector3[] norm = new Vector3[5];
	private Vector3 centerRay;
	public Light endLight;
	private bool ray1Hit;
	private bool ray2Hit;
	void Start () {
		//endLight = GameObject.FindGameObjectWithTag ("FlashEnd") as Light;
		lightMesh = GetComponent<MeshFilter> ().mesh;
	}
	
	// Update is called once per frame
	void Update () {
		lightMesh.Clear ();
		verts [0] = Vector3.zero;
		RaycastHit mouseInfo;
		Ray mouseRay = (Camera.main.ScreenPointToRay (Input.mousePosition));
		if (Physics.Raycast (mouseRay,out mouseInfo)) {
			centerRay = new Vector3(mouseInfo.point.x,mouseInfo.point.y,0);
			centerRay-=transform.position;
			centerRay.Normalize ();
			centerRay *= coneRad;
		}
		phi = Mathf.Atan2 (centerRay.y, centerRay.x);
		ray1 = new Vector3 (coneRad * Mathf.Cos (phi - spreadAng),
		                            coneRad * Mathf.Sin (phi - spreadAng), 0);
		ray2 = new Vector3 (coneRad * Mathf.Cos (phi + spreadAng), 
		                            coneRad * Mathf.Sin (phi + spreadAng), 0);
	


		RaycastHit info1;
		RaycastHit info2;
		ray1Hit = false;
		ray2Hit = false;
		if (Physics.Raycast (transform.position, ray1, out info1, coneRad)) {

			Vector3 crs =Vector3.Cross(info1.normal,Vector3.forward);
			Vector3 shrt = ray1;
			shrt.Normalize();
			shrt*=(ray1.magnitude-info1.distance);
			ray1.Normalize();
			ray1*=info1.distance;
			if(Vector3.Dot(crs,shrt) > 0)
			{
				ray1Dot = crs*(Vector3.Dot(crs,shrt));
				RaycastHit dotHit;
				if(Physics.Raycast (info1.point,ray1Dot,out dotHit,ray1Dot.magnitude))
				{
					ray1Dot.Normalize();
					ray1Dot*=dotHit.distance;
				}
				ray1Dot += ray1;
			
			}
			else
			{
				ray1Dot = ray1;
			}


			ray1Hit = true;
			Debug.Log("Ray one hit");
		}
		else
		{
			ray1Dot = ray1;
		}
		if (Physics.Raycast (transform.position, ray2, out info2, coneRad)) {


			Vector3 crs =Vector3.Cross(info2.normal,Vector3.forward);
			Vector3 shrt = ray2;
			shrt.Normalize();
			shrt*=(ray2.magnitude-info2.distance);
			ray2.Normalize();
			ray2*=info2.distance;
			if(Vector3.Dot(crs,shrt) < 0)
			{
				ray2Dot = crs*(Vector3.Dot(crs,shrt));
			
				RaycastHit dotHit;
				if(Physics.Raycast (info2.point,ray2Dot,out dotHit,ray2Dot.magnitude))
				{
					ray2Dot.Normalize();
					ray2Dot*=dotHit.distance;
				}
				ray2Dot+=ray2;
			
			}
			else
			{
				ray2Dot = ray2;
			}
			ray2Hit = true;
		
				//Debug.DrawRay(info2.point,shrt);
			Debug.Log("Ray two hit");
		}



		else
		{
			ray2Dot = ray2;
		}
	
		endLight.range = Vector3.Distance(ray1,ray2)*2;
		Debug.DrawLine(ray1Dot+transform.position,ray2Dot+transform.position);
		endLight.transform.position = (ray2Dot*.8f-ray1Dot*.8f)/2 + ray1Dot*.8f + transform.position;
	

		verts [1] = ray1;
		verts [2] = ray1Dot;
		verts [3] = ray2Dot;
		verts [4] = ray2;
		//Left triangle
		tri [0] = 2;
		tri [1] = 1;
		tri [2] = 0;
		//Center triangle
		tri [3] = 0;
		tri [4] = 3;
		tri [5] = 2;
		//Right Triangle
		tri [6] = 4;
		tri [7] = 3;
		tri [8] = 0;
		norm [0] = Vector3.back;
		norm [1] = Vector3.back;
		norm [2] = Vector3.back;
		norm [3] = Vector3.back;
		lightMesh.vertices = verts;
		lightMesh.triangles = tri;

		uv [0] = new Vector2 (0, 0.5f);
		uv [1] = new Vector2 (Mathf.Abs(ray1.x)/coneRad,Mathf.Abs(ray1.y)/coneRad);
		uv [2] = new Vector2 (Mathf.Abs(ray1Dot.x)/coneRad,Mathf.Abs(ray1Dot.y)/coneRad);
		uv [3] = new Vector2 (Mathf.Abs(ray2Dot.x)/coneRad,Mathf.Abs(ray2Dot.y)/coneRad);
		uv [4] = new Vector2 (Mathf.Abs(ray2.x)/coneRad,Mathf.Abs(ray2.y)/coneRad);
		lightMesh.normals = norm;
		lightMesh.uv = uv;


		/*
		Debug.DrawLine (verts [0]+transform.position, verts [1]+transform.position);
		Debug.DrawLine (verts [1]+transform.position, verts [2]+transform.position);
		Debug.DrawLine (verts [2]+transform.position, verts [3]+transform.position);
		Debug.DrawLine (verts [3]+transform.position, verts [4]+transform.position);
		Debug.DrawLine (verts [4]+transform.position, verts [0]+transform.position);
		*/
	}
}
