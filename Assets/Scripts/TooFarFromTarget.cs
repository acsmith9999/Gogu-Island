using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooFarFromTarget : MonoBehaviour
{
    private CalculateDirection c;
    // Start is called before the first frame update
    void Start()
    {
        c = FindObjectOfType<CalculateDirection>();
        this.gameObject.GetComponent<CapsuleCollider>().radius = Parameters.targetRadius;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            c.source = "Too far";
            c.GetDirection(Parameters.numberOfAxes);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (c.timeSinceLastDirection <= 0)
        {
            c.source = "Too far";
            c.GetDirection(Parameters.numberOfAxes);
        }
    }
}
