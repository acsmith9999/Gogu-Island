using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementData
{
    public Vector3 pos;
    //rotations?
    public int currentTrial, closerOrFurther;
    public float timeSinceStart, rotX, rotY, distance;


    public MovementData()
    {

    }
    public MovementData(Vector3 p, float x, float y, int c, float time, float dis)
    {
        pos = p;
        rotX = x;
        rotY = y;
        currentTrial = c;
        timeSinceStart = time;
        distance = dis;
    }
}
