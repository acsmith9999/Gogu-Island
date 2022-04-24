using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class CalculateDirection : MonoBehaviour
{
    public GameObject[] anchors;
    private FirstPersonController player;
    public GameObject target;
    public GameObject startCrossAxis, endCrossAxis, inlandAnchor;
    public string sceneName, givenDirection;

    public float targetToPlayer, refToPlayer, refToTarget, minDist;
    public float angle, inlandToPlayer, inlandToTarget;

    private Vector3 aMin;
    public Vector3 targetRef;
    private Vector2 endPointV2, inlandtoV2, playerRef;

    public Text directionText;

    public string towards, away, across1, across2;

    public AudioSource src;
    public AudioClip towardsGeo, awayGeo, towardsAbs, awayAbs;
    
    public int numberOfDirections = 0;
    public List<GameDirection> gameDirections;

    private LevelController l;
    private SoundManager sm;

    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        src = GetComponent<AudioSource>();
        l = FindObjectOfType<LevelController>();
        sm = FindObjectOfType<SoundManager>();

        directionText.text = "";

        playerRef = new Vector2(player.transform.position.x, player.transform.position.z);
        //targetRef = target.transform.position;

        sceneName = SceneManager.GetActiveScene().name;

        endPointV2 = new Vector2(endCrossAxis.transform.position.x, endCrossAxis.transform.position.z);

        if (MainMenu.wordList1)
        {
            towardsGeo = sm.baki;
            awayGeo = sm.bago;
            towardsAbs = sm.duki;
            awayAbs = sm.dugo;
        }
        else if (!MainMenu.wordList1)
        {
            towardsGeo = sm.duki;
            awayGeo = sm.dugo;
            towardsAbs = sm.baki;
            awayAbs = sm.bago;
        }

    }

    public void Triangulate(string sceneName)
    {
        if (target != null)
        {
            numberOfDirections++;
            playerRef = new Vector2(player.transform.position.x, player.transform.position.z);
            if (sceneName == "Hill")
            {
                GetClosestAnchor();
                TriangulateSlope();
            }
            else if (sceneName == "StraightCoast")
            {
                TriangulateCoast();
            }
            else if(sceneName == "RoundCoast")
            {
                TriangulateRoundCoast();
            }

        }

    }
    //Method compares two axes
    public void TriangulateSlope()
    {
        //sides of triangle
        //relative to across axis
        targetToPlayer = Vector2.Distance(new Vector2(targetRef.x, targetRef.z), playerRef);
        refToPlayer = Vector2.Distance(aMin, playerRef);
        refToTarget = Vector2.Distance(aMin, new Vector2(targetRef.x, targetRef.z));

        float cAngCross = (targetToPlayer * targetToPlayer + refToPlayer * refToPlayer - refToTarget * refToTarget) / (2 * targetToPlayer * refToPlayer);

        //relative to slope axis
        float e = Vector2.Distance(endPointV2, playerRef);
        float f = Vector2.Distance(endPointV2, new Vector2(targetRef.x, targetRef.z));
        Debug.Log(e + " " + f);

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
    //Method uses one axis only
    public void TriangulateCoast()
    {
        //sides of triangle
        //relative to up/down solar axis
        targetToPlayer = Vector2.Distance(new Vector2(targetRef.x, targetRef.z), playerRef);
        refToPlayer = Vector2.Distance(endPointV2, playerRef);
        refToTarget = Vector2.Distance(endPointV2, new Vector2(targetRef.x, targetRef.z));

        float cAngSun = (targetToPlayer * targetToPlayer + refToPlayer * refToPlayer - refToTarget * refToTarget) / (2 * targetToPlayer * refToPlayer);

        //relative to land/sea axis
        //method only returns 0-180
        inlandToPlayer = Vector2.Distance(inlandtoV2, playerRef);
        inlandToTarget = Vector2.Distance(inlandtoV2, new Vector2(targetRef.x, targetRef.z));


        float rad = Mathf.Acos(cAngSun);
        angle = Mathf.Rad2Deg * rad; //this is the up/down angle

        if (l.axis.Contains("Land"))
        {
            if (inlandToPlayer < inlandToTarget)
            {
                src.PlayOneShot(towardsGeo);
                givenDirection = towardsGeo.name;
            }
            else if (inlandToPlayer > inlandToTarget)
            {
                src.PlayOneShot(awayGeo);
                givenDirection = awayGeo.name;
            }
        }
        else if (l.axis.Contains("Coast"))
        {
            if (angle <= 90)
            {
                src.PlayOneShot(towardsAbs);
                givenDirection = towardsAbs.name;
            }
            else if (angle > 90)
            {
                src.PlayOneShot(awayAbs);
                givenDirection = awayAbs.name;
            }
        }

        GameDirection temp = new GameDirection(numberOfDirections, givenDirection, targetToPlayer, angle, l.trialTime);
        gameDirections.Add(temp);

    }

    //Method compares two axes
    public void TriangulateRoundCoast()
    {
        targetToPlayer = Vector2.Distance(new Vector2(targetRef.x, targetRef.z), playerRef);
        refToPlayer = Vector2.Distance(endPointV2, playerRef);
        refToTarget = Vector2.Distance(endPointV2, new Vector2(targetRef.x, targetRef.z));

        float cAngSun = (targetToPlayer * targetToPlayer + refToPlayer * refToPlayer - refToTarget * refToTarget) / (2 * targetToPlayer * refToPlayer);

        //relative to land/sea axis
        //method only returns 0-180
        float e = Vector2.Distance(inlandtoV2, playerRef);
        float f = Vector2.Distance(inlandtoV2, new Vector2(targetRef.x, targetRef.z));

        Debug.Log(e + " " + f);

        float rad = Mathf.Acos(cAngSun);
        angle = Mathf.Rad2Deg * rad; //this is the up/down angle


        //if on opposite side on the island, directions are more difficult
        //but that can be avoided through designing the gameplay

        //calculate which side of the object the player is on
        if (angle <= 60)
        {
            directionText.text = towards;
        }
        else if (angle > 120)
        {
            directionText.text = away;
        }
        else if (angle > 60 && angle <= 120 && e < f)
        {
            directionText.text = across1;
        }
        else if (angle > 60 && angle <= 180 && e > f)
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
        //if(target != null)
        //{
        //if(FindObjectOfType<CheckIfMoving>().timer == 0)
        //{
        //playerRef = new Vector2(player.transform.position.x, player.transform.position.z);
        //if (isSlope)
        //{
        //    GetClosestAnchor();
        //    TriangulateSlope();
        //}
        //if (isCoast) 
        //{
        //    TriangulateCoast();
        //    Triangulate(sceneName);
        //}
        //if (isRiver) { }
        //Triangulate(sceneName);
        //}

        //}
        //if (!FindObjectOfType<CheckIfMoving>().isMoving)
        //{
        //    Triangulate(sceneName);
        //}
    }
}
