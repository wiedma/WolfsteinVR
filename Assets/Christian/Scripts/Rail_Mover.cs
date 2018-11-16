using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail_Mover : MonoBehaviour {

    public Rail rail;
    public PlayMode mode;

    public float speed = 2.5f;
    public bool isReversed;
    public bool isLooping;
    public bool pingPong;

    private int currentSegment;
    private float transition;
    private bool isCompleted;

	void Update () {
        if (!rail)
            return;

        if (!isCompleted)
            Play(!isReversed);
	}
	
	private void Play(bool forward = true) {
        float m = (rail.nodes[currentSegment + 1].position - rail.nodes[currentSegment].position).magnitude;
        float s = (Time.deltaTime * 1 / m) * speed;
        transition += (forward) ? s :  -s;
        if(transition > 1)
        {
            transition = 0;
            currentSegment++;
            if (currentSegment == rail.nodes.Length - 1)
            {
                if (isLooping)
                {
                    if (pingPong)
                    {
                        transition = 1;
                        currentSegment = rail.nodes.Length - 2;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSegment = 0;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }
        else if(transition < 0)
        {
            transition = 1;
            currentSegment--;
            if (currentSegment == - 1)
            {
                if (isLooping)
                {
                    if (pingPong)
                    {
                        transition = 0;
                        currentSegment = 0;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSegment = rail.nodes.Length - 2;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }

        transform.position = rail.PositionOnRail(currentSegment, transition, mode);
        transform.rotation = rail.Orientation(currentSegment, transition);
	}
}
