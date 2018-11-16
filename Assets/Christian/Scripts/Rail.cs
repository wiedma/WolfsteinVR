using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum PlayMode
{
    Linear,
    Catmull,
}

 [ExecuteInEditMode]
public class Rail : MonoBehaviour {

    public Transform[] nodes;

    void Start ()
    {
        nodes = GetComponentsInChildren<Transform>();
	}

    public Vector3 PositionOnRail(int segment, float reatio, PlayMode mode)
    {
        switch (mode)
        {
            default:
            case PlayMode.Linear:
                return LinearPosition(segment, reatio);
            case PlayMode.Catmull:
                return CatmullPosition(segment, reatio);
        }
    }

    public Vector3 LinearPosition(int segment, float ration)
    {
        Vector3 p1 = nodes[segment].position;
        Vector3 p2 = nodes[segment + 1].position;

        return Vector3.Lerp(p1, p2, ration);
    }
    public Vector3 CatmullPosition(int segment, float ration)
    {
        Vector3 p1, p2, p3, p4;

        if (segment == 0)
        {
            p1 = nodes[segment].position;
            p2 = p1;
            p3 = nodes[segment + 1].position;
            p4 = nodes[segment + 2].position;
        }
        else if (segment == nodes.Length - 2)
        {
            p1 = nodes[segment - 1].position;
            p2 = nodes[segment].position;
            p3 = nodes[segment + 1].position;
            p4 = p3;
        }
        else
        {
            p1 = nodes[segment - 1].position;
            p2 = nodes[segment].position;
            p3 = nodes[segment + 1].position;
            p4 = nodes[segment + 2].position;
        }

        float t2 = ration * ration;
        float t3 = t2 * ration;

        float x =
            0.5f * ((2.0f * p2.x) +
            (-p1.x + p3.x) * ration +
            (2.0f * p1.x - 5.0f * p2.x + 4 * p3.x - p4.x) * t2 +
            (-p1.x + 3.0f * p2.x - 3.0f * p3.x + p4.x) * t3);

        float y =
            0.5f * ((2.0f * p2.y) +
            (-p1.y + p3.y) * ration +
            (2.0f * p1.y - 5.0f * p2.y + 4 * p3.y - p4.y) * t2 +
            (-p1.y + 3.0f * p2.y - 3.0f * p3.y + p4.y) * t3);

        float z =
            0.5f * ((2.0f * p2.z) +
            (-p1.z + p3.z) * ration +
            (2.0f * p1.z - 5.0f * p2.z + 4 * p3.z - p4.z) * t2 +
            (-p1.z + 3.0f * p2.z - 3.0f * p3.z + p4.z) * t3);

        return new Vector3(x, y, z);
    }


    public Quaternion Orientation(int segment, float ration)
    {
        Quaternion q1 = nodes[segment].rotation;
        Quaternion q2 = nodes[segment + 1].rotation;

        return Quaternion.Lerp(q1, q2, ration);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < nodes.Length - 1; i++)
        {
            Handles.DrawDottedLine(nodes[i].position, nodes[i + 1].position, 3.0f);
        }
    }

}
