using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PositionObject : MonoBehaviour
{
    public Transform relativeTo;
    public float distance, minDist;
    CursorPosition cursor;
    public GameObject xUp, xDown, zUp, zDown;
    
    // Start is called before the first frame update
    void Start()
    {
        relativeTo = FindObjectOfType<FirstPersonController>().transform;
        cursor = FindObjectOfType<CursorPosition>();

    }

    // Update is called once per frame
    void Update()
    {
        xUp.transform.position = new Vector3(relativeTo.position.x + distance, (0-relativeTo.GetChild(0).gameObject.transform.eulerAngles.x), relativeTo.position.z);
        xDown.transform.position = new Vector3(relativeTo.position.x - distance, (90-relativeTo.GetChild(0).gameObject.transform.eulerAngles.x), relativeTo.position.z);
        zUp.transform.position = new Vector3(relativeTo.position.x, (180-relativeTo.GetChild(0).gameObject.transform.eulerAngles.x), relativeTo.position.z + distance);
        zDown.transform.position = new Vector3(relativeTo.position.x, (270-relativeTo.GetChild(0).gameObject.transform.eulerAngles.x), relativeTo.position.z - distance);

        Ray ray = Camera.main.ScreenPointToRay(cursor.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, minDist))
        {
            if (hit.collider.CompareTag("gaze"))
            {
                Debug.Log("gazing");
            }
        }

    }
}
