using UnityEngine;
using System.Collections;

public class BlurControl : MonoBehaviour {
	
	float value; 
	
	// Use this for initialization
	void Start () {
		value = 50.0f;
		transform.GetComponent<Renderer>().material.SetFloat("_blurSizeXY",value);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
}
