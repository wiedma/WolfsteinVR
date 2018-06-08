using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class TextBox : MonoBehaviour {

	public string beschreibung = "Beschreibung einfügen";
	public int schriftgröße = 12;
	public GameObject textPanel;

	private GameObject canvas;
	private GameObject textPanelObject;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("Canvas");
		textPanelObject = Instantiate (textPanel, canvas.transform.position, canvas.transform.rotation) as GameObject;
		textPanelObject.transform.SetParent (canvas.transform);
		Text textObject = textPanelObject.GetComponentInChildren<Text> ();
		textObject.fontSize = schriftgröße;
		textObject.text = beschreibung;
		textPanelObject.SetActive (false);
		EventTrigger trigger = GetComponent<EventTrigger>();
		EventTrigger.Entry entry1 = new EventTrigger.Entry();
		entry1.eventID = EventTriggerType.PointerEnter;
		entry1.callback.AddListener((data) => { OnPointerEnterDelegate((PointerEventData)data); });
		EventTrigger.Entry entry2 = new EventTrigger.Entry ();
		entry2.eventID = EventTriggerType.PointerExit;
		entry2.callback.AddListener((data) => { OnPointerExitDelegate((PointerEventData)data); });
		trigger.triggers.Add (entry1);
		trigger.triggers.Add (entry2);
	}

	public void OnPointerEnterDelegate(PointerEventData data){
		textPanelObject.SetActive (true);
	}

	public void OnPointerExitDelegate(PointerEventData data){
		textPanelObject.SetActive (false);
	}
}
