using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCollision : MonoBehaviour
{
    private CalculateDirection c;
    // Start is called before the first frame update
    void Start()
    {
        c = FindObjectOfType<CalculateDirection>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            c.source = "Boundary Collision";
            c.GetDirection(Parameters.numberOfAxes);
        }
    }
}
