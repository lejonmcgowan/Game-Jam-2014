using UnityEngine;
using System.Collections;

public class FlashLight : MonoBehaviour {
	public float coneRad;
	public float spreadAng;
	public int iter = 3; //# of triangles, must be 3 or >
	// Use this for initialization
	private float phi;
	private Mesh lightMesh;

	private Vector3[] verts;
	private Vector3 ray1, ray2, ray1Dot, ray2Dot;
	private int[] tri;
	private Vector2[] uv;
	private Vector3[] norm;
	private Vector3 centerRay;
	public Light endLight;
	private bool ray1Hit;
	private bool ray2Hit;
	void Start () {
		//endLight = GameObject.FindGameObjectWithTag ("FlashEnd") as Light;
		lightMesh = GetComponent<MeshFilter> ().mesh;
		verts = new Vector3[iter+1];
		tri = new int[3 * (iter-2)];
		uv = new Vector2[iter+1];
		norm = new Vector3[iter+1];
		
		uv [0] = new Vector2 (0.5f, 0.5f);
		int lastTri = 1;
		for(int i = 0; i < tri.Length; i+=3)
		{
			//Debug.Log("Building tris ["+i+"]["+(i+1)+"]["+(i+2)+"]");
			tri[i] = 0;
			tri[i+1] = lastTri;
			tri[i+2] = lastTri+1;
		
			//Debug.Log("Built tris "  + tri[i]  +" "+tri[i+1] + " " + tri[i+2]);
			lastTri++;
		}

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
			
		}
		phi = Mathf.Atan2(centerRay.y, centerRay.x);
		Debug.DrawRay (transform.position, centerRay);
		float dPhi = Mathf.Abs (spreadAng / iter);
		float currPhi = phi + spreadAng / 2;
		for (int i = 1; i < iter; i++) {
			Vector3 vert = new Vector3(coneRad*Mathf.Cos(currPhi),coneRad*Mathf.Sin(currPhi),0);

			RaycastHit cast;
			if(Physics.Raycast(this.transform.position,vert,out cast,coneRad))
			{
			   vert.Normalize();
			   vert*=cast.distance;
			}
			currPhi-=dPhi;
			verts[i]=vert;
		
			uv[i] =new Vector2(vert.x/2/coneRad + 0.5f,vert.y/2/coneRad + 0.5f);
			norm[i] = Vector3.back;
			//Debug.DrawRay(transform.position,vert);

		}
		lightMesh.vertices = verts;
		lightMesh.triangles = tri;
		lightMesh.uv = uv;
		lightMesh.normals = norm;

		endLight.transform.position = verts [iter / 2] * .8f + transform.position;
		endLight.range = Vector3.Distance (verts [iter / 4], verts [iter / 4 * 3]);
		/*
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
		endLight.intensity = 1 / Vector3.Distance (ray1, ray2)/2;
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

		uv [0] = new Vector2 (0.5f, 0.5f);
		uv [1] = new Vector2 (ray1.x/coneRad + 0.5f,ray1.y/coneRad+ 0.5f);
		uv [2] = new Vector2 (ray1Dot.x/coneRad + 0.5f,ray1Dot.y/coneRad + 0.5f);
		uv [3] = new Vector2 (ray2Dot.x/coneRad + 0.5f,ray2Dot.y/coneRad+ 0.5f);
		uv [4] = new Vector2 (ray2.x/coneRad + 0.5f,ray2.y/coneRad + 0.5f);
		lightMesh.normals = norm;
		lightMesh.uv = uv;
		*/

		/*
		Debug.DrawLine (verts [0]+transform.position, verts [1]+transform.position);
		Debug.DrawLine (verts [1]+transform.position, verts [2]+transform.position);
		Debug.DrawLine (verts [2]+transform.position, verts [3]+transform.position);
		Debug.DrawLine (verts [3]+transform.position, verts [4]+transform.position);
		Debug.DrawLine (verts [4]+transform.position, verts [0]+transform.position);
		*/
	}
	void buildTris(int[] arr)
	{

	}

}
