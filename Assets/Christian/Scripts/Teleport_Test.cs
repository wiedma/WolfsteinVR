using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Test : MonoBehaviour {
    public bool TeleportEnabled
    {
        get { return teleportEnabled; }
    }

    private bool firstClick;
    private float firstClickTime;
    public float doubleClickTimeLimit = 0.5f;

    private bool teleportEnabled;

    public Bezier bezier;
    public GameObject teleportSprite;
    public GameObject player;


	// Use this for initialization
	void Start () {
        teleportEnabled = false;
        firstClick = false;
        //firstClickTime = 0f;
        teleportSprite.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTeleportEnabled();

        if (teleportEnabled)
        {
            HandleTeleport();
        }
	}

    void UpdateTeleportEnabled()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            if (!firstClick)
            {
                Debug.Log("First Click");
                firstClick = true;
                firstClickTime = Time.unscaledTime;
            }
            else
            {
                Debug.Log("Double Click");
                firstClick = false;
                ToggleTeleportMode();
            }
        }

        if(Time.unscaledTime - firstClickTime > doubleClickTimeLimit)
        {
            //Debug.Log("Reset timer");
            firstClick = false;
        }
    }

    void HandleTeleport()
    {
        if (bezier.endPointDetected)
        {
            teleportSprite.SetActive(true);
            teleportSprite.transform.position = bezier.EndPoint;

            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            {
                TeleportToPosition(bezier.EndPoint);
            }
        }
        else
        {
            teleportSprite.SetActive(false);
        }
    }

    void TeleportToPosition(Vector3 teleportPos)
    {
        player.transform.position = teleportPos + Vector3.up * 1f;
    }

    void ToggleTeleportMode()
    {
        teleportEnabled = !teleportEnabled;
        bezier.ToggleDraw(teleportEnabled);
        if (!teleportEnabled)
        {
            teleportSprite.SetActive(false);
        }
    }
}
