using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CalculateDirection : MonoBehaviour
{
    public FirstPersonController player;
    private LevelController l;
    private SoundManager sm;

    public GameObject target, lostSpawn;
    private GameObject absoluteAnchor, geocentricAnchor;
    
    private string givenDirection;
    private float targetToPlayer, AbsRefToPlayer, refToTarget, angle, inlandToPlayer, inlandToTarget, timeSinceLastDirection;
    public float autoDirection;

    private Vector2 endPointV2, inlandtoV2, playerRef, targetRef;

    private AudioSource src;
    public AudioDirections towardsGeo, awayGeo, towardsAbs, awayAbs;
    
    private int _numberOfDirections = 0;
    public int numberOfDirections 
    { 
        get { return _numberOfDirections; }
        set { _numberOfDirections = value; }
    }

    public List<GameDirection> gameDirections;
    private Transform lastTransform;
    private bool getMovement, getMouseX, getMouseY, getKeyStroke;

    private string firstMovement;
    private Transform compareTransform;
    private float firstMouseX;
    private float firstMouseY;
    private GameDirection temp;
    public List<Vector3> tl;
    public List<MovementData> movementData;

    private string currentSender, prevSender;
    public string source;

    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        src = GetComponent<AudioSource>();
        l = FindObjectOfType<LevelController>();
        sm = FindObjectOfType<SoundManager>();
        tl = new List<Vector3>();
        movementData = new List<MovementData>();

        timeSinceLastDirection = 0;

        absoluteAnchor = GameObject.Find("absolute");
        geocentricAnchor = GameObject.Find("geocentric");

        endPointV2 = new Vector2(absoluteAnchor.transform.position.x, absoluteAnchor.transform.position.z);
        inlandtoV2 = new Vector2(geocentricAnchor.transform.position.x, geocentricAnchor.transform.position.z);

        if (Parameters.wordList1)
        {
            towardsGeo = sm.baki;
            awayGeo = sm.bago;
            towardsAbs = sm.duki;
            awayAbs = sm.dugo;
        }
        else if (!Parameters.wordList1)
        {
            towardsGeo = sm.duki;
            awayGeo = sm.dugo;
            towardsAbs = sm.baki;
            awayAbs = sm.bago;
        }
    }

    public void GetDirection(int axes)
    {
        if (!src.isPlaying && target != null)
        {
            //increase the counter for directions given
            numberOfDirections++;

            //calculate player and target position ----- this is also under calculatedirection function
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
                        currentSender = towardsGeo.plain.name;
                        DetermineAudio(towardsGeo);
                    }
                    else
                    {
                        currentSender = awayGeo.plain.name;
                        DetermineAudio(awayGeo);
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

                    //determine direction
                    if (target.transform.position.z > player.transform.position.z)
                    {
                        currentSender = towardsAbs.plain.name;
                        DetermineAudio(towardsAbs);
                    }
                    else
                    {
                        currentSender = awayAbs.plain.name;
                        DetermineAudio(awayAbs);
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
                    currentSender = towardsGeo.plain.name;
                    DetermineAudio(towardsGeo);
                }
                else if (angle > 120)
                {
                    currentSender = awayGeo.plain.name;
                    DetermineAudio(awayGeo);
                }
                else if (angle > 60 && angle <= 120 && inlandToPlayer < inlandToTarget)
                {
                    currentSender = towardsAbs.plain.name;
                    DetermineAudio(towardsAbs);
                }
                else if (angle > 60 && angle <= 180 && inlandToPlayer > inlandToTarget)
                {
                    currentSender = awayAbs.plain.name;
                    DetermineAudio(awayAbs);
                }
            }
            else { Debug.Log("error calculating"); }

            temp = new GameDirection(numberOfDirections, source, givenDirection, targetToPlayer, angle, l.trialTime, l.elapsedTime, new ResponseAction());
            gameDirections.Add(temp);

            getMovement = true;
            getKeyStroke = true;
            getMouseX = true;
            getMouseY = true;

            timeSinceLastDirection = 0;
        }
        else
        {
            Debug.Log("No targets");
        }
    }

    private void DetermineAudio(AudioDirections audioClips)
    {
        //conditions to determine type of inflected audio
        //check if previous exists for comparison of distance
        if (numberOfDirections > 1)
        {
            //check if direction has changed
            if(currentSender != prevSender)
            {
                //play plain
                //uninflected
                src.PlayOneShot(audioClips.plain);
                givenDirection = audioClips.plain.name;
            }
            else
            {
                //closer or further with same direction
                if (targetToPlayer < gameDirections.Last().distanceToTarget)
                {
                    Debug.Log("closer");
                    //excited dir
                    src.PlayOneShot(audioClips.gettingCloser);
                    givenDirection = audioClips.gettingCloser.name;
                }
                else if (targetToPlayer > gameDirections.Last().distanceToTarget)
                {
                    Debug.Log("further");
                    //emphasised dir
                    src.PlayOneShot(audioClips.gettingFurther);
                    givenDirection = audioClips.gettingFurther.name;
                }
                else
                {
                    Debug.Log("same");
                    //neutral
                    src.PlayOneShot(audioClips.plain);
                    givenDirection = audioClips.plain.name;
                }
            } 
        }
        else if (numberOfDirections == 1)
        {
            //first direction of the trial / game
            //uninflected
            src.PlayOneShot(audioClips.plain);
            givenDirection = audioClips.plain.name;
        }
        prevSender = audioClips.plain.name;
    }

    private void Update()
    {
        //lost key
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.transform.position = lostSpawn.transform.position;
        }
        if (src.isPlaying)
        {
            player.mouseLookEnabled = false;
            player.GetComponent<CharacterController>().enabled = false;
            player.m_UseHeadBob = false;
            player.m_AudioSource.enabled = false;

        }
        else { player.mouseLookEnabled = true;
            player.GetComponent<CharacterController>().enabled = true;
            player.m_UseHeadBob = true;
            player.m_AudioSource.enabled = true;
        }

        if (getMovement)
        {
            lastTransform = player.transform;

            if (getMouseX)
            {
                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    firstMouseX = Input.GetAxis("Mouse X");
                    gameDirections.Last().response.firstMouseX = firstMouseX;
                    getMouseX = false;
                }
            }

            if (getMouseY)
            {
                if(Input.GetAxis("Mouse Y") != 0)
                {
                    firstMouseY = Input.GetAxis("Mouse Y");
                    gameDirections.Last().response.firstMouseY = firstMouseY;
                    getMouseY = false;
                }
            }

            if (getKeyStroke)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    firstMovement = Input.inputString;
                    gameDirections.Last().response.firstKeyStroke = firstMovement;
                    getKeyStroke = false;
                }
            }

            if (getMouseX==false && getKeyStroke == false && getMouseY==false)
            {
                StartCoroutine(getNewTransform());
                getMovement = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            ExportTrialData.ExportMovement(ExportTrialData.movements);
        }
        if(target == null)
        {
            getMovement = false;
        }

    }
    private void FixedUpdate()
    {
        //Directions
        if(target != null)
        {   //when requested by player
            if (Input.GetMouseButtonDown(0))
            {
                source = "Click";
                GetDirection(Parameters.numberOfAxes);
            }
            //player too far away or lost - ontriggerExit from prefab collider in tooFarFromTarget
            
            //player collides with boundaries - in ontrigger function in boundarycollision script

            //more than 20s pass without directions - in next if statement where timer is on

            //player looks at gogu for help

            //NOT DONE YET when player starts moving?
        }
        if (l.timer)
        {
            //calculate player and target position ----- this is also under calculatedirection function
            if(target != null)
            {
                playerRef = new Vector2(player.transform.position.x, player.transform.position.z);
                targetRef = new Vector2(target.transform.position.x, target.transform.position.z);
                //distance from player to target
                targetToPlayer = Vector2.Distance(targetRef, playerRef);

                //movementData.Add(new MovementData(player.transform.position, player.transform.eulerAngles.y, player.transform.GetChild(0).gameObject.transform.eulerAngles.x, l.trialNumber, l.elapsedTime, targetToPlayer));
                ExportTrialData.movements.Add(new MovementData(player.transform.position, player.transform.eulerAngles.y, player.transform.GetChild(0).gameObject.transform.eulerAngles.x, l.trialNumber, l.elapsedTime, targetToPlayer, l.sequencesCompleted));
                if (ExportTrialData.movements.Count > 1)
                {
                    int closerFurther;
                    if (targetToPlayer < ExportTrialData.movements[ExportTrialData.movements.Count-2].distance)
                    {
                        closerFurther = 1;
                    }
                    else if (targetToPlayer > ExportTrialData.movements[ExportTrialData.movements.Count - 2].distance)
                    {
                        closerFurther = -1;
                    }
                        else
                    {
                        closerFurther = 0;
                    }
                ExportTrialData.movements.Last().closerOrFurther = closerFurther;
                }

                timeSinceLastDirection += Time.deltaTime;
            }

            if (timeSinceLastDirection >= autoDirection)
            {
                source = "Wait";
                GetDirection(Parameters.numberOfAxes);
            }
        }
    }
    IEnumerator getNewTransform()
    {
        yield return new WaitForSeconds(1);
        compareTransform = player.transform;
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
