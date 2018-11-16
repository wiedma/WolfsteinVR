using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextField : MonoBehaviour {

    public GameObject text;

	// Use this for initialization
	void Start () {
        text.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void ChangeCursor()
    {

    }

    public void Enter()
    {
        text.SetActive(true);
    }

    public void Leave()
    {
        text.SetActive(false);
    }
}
