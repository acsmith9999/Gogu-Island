using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CalculateDirection : MonoBehaviour
{
    public GameObject[] anchors;
    public GameObject player;
    public GameObject target;
    public GameObject startCrossAxis, endCrossAxis, inlandAnchor;

    private float a, b, c, minDist;
    public float angle;

    private Vector3 aMin, playerRef, targetRef;
    private Vector2 startPointV2, endPointV2, inlandtoV2;

    public Text directionText;
    public Button directionButton;

    public string towards, away, across1, across2;

    private bool isSlope, isCoast, isRiver;

    private void Start()
    {
        directionText.text = "";

        playerRef = player.transform.position;
        targetRef = target.transform.position;


        startPointV2 = new Vector2(startCrossAxis.transform.position.x, startCrossAxis.transform.position.z);
        endPointV2 = new Vector2(endCrossAxis.transform.position.x, endCrossAxis.transform.position.z);


        if (SceneManager.GetActiveScene().name == "Hill")
        {
            isSlope = true;
        }
        else if (SceneManager.GetActiveScene().name == "Coast")
        {
            isCoast = true;
            inlandAnchor = GameObject.Find("inland");
            inlandtoV2 = new Vector2(inlandAnchor.transform.position.x, inlandAnchor.transform.position.z);
        }
        else if(SceneManager.GetActiveScene().name == "Flat River"){
            isRiver = true;
        }
    }

    public void TriangulateSlope()
    {
        //sides of triangle
        //relative to across axis
        a = Vector2.Distance(new Vector2(targetRef.x, targetRef.z), new Vector2(playerRef.x, playerRef.z));
        b = Vector2.Distance(aMin, new Vector2(playerRef.x, playerRef.z));
        c = Vector2.Distance(aMin, new Vector2(targetRef.x, targetRef.z));

        float cAngCross = (a * a + b * b - c * c) / (2 * a * b);

        //relative to slope axis
        float e = Vector2.Distance(endPointV2, new Vector2(playerRef.x, playerRef.z));
        float f = Vector2.Distance(endPointV2, new Vector2(targetRef.x, targetRef.z));


        float rad = Mathf.Acos(cAngCross);
        angle = Mathf.Rad2Deg * rad; //this is the up/down angle

        //calculate which side of the object the player is on
        if (angle <= 60)
        {
            directionText.text = away;
        }
        else if (angle > 120)
        {
            directionText.text = towards;
        }
        else if (angle > 60 && angle <= 120 && e < f)
        {
            directionText.text = across1;
        }
        else if (angle > 60 && angle <= 120 && e > f)
        {
            directionText.text = across2;
        }

    }
    public void TriangulateCoast()
    {
        //sides of triangle
        //relative to up/down solar axis
        //method only returns 0-180
        a = Vector2.Distance(new Vector2(targetRef.x, targetRef.z), new Vector2(playerRef.x, playerRef.z));
        b = Vector2.Distance(endPointV2, new Vector2(playerRef.x, playerRef.z));
        c = Vector2.Distance(endPointV2, new Vector2(targetRef.x, targetRef.z));

        float cAngSun = (a * a + b * b - c * c) / (2 * a * b);

        //relative to land/sea axis
        //method only returns 0-180
        float e = Vector2.Distance(inlandtoV2, new Vector2(playerRef.x, playerRef.z));
        float f = Vector2.Distance(inlandtoV2, new Vector2(targetRef.x, targetRef.z));

        float rad = Mathf.Acos(cAngSun);
        angle = Mathf.Rad2Deg * rad; //this is the up/down angle


        //if on opposite side on the island, directions are more difficult
        //but that can be avoided through designing the gameplay

        //calculate which side of the object the player is on
        if(angle <= 60)
        {
            directionText.text = towards;
        }
        else if(angle > 120)
        {
            directionText.text = away;
        }
        else if(angle > 60 && angle <= 120 && e < f)
        {
            directionText.text = across1;
        }
        else if (angle > 60 && angle <= 120 && e > f)
        {
            directionText.text = across2;
        }

    }

    public void GetClosestAnchor()
    {
        minDist = Mathf.Infinity;
        //find closest anchor point to base calculations off
        foreach (GameObject a in anchors)
        {
            float dist = Vector3.Distance(a.transform.position, playerRef);
            if (dist <= minDist)
            {
                aMin = a.transform.position;
                minDist = dist;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerRef = new Vector2(player.transform.position.x, player.transform.position.z);
            if (isSlope)
            {
                GetClosestAnchor();
                TriangulateSlope();
            }
            if (isCoast) {TriangulateCoast();}
            if (isRiver) { }
        }

    }
}
