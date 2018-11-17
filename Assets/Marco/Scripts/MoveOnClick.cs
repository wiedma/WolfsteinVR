using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

		//Get Object wchich is currently hit by Gaze
		RaycastResult rayRes = GvrPointerInputModule.CurrentRaycastResult;
		GameObject resObj = rayRes.gameObject;

		//Check if that Object is an Eventtrigger
		if (resObj == null) {
			move = true;
		} else {
			move = resObj.GetComponent<EventTrigger> () == null;
		}

		//If the Button on the Cardboard is being pressed
		//First Condition: Not move while interacting
		//Second Condition: Only for the Editor
		//Third Condition: Touchscreen input from Phone (support of the magnetic toucher has been removed somehow)
		if (move && (GvrControllerInput.ClickButton ||
			(Input.touchCount > 0))) {
			//Find the forward vector
			Vector3 forward = vrCam.TransformDirection(Vector3.forward);
			//Move forward
			controller.SimpleMove(forward * speed);
		}
        else
        {
            controller.SimpleMove(Vector3.zero);
        }
		
	}
}
