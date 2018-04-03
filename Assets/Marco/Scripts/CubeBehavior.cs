using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour {

	private bool isRotating = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (isRotating) {
			GetComponent<Transform> ().Rotate (0, 1, 0);
		}
		
	}

	public void onClick(){
		isRotating = !isRotating;
	}
}
