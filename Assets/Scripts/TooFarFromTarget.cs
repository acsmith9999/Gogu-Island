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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            c.source = "Too far";
            c.GetDirection(Parameters.numberOfAxes);
        }
    }
}
