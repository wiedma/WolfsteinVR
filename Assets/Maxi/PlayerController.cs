using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/**
 * This class manages the movement of the player.
 **/
public class PlayerController : MonoBehaviour {

	// Array of waypoints that the player will visit.
	// Set in the Inspector.
	public WaypointController[] waypoints;
	public float speed = 2f;

	// The current waypoint that the player is traveling to.
	int currentWaypoint = 0;
	// Flag that indicates whether the player is currently in motion.
	bool isMoving = true;

	// Checks if any of the Waypoints are null.
	void CheckWaypoints() {
		Debug.Log("Checking " + waypoints.Length + " waypoints");
		if (waypoints == null) {
			Debug.Log ("waypoints array is null");
		}
		for (int i = 0; i < waypoints.Length; i++) {
			if (waypoints [i] == null) {
				Debug.Log ("Waypoint " + i + " is null.");
			}
		}
	}

	// Checks if the player has reached the current waypoint.
	bool AtWaypoint() {
		float distance;
		if (waypoints [currentWaypoint] != null) {
			distance = Vector3.Distance (transform.position, waypoints [currentWaypoint].transform.position);
		} else {
			Debug.Log ("Cannot determine distance because waypoint is null.");
			distance = 0;
		}

		return (distance == 0);
	}

	// Update is called once per frame.
	// If isMoving is true, and the player has not arrived at the current
	// Waypoint, move the player towards the Waypoint.
	void Update () {
		if (isMoving) {
			if (!AtWaypoint ()) {
				transform.position = Vector3.MoveTowards (transform.position, waypoints [currentWaypoint].transform.position, speed * Time.deltaTime);
			} else {
				Debug.Log ("Arrived at waypoint " + currentWaypoint + ". Showing description.");
				isMoving = false;
				waypoints [currentWaypoint].ShowDescription ();
			}
		}
	}

	// Called after the player clicks on the button to close a waypoint's description.
	public void ContinueTour() {

		Debug.Log("ContinueTour: currentWaypoint=" + currentWaypoint);

		if (AtWaypoint() && waypoints [currentWaypoint] != null) {
			// Hide the description.
			Debug.Log ("Hiding waypoint " + currentWaypoint);
			waypoints [currentWaypoint].HideDescription ();
			// If this is the last waypoint in the circuit,
			// load the next scene.
			if (currentWaypoint == waypoints.Length - 1) {
				Debug.Log ("Reached end of tour.");
				Debug.Log("Loading next scene");
				LoadNextScene ();

			}
			// If there are more waypoints, continue to the next waypoint.
			else {
				currentWaypoint++;
				Debug.Log ("Next waypoint: " + currentWaypoint);
				isMoving = true;
			}
		} else {
			Debug.Log ("ERROR: Something's wrong... Either we have not arrived at the waypoint, or the waypoint is null");
		}

	}

	// Loads the next scene. If we're already at the last scene,
	// loads the first scene.
	void LoadNextScene() {
		Debug.Log ("Loading next scene");
		int currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1) {
			SceneManager.LoadScene (currentSceneIndex + 1);
		} else {
			SceneManager.LoadScene (0);
		}
	}
} 
