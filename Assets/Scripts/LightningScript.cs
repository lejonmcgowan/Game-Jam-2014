using UnityEngine;
using System.Collections;

public class LightningScript : MonoBehaviour {
	public int lightningChance;
	public Light lightning;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lightning.enabled = false;
		if (Random.Range (0, lightningChance) == 0) {
			lightning.enabled=true;
		}
	}
}
