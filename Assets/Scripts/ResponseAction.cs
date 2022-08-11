using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResponseAction
{
    public string firstKeyStroke;
    public float firstMouseX; 
    public float firstMouseY;

    public float keyOnsetTime;
    public float mouseOnsetTime;

    public int directionRespondedTo;

    public ResponseAction()
    {

    }
    public ResponseAction(string firstKey, float firstMX, float firstMY)
    {
        firstKeyStroke = firstKey;
        firstMouseX = firstMX;
        firstMouseY = firstMY;
    }
}
