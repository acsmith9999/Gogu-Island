using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrialData
{
    public int trialNumber;
    public int sequenceNumber;
    public string axis;
    public int numberOfDirections;
    public float timeTaken;
    public List<GameDirection> gameDirections;
    public bool success;


    public TrialData(int trialNumber,int seqNo, string axis, int numberOfDirections, float timeTaken, List<GameDirection> gameDirections, bool success)
    {
        this.trialNumber = trialNumber;
        this.sequenceNumber = seqNo;
        this.axis = axis;
        this.numberOfDirections = numberOfDirections;
        this.timeTaken = timeTaken;
        this.gameDirections = gameDirections;
        this.success = success;

    }
    public TrialData()
    {

    }
}
