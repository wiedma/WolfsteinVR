using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour {

    private bool open;
    private bool busy;

    public AudioClip openDoor;
    public AudioClip closeDoor;
    public AudioSource audioSource;

    // Use this for initialization
    void Start () {
        open = false;
        busy = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        if (!busy)
        {
            if (open)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
    }

    //Öffnet die Tür
    public void OpenDoor()
    {
        audioSource.clip = openDoor;
        audioSource.Play();
        open = true;
    }

    //Schließt die Tür
    public void CloseDoor()
    {
        audioSource.clip = closeDoor;
        audioSource.Play();
        open = false;
    }
}
