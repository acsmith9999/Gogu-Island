using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CalculateDirection : MonoBehaviour
{
    private FirstPersonController player;
    private LevelController l;
    private SoundManager sm;

    public GameObject target;
    private GameObject absoluteAnchor, geocentricAnchor;
    
    private string givenDirection;
    private float targetToPlayer, AbsRefToPlayer, refToTarget, angle, inlandToPlayer, inlandToTarget;

    private Vector2 endPointV2, inlandtoV2, playerRef, targetRef;

    private AudioSource src;
    private AudioClip towardsGeo, awayGeo, towardsAbs, awayAbs;
    
    private int _numberOfDirections = 0;
    public int numberOfDirections 
    { 
        get { return _numberOfDirections; }
        set { _numberOfDirections = value; }
    }

    public List<GameDirection> gameDirections;

    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        src = GetComponent<AudioSource>();
        l = FindObjectOfType<LevelController>();
        sm = FindObjectOfType<SoundManager>();

        absoluteAnchor = GameObject.Find("absolute");
        geocentricAnchor = GameObject.Find("geocentric");

        endPointV2 = new Vector2(absoluteAnchor.transform.position.x, absoluteAnchor.transform.position.z);
        inlandtoV2 = new Vector2(geocentricAnchor.transform.position.x, geocentricAnchor.transform.position.z);

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

    public void GetDirection(int axes)
    {
        //increase th counter for directions given
        numberOfDirections++;
        //calculate player and target position
        playerRef = new Vector2(player.transform.position.x, player.transform.position.z);
        targetRef = new Vector2(target.transform.position.x, target.transform.position.z);

        //distance from player to target
        targetToPlayer = Vector2.Distance(targetRef, playerRef);

        //Method calculates on one axis using transform
        if (axes == 1)
        {
        if (l.axis.Contains("Land"))
        {
            //calculate distances between player target and geo ref
            inlandToPlayer = Vector2.Distance(inlandtoV2, playerRef);
            inlandToTarget = Vector2.Distance(inlandtoV2, targetRef);
        
            //get cosine of angle geocentric ref
            float cAngLand = (targetToPlayer * targetToPlayer + inlandToPlayer * inlandToPlayer - inlandToTarget * inlandToTarget) / (2 * targetToPlayer * inlandToPlayer);
            //convert angle to radians
            float rad2 = Mathf.Acos(cAngLand);
            //convert radians to complementary angle
            angle = Mathf.Rad2Deg * rad2; //this is the up/down angle

            if (target.transform.position.x > player.transform.position.x)
            {
                src.PlayOneShot(towardsGeo);
                givenDirection = towardsGeo.name;
            }
            else
            {
                src.PlayOneShot(awayGeo);
                givenDirection = awayGeo.name;
            }
        }
        else if (l.axis.Contains("Coast"))
        {
            //calculate distances between player target and absolute ref
            AbsRefToPlayer = Vector2.Distance(endPointV2, playerRef);
            refToTarget = Vector2.Distance(endPointV2, targetRef);

            //get cosine of angle absolute ref
            float cAngSun = (targetToPlayer * targetToPlayer + AbsRefToPlayer * AbsRefToPlayer - refToTarget * refToTarget) / (2 * targetToPlayer * AbsRefToPlayer);
            //convert angle to radians
            float rad = Mathf.Acos(cAngSun);
            //convert radians to complementary angle
            angle = Mathf.Rad2Deg * rad; //this is the up/down angle

            if (target.transform.position.z > player.transform.position.z)
            {
                src.PlayOneShot(towardsAbs);
                givenDirection = towardsAbs.name;
            }
            else
            {
                src.PlayOneShot(awayAbs);
                givenDirection = awayAbs.name;
            }
        }
        }
        //Method compares two axes and returns direction up to 180 degrees
        else if (axes == 2)
        {
            //calculate distances between player target and geo ref
            inlandToPlayer = Vector2.Distance(inlandtoV2, playerRef);
            inlandToTarget = Vector2.Distance(inlandtoV2, targetRef);

            //calculate distances between player target and absolute ref
            AbsRefToPlayer = Vector2.Distance(endPointV2, playerRef);
            refToTarget = Vector2.Distance(endPointV2, targetRef);

            //get cosine of angle absolute ref
            float cAngSun = (targetToPlayer * targetToPlayer + AbsRefToPlayer * AbsRefToPlayer - refToTarget * refToTarget) / (2 * targetToPlayer * AbsRefToPlayer);
            //convert angle to radians
            float rad = Mathf.Acos(cAngSun);
            //convert radians to complementary angle
            angle = Mathf.Rad2Deg * rad; //this is the up/down angle

            //calculate which side of the object the player is on
            if (angle <= 60)
            {
                src.PlayOneShot(towardsGeo);
                givenDirection = towardsGeo.name;
            }
            else if (angle > 120)
            {
                src.PlayOneShot(awayGeo);
                givenDirection = awayGeo.name;
            }
            else if (angle > 60 && angle <= 120 && inlandToPlayer < inlandToTarget)
            {
                src.PlayOneShot(towardsAbs);
                givenDirection = towardsAbs.name;
            }
            else if (angle > 60 && angle <= 180 && inlandToPlayer > inlandToTarget)
            {
                src.PlayOneShot(awayAbs);
                givenDirection = awayAbs.name;
            }
        }
        else { Debug.Log("error calculating"); }

        GameDirection temp = new GameDirection(numberOfDirections, givenDirection, targetToPlayer, angle, l.trialTime);
        gameDirections.Add(temp);
        Debug.Log(temp.id + " " + temp.direction + " " + temp.distanceToTarget + " " + temp.angleToTarget + " " + temp.timeElapsed);
    }

    #region old methods
    public void Triangulate(string sceneName)
    {
        if (target != null)
        {
            numberOfDirections++;
            playerRef = new Vector2(player.transform.position.x, player.transform.position.z);
            if (sceneName == "Hill")
            {
                //GetClosestAnchor();
                TriangulateSlope();
            }
            else if (sceneName == "StraightCoast")
            {
                //TriangulateCoast();

            }
            else if (sceneName == "RoundCoast")
            {
                //TriangulateRoundCoast();

            }

        }

    }
    public void TriangulateSlope()
    {
        //sides of triangle
        //relative to across axis
        //targetToPlayer = Vector2.Distance(new Vector2(targetRef.x, targetRef.z), playerRef);
        //AbsRefToPlayer = Vector2.Distance(aMin, playerRef);
        //refToTarget = Vector2.Distance(aMin, new Vector2(targetRef.x, targetRef.z));

        //float cAngCross = (targetToPlayer * targetToPlayer + AbsRefToPlayer * AbsRefToPlayer - refToTarget * refToTarget) / (2 * targetToPlayer * AbsRefToPlayer);

        ////relative to slope axis
        //float e = Vector2.Distance(endPointV2, playerRef);
        //float f = Vector2.Distance(endPointV2, new Vector2(targetRef.x, targetRef.z));
        //Debug.Log(e + " " + f);

        //float rad = Mathf.Acos(cAngCross);
        //angle = Mathf.Rad2Deg * rad; //this is the up/down angle

        ////calculate which side of the object the player is on
        //if (angle <= 60)
        //{

        //}
        //else if (angle > 120)
        //{

        //}
        //else if (angle > 60 && angle <= 120 && e < f)
        //{

        //}
        //else if (angle > 60 && angle <= 120 && e > f)
        //{

        //}

    }
    public void TriangulateCoast()
    {

        ////sides of triangle
        ////relative to up/down solar axis
        //targetToPlayer = Vector2.Distance(new Vector2(targetRef.x, targetRef.z), playerRef);
        //AbsRefToPlayer = Vector2.Distance(endPointV2, playerRef);
        //refToTarget = Vector2.Distance(endPointV2, new Vector2(targetRef.x, targetRef.z));

        ////(a2 + b2 - c2)/(2ab)
        //float cAngSun = (targetToPlayer * targetToPlayer + AbsRefToPlayer * AbsRefToPlayer - refToTarget * refToTarget) / (2 * targetToPlayer * AbsRefToPlayer);

        ////relative to land/sea axis
        ////method only returns 0-180
        //inlandToPlayer = Vector2.Distance(inlandtoV2, playerRef);
        //inlandToTarget = Vector2.Distance(inlandtoV2, new Vector2(targetRef.x, targetRef.z));


        //float rad = Mathf.Acos(cAngSun);
        //angle = Mathf.Rad2Deg * rad; //this is the up/down angle

        //if (l.axis.Contains("Land"))
        //{
        //    if (inlandToPlayer < inlandToTarget)
        //    {
        //        src.PlayOneShot(towardsGeo);
        //        givenDirection = towardsGeo.name;
        //    }
        //    else if (inlandToPlayer > inlandToTarget)
        //    {
        //        src.PlayOneShot(awayGeo);
        //        givenDirection = awayGeo.name;
        //    }
        //}
        //else if (l.axis.Contains("Coast"))
        //{
        //    if (angle <= 90)
        //    {
        //        src.PlayOneShot(towardsAbs);
        //        givenDirection = towardsAbs.name;
        //    }
        //    else if (angle > 90)
        //    {
        //        src.PlayOneShot(awayAbs);
        //        givenDirection = awayAbs.name;
        //    }
        //}

        //GameDirection temp = new GameDirection(numberOfDirections, givenDirection, targetToPlayer, angle, l.trialTime);
        //gameDirections.Add(temp);

    }
    //Method compares two axes and returns direction in 360 degrees
    public void TwoAxes()
    {
        //playerRef = new Vector2(player.transform.position.x, player.transform.position.z);
        //Vector2 targetRefV2 = new Vector2(targetRef.x, targetRef.z);
        //targetToPlayer = Vector2.Distance(targetRefV2, playerRef);
        //AbsRefToPlayer = Vector2.Distance(endPointV2, playerRef);
        //refToTarget = Vector2.Distance(endPointV2, targetRefV2);

        //float cAngSun = (targetToPlayer * targetToPlayer + AbsRefToPlayer * AbsRefToPlayer - refToTarget * refToTarget) / (2 * targetToPlayer * AbsRefToPlayer);

        //inlandToPlayer = Vector2.Distance(inlandtoV2, playerRef);
        //inlandToTarget = Vector2.Distance(inlandtoV2, targetRefV2);

        //float rad = Mathf.Acos(cAngSun);
        //angle = Mathf.Rad2Deg * rad; //this is the up/down angle

        ////calculate which side of the object the player is on
        //if (angle <= 60)
        //{
        //    src.PlayOneShot(towardsGeo);
        //    givenDirection = towardsGeo.name;
        //}
        //else if (angle > 120)
        //{
        //    src.PlayOneShot(awayGeo);
        //    givenDirection = awayGeo.name;
        //}
        //else if (angle > 60 && angle <= 120 && inlandToPlayer < inlandToTarget)
        //{
        //    src.PlayOneShot(towardsAbs);
        //    givenDirection = towardsAbs.name;
        //}
        //else if (angle > 60 && angle <= 180 && inlandToPlayer > inlandToTarget)
        //{
        //    src.PlayOneShot(awayAbs);
        //    givenDirection = awayAbs.name;
        //}
    }

    //public void GetClosestAnchor()
    //{
    //    minDist = Mathf.Infinity;
    //    //find closest anchor point to base calculations off
    //    foreach (GameObject a in anchors)
    //    {
    //        float dist = Vector3.Distance(a.transform.position, playerRef);
    //        if (dist <= minDist)
    //        {
    //            aMin = a.transform.position;
    //            minDist = dist;
    //        }
    //    }
    //}

    #endregion
}
