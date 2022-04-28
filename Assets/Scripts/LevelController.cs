using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PixelCrushers.DialogueSystem;
using System;
using System.Xml.Serialization;
using System.IO;

public class LevelController : MonoBehaviour
{
    public GameObject pickup;

    public bool timer = false;
    private float elapsedTime = 0;
    public float trialTime = 0;

    private ObjectLoader objectLoader;
    private CalculateDirection calculateDirection;
    public List<Locations> locationsList;

    public int trialNumber = 1;
    public int sequencesCompleted = 0;
    private bool geoFirstSeq;

    public string axis;

    //export this
    public DataCollection trialDatas;

    // Start is called before the first frame update
    void Start()
    {
        objectLoader = FindObjectOfType<ObjectLoader>();
        calculateDirection = FindObjectOfType<CalculateDirection>();
        trialDatas = new DataCollection();
        geoFirstSeq = MainMenu.startGeo;
        if (geoFirstSeq)
        {
            DialogueLua.SetVariable("IsGeo", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ////for use in testing
        //if (Input.GetKeyDown(KeyCode.Return) && locationsList.Count > 0 && GameObject.FindGameObjectsWithTag("pickup").Length == 0)
        //{
        //    SpawnPickup();
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
        if (trialTime >= 90)
        {
            GameObject.FindObjectOfType<Pickup>().CompleteTrial(false);
        }
    }
    public void SpawnPickup() {
        if (locationsList.Count > 0)
        {
            trialTime = 0;
            timer = true;

            trialNumber++;

            int index = UnityEngine.Random.Range(0, locationsList.Count);
            calculateDirection.target = Instantiate(pickup, locationsList.ElementAt(0).location, Quaternion.identity);
            
            locationsList.RemoveAt(index);
        }
        else { Debug.Log("No more locations to spawn"); }
    }
    public void SpawnExample()
    {
        Instantiate(pickup, new Vector3(391.8f, 33.43f, 488.1f), Quaternion.identity);
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
        PixelCrushers.DialogueSystem.DialogueManager.StartConversation("RobOnSequenceEnd");
        DialogueLua.SetVariable("GameFinished", true);
        DialogueLua.SetVariable("IsGeo", !geoFirstSeq);
        trialNumber = 0;
        axis = objectLoader.fileToLoad;
    }
    public void ExportData()
    {
        //Export the data to csv
        if (!Directory.Exists(Application.streamingAssetsPath + "/Export"))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Export");
        }
        string path = Application.streamingAssetsPath + "\\Export\\";
        string exportFile = (DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")).ToString();
        string exportPath = path + exportFile + "-records.csv";
        ExportTrialData.Write(exportPath, trialDatas);

        //Export data to xml
        var serializer = new XmlSerializer(typeof(DataCollection));
        var stream = new FileStream(path + exportFile + "-records.xml", FileMode.Create);
        serializer.Serialize(stream, trialDatas);
    }

    private void OnApplicationQuit()
    {
        ExportData();
    }
}
