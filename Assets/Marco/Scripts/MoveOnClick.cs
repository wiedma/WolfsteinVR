using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveOnClick : MonoBehaviour {

	//Movement speed
	public float speed = 3.0F;

	//Represents if you want to move or interact
	private bool move;

	//CharacterController Component
	private CharacterController controller;

	//VR Head
	private Transform vrCam;

	// Use this for initialization
	void Start () {
		//Find the CharacterController
		controller = GetComponent<CharacterController>();
		//Find the VR Head
		vrCam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {


		//If the Button on the Cardboard is being pressed
		//First Condition: Only for the Editor
		//Second Condition: Touchscreen input from Phone (support of the magnetic toucher has been removed somehow)
		if (GvrControllerInput.ClickButton ||
			(Input.touchCount > 0)) {
			//Find the forward vector
			Vector3 forward = vrCam.TransformDirection(Vector3.forward);
			//Move forward
			controller.SimpleMove(forward * speed);
		}
		
	}
}
