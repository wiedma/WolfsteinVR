using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This class manages Waypoints' canvases.
 **/
public class WaypointController : MonoBehaviour {

	Canvas waypointDescriptionCanvas;
	CanvasGroup canvasGroup;

	// Called when this object is created.
	void Awake() {
		waypointDescriptionCanvas = GetComponentInChildren<Canvas> ();

		if (waypointDescriptionCanvas == null) {
			Debug.Log ("Could not get canvas.");
		} else {
			// Get the canvas's CanvasGroup, and hide it.
			canvasGroup = waypointDescriptionCanvas.GetComponent<CanvasGroup>();
			HideDescription();
		}
	}

	// Make the CanvasGroup visible.
	public void ShowDescription() {
		waypointDescriptionCanvas.transform.position = Camera.main.transform.position + new Vector3 (0f, -0.5f, 3f);
		//waypointDescriptionCanvas.transform.rotation = new Quaternion( 0.0f, Camera.main.transform.rotation.y, 0.0f, Camera.main.transform.rotation.w );

		// Set the canvas's forward vector to face the camera,
		// so that the text is right-side up to the camera.
		Vector3 direction = waypointDescriptionCanvas.transform.position - Camera.main.transform.position;
		waypointDescriptionCanvas.transform.forward = direction;

		// Make the CanvasGroup visible, and allow interactions
		// with its UI components.
		canvasGroup.alpha = 1;
		canvasGroup.interactable = true;
	}

	// Hide the CanvasGroup.
	public void HideDescription() {
		if (canvasGroup != null) {
			// Make the CanvasGroup invisible,
			// and do not allow interactions.
			canvasGroup.alpha = 0;
			canvasGroup.interactable = false;
		} else {
			Debug.Log ("canvasGroup is null");
		}
	}

}