using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDirection
{
    public int id;
    public string direction;
    public float distanceToTarget, angleToTarget, timeElapsed;
    public ResponseAction response;

    public GameDirection(int id, string dir, float disToTar, float angleToTar, float timeElapsed)
    {
        this.id = id;
        direction = dir;
        distanceToTarget = disToTar;
        angleToTarget = angleToTar;
        this.timeElapsed = timeElapsed;
    }
    public GameDirection(int id, string dir, float disToTar, float angleToTar, float timeElapsed, ResponseAction r)
    {
        this.id = id;
        direction = dir;
        distanceToTarget = disToTar;
        angleToTarget = angleToTar;
        this.timeElapsed = timeElapsed;
        response = r;
    }
    public GameDirection()
    {

    }
}
