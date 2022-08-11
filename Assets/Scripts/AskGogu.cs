using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskGogu : MonoBehaviour
{
    private CalculateDirection c;
    public bool inRange = false;
    public float minDist;
    CursorPosition cursor;

    // Start is called before the first frame update
    void Start()
    {
        c = FindObjectOfType<CalculateDirection>();
        cursor = FindObjectOfType<CursorPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (inRange && Vector3.Distance(this.transform.position, c.player.transform.position) < minDist)
        //{
        //    c.source = "Ask Gogu";
        //    c.GetDirection(Parameters.numberOfAxes);
        //    Debug.Log("in range");
        //}

    }
    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(cursor.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, minDist))
        {
            if (hit.collider.name == "GoguCollider")
            {
                c.source = "Ask Gogu";
                c.GetDirection(Parameters.numberOfAxes);

            }
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
