using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRaycast : MonoBehaviour
{
    CursorPosition cursor;

    // Start is called before the first frame update
    void Start()
    {
        cursor = FindObjectOfType<CursorPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(cursor.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("gaze"))
            {
                Debug.Log("gazing at "+hit.collider.name);
            }
        }
    }
}
