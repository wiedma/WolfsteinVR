using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class TextBox : MonoBehaviour {

	public string beschreibung = "Beschreibung einfügen";
	public int schriftgröße = 12;
	public float entfernung = 0.1f;
	public GameObject textPanel;

	private GameObject canvas;
	private GameObject textPanelObject;
	private GameObject player;
	private bool repositionAtUpdate = false;

	// Use this for initialization
	void Start () {
		//Format Text
		beschreibung = ResolveTextSize(beschreibung, 40);

		//UI initialization
		canvas = GameObject.Find("Canvas");
		textPanelObject = Instantiate (textPanel, canvas.transform.position, canvas.transform.rotation) as GameObject;
		textPanelObject.transform.SetParent (canvas.transform);
		Text textObject = textPanelObject.GetComponentInChildren<Text> ();
		textObject.fontSize = schriftgröße;
		textObject.text = beschreibung;
		textPanelObject.transform.localScale = new Vector3 (0.001f, 0.001f, 1f);
		textPanelObject.SetActive (false);

		//Event Handling
		EventTrigger trigger = GetComponent<EventTrigger>();
		EventTrigger.Entry entry1 = new EventTrigger.Entry();
		entry1.eventID = EventTriggerType.PointerEnter;
		entry1.callback.AddListener((data) => { OnPointerEnterDelegate((PointerEventData)data); });
		EventTrigger.Entry entry2 = new EventTrigger.Entry ();
		entry2.eventID = EventTriggerType.PointerExit;
		entry2.callback.AddListener((data) => { OnPointerExitDelegate((PointerEventData)data); });
		trigger.triggers.Add (entry1);
		trigger.triggers.Add (entry2);

		//Find Player Object
		player = GameObject.Find("Main Camera");
	}

	public void OnPointerEnterDelegate(PointerEventData data){
		SetUIInFrontOfPlayer ();
		textPanelObject.SetActive (true);
		repositionAtUpdate = true;
	}

	public void OnPointerExitDelegate(PointerEventData data){
		textPanelObject.SetActive (false);
	}

	public void SetUIInFrontOfPlayer(){
		//Set UI in front the player
		Vector3 playerPos = player.transform.position;
		Vector3 forward = player.transform.forward;
		Vector3 uiPos = playerPos + (forward * entfernung);

		//Rotate it 180 degrees so it faces towards the player
		Quaternion playerRotation = player.transform.rotation;
		Vector3 uiRotation = playerRotation.eulerAngles;
//		uiRotation = new Vector3 (uiRotation.x, uiRotation.y + 180, uiRotation.z);

		canvas.transform.SetPositionAndRotation (uiPos, Quaternion.Euler (uiRotation));
	}

	public void Update(){
		if (repositionAtUpdate) {
			SetUIInFrontOfPlayer ();
		}
	}

	// Wrap text by line height
	private string ResolveTextSize(string input, int lineLength){

		// Split string by char " "         
		string[] words = input.Split(" "[0]);

		// Prepare result
		string result = "";

		// Temp line string
		string line = "";

		// for each all words        
		foreach(string s in words){
			// Append current word into line
			string temp = line + " " + s;

			// If line length is bigger than lineLength
			if(temp.Length > lineLength){

				// Append current line into result
				result += line + "\n";
				// Remain word append into new line
				line = s;
			}
			// Append current word into current line
			else {
				line = temp;
			}
		}

		// Append last line into result        
		result += line;

		// Remove first " " char
		return result.Substring(1,result.Length-1);
	}
}
