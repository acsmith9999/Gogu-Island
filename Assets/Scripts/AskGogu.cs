using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskGogu : MonoBehaviour
{
    private CalculateDirection c;
    public bool inRange = false;
    public float minDist;
    // Start is called before the first frame update
    void Start()
    {
        c = FindObjectOfType<CalculateDirection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Vector3.Distance(this.transform.position, c.player.transform.position) < minDist)
        {
            c.source = "Ask Gogu";
            c.GetDirection(Parameters.numberOfAxes);
            Debug.Log("in range");
        }

    }

    private void OnMouseEnter()
    {
        inRange = true;
    }
    private void OnMouseExit()
    {
        inRange = false;
    }
}
