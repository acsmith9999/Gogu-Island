using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PixelCrushers.DialogueSystem;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections;

public class LevelController : MonoBehaviour
{
    public GameObject pickup, example, tutorial, exampleSpawn;

    public bool timer = false;
    public bool tutorialActive;
    public float elapsedTime = 0;
    public float trialTime = 0;

    private ObjectLoader objectLoader;
    private CalculateDirection calculateDirection;
    public List<Locations> locationsList;

    public int trialNumber = 0;
    public int sequencesCompleted = 0;
    private bool geoFirstSeq;

    public string axis;

    public GameObject geoBoundary, absBoundary, tutTrigger;

    //export this
    public DataCollection trialDatas;

    private Tutorial tut;

    // Start is called before the first frame update
    void Start()
    {
        objectLoader = FindObjectOfType<ObjectLoader>();
        calculateDirection = FindObjectOfType<CalculateDirection>();
        trialDatas = new DataCollection();
        tut = FindObjectOfType<Tutorial>();
        tutTrigger.SetActive(false);

        geoFirstSeq = Parameters.startGeo;
        if (geoFirstSeq)
        {
            DialogueLua.SetVariable("IsGeo", true);
        }
        geoBoundary = GameObject.FindGameObjectWithTag("geoBoundary");
        geoBoundary.SetActive(false);
        absBoundary = GameObject.FindGameObjectWithTag("absBoundary");
        absBoundary.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ////for use in testing
        //if (Input.GetKeyDown(KeyCode.Backspace))
        //{
        //    objectLoader.AddObjectsToLists();
        //}
        //if (Input.GetKeyDown(KeyCode.Return) && locationsList.Count > 0 && GameObject.FindGameObjectsWithTag("pickup").Length == 0)
        //{
        //    SpawnTutorial();
        //}

        ////for use in testing
        //if (Input.GetKeyDown(KeyCode.Backspace))
        //{
        //    foreach (GameObject g in GameObject.FindGameObjectsWithTag("pickup"))
        //    {
        //        Destroy(g);
        //    }
        //    //if (locationsList.Count ==0 && objectLoader.inRange == true)
        //    //{
        //    //    objectLoader.AddObjectsToLists();
        //    //}
        //    // The only way at the moment to repopulate the items is by leaving the trigger area
        //    timer = false;
        //    //elapsedTime = 0;
        //}

        if (timer)
        {
            elapsedTime += Time.deltaTime;
            trialTime += Time.deltaTime;
            
        }



    }
    private void LateUpdate()
    {
        //1min time limit per trial
        if (sequencesCompleted > 0)
        {
            if (trialTime >= 60)
            {
                GameObject.FindObjectOfType<Pickup>().CompleteTrial(false);
            }
        }

    }
    public void SpawnPickup() {
        if (locationsList.Count > 0)
        {
            trialTime = 0;
            timer = true;

            trialNumber++;

            //int index = UnityEngine.Random.Range(0, locationsList.Count);
            calculateDirection.target = Instantiate(pickup, locationsList.ElementAt(0).location, Quaternion.identity);
            calculateDirection.source = "Spawn";
            calculateDirection.timeSinceLastDirection = 4;
            calculateDirection.GetDirection(Parameters.numberOfAxes);

            locationsList.RemoveAt(0);
        }
        else { Debug.Log("No more locations to spawn"); }
    }
    public void SpawnExample()
    {
        Instantiate(example, exampleSpawn.transform.position, Quaternion.identity);
    }
    public void SpawnTutorial()
    {
        if (locationsList.Count > 0)
        {
            trialTime = 0;
            timer = true;
            trialNumber++;
            calculateDirection.target = Instantiate(tutorial, locationsList.ElementAt(0).location, Quaternion.identity);
            calculateDirection.source = "TutorialSpawn";
            calculateDirection.GetDirection(Parameters.numberOfAxes);
            //camera to target 2s. fix this
            //PixelCrushers.DialogueSystem.DialogueManager.PlaySequence("Camera(Full Back,listener,1);Delay(2)", calculateDirection.target.transform, calculateDirection.target.transform);
            //FindObjectOfType<Camera>().transform.LookAt(calculateDirection.target.transform);

            locationsList.RemoveAt(0);
        }
        else { Debug.Log("No more locations to spawn"); timer = false; }
    }

    public void DestroyTargets()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("pickup"))
        {
            Destroy(g);
        }
    }
    public void LoadNextSequence()
    {

        StartCoroutine(Wait2Seconds());
        
        //PixelCrushers.DialogueSystem.DialogueManager.StartConversation("RobOnSequenceEnd");
        PixelCrushers.DialogueSystem.DialogueManager.StartConversation("Bob2");
        DialogueLua.SetVariable("GameFinished", true);
        DialogueLua.SetVariable("IsGeo", !geoFirstSeq);
        trialNumber = 0;
        objectLoader.WhichFileToLoad();
        axis = objectLoader.fileToLoad;
    }
    private IEnumerator Wait2Seconds()
    {
        yield return new WaitForSeconds(2);
        calculateDirection.ReturnToStart();
    }

    public void ReadyToStartTutorial()
    {
        tutTrigger.SetActive(true);
        tutorialActive = true;
    }
    public void TutorialFinished()
    {
        PixelCrushers.DialogueSystem.DialogueManager.StartConversation("TutorialFinished");
        DialogueLua.SetVariable("TutorialFinished", true);
        trialNumber = 0;
        objectLoader.WhichFileToLoad();
        axis = objectLoader.fileToLoad;
        tut.TutorialBoundaries();
        tutorialActive = false;
    }

    public void Boundaries()
    {
        if (axis.Contains("LandSea"))
        {
            geoBoundary.SetActive(true);
            absBoundary.SetActive(false);
        }
        else if (axis.Contains("CoastUpDown"))
        {
            geoBoundary.SetActive(false);
            absBoundary.SetActive(true);
        }
    }
}
