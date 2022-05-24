using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDirection
{
    public int id;
    public string direction;
    public float distanceToTarget, angleToTarget, timeElapsed, timeSinceStart;
    public ResponseAction response;

    public GameDirection(int id, string dir, float disToTar, float angleToTar, float timeElapsed)
    {
        this.id = id;
        direction = dir;
        distanceToTarget = disToTar;
        angleToTarget = angleToTar;
        this.timeElapsed = timeElapsed;
    }
    public GameDirection(int id, string dir, float disToTar, float angleToTar, float timeElapsed, float timeSinceStart, ResponseAction r)
    {
        this.id = id;
        direction = dir;
        distanceToTarget = disToTar;
        angleToTarget = angleToTar;
        this.timeElapsed = timeElapsed;
        this.timeSinceStart = timeSinceStart;
        response = r;
    }
    public GameDirection()
    {

    }
}
