using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class Door : MonoBehaviour
{

    //Beschreibt, ob die Tür gerade geöffnet oder geschlossen ist
    private bool open;
    //Beschreibt, ob die Tür sich gerade öffnet, oder schließt
    private bool busy;
    //Der Punkt im Worldspace um den die Tür rotiert
    private Vector3 anchor;

    //Die Höhe der Tür
    public float height = 1f;
    //Die Breite der Tür
    public float width = 1f;
    //Die Geschwindigkeit, mit der sich die Tür dreht
    public float speed = 1f;

    void Start()
    {
        open = false;
        busy = false;

        //Berechne den Punkt um den sich die Tür drehen soll
        Vector3 pos = transform.position;
        Vector3 bottomCenter = pos + new Vector3(0, -(height / 2f), 0);
        Vector3 bottomCorner = bottomCenter + transform.right * (width / 2f);
        anchor = bottomCorner;
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
        //transform.RotateAround (anchor, Vector3.up, 90);
        StartCoroutine("Rotate", true);
        open = true;
    }

    //Schließt die Tür
    public void CloseDoor()
    {
        //transform.RotateAround (anchor, Vector3.up, -90);
        StartCoroutine("Rotate", false);
        open = false;
    }

    //Coroutine zum langsamen öffnen der Tür
    public IEnumerator Rotate(bool positive)
    {
        busy = true;
        float rotationSum = 0;
        float desiredRotation = (positive) ? 90 : -90;
        while (rotationSum != desiredRotation)
        {
            float nextRotation = Mathf.Min(Mathf.Abs(speed * Time.deltaTime), Mathf.Abs(desiredRotation - rotationSum)) * ((positive) ? 1 : -1);
            transform.RotateAround(anchor, Vector3.up, nextRotation);
            rotationSum += nextRotation;
            yield return null;
        }
        busy = false;
    }
}
