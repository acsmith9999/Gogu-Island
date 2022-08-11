using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDirection
{
    public int id;
    public string direction, landmark ,source;
    public float distanceToTarget, angleToTarget, timeElapsed, timeSinceStart;
    public ResponseAction response;

    public GameDirection(int id, string src, string dir, string lm, float disToTar, float angleToTar, float timeElapsed)
    {
        this.id = id;
        source = src;
        direction = dir;
        landmark = lm;
        distanceToTarget = disToTar;
        angleToTarget = angleToTar;
        this.timeElapsed = timeElapsed;
    }
    public GameDirection(int id, string src, string dir, string lm, float disToTar, float angleToTar, float timeElapsed, float timeSinceStart, ResponseAction r)
    {
        this.id = id;
        source = src;
        direction = dir;
        landmark = lm;
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
