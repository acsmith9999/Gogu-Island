using System.Collections.Generic;
using System;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Collections;

public class Pickup : MonoBehaviour
{
    private LevelController levelController;
    private CalculateDirection c;

    private TrialData currentTrial;
    private SoundManager sm;
    private CheckIfMoving checkIfMoving;


    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        c = FindObjectOfType<CalculateDirection>();
        sm = FindObjectOfType<SoundManager>();
        checkIfMoving = FindObjectOfType<CheckIfMoving>();

    }

    private void OnTriggerEnter(Collider other)
    {
        CompleteTrial(true);
    }
    public void CompleteTrial(bool success)
    {
        levelController.timer = false;
        GetComponent<BoxCollider>().isTrigger = false;
        

        List<GameDirection> g = new List<GameDirection>(c.gameDirections);
        currentTrial = new TrialData(levelController.trialNumber, levelController.axis, c.numberOfDirections, levelController.trialTime, g, success);
        levelController.trialDatas.Add(currentTrial);
        c.numberOfDirections = 0;
        c.gameDirections.Clear();
        levelController.trialTime = 0;

        if (success)
        {
            sm.src.PlayOneShot(sm.successSound);
        }
        else
        { 
            sm.src.PlayOneShot(sm.failSound);
        }


        if (levelController.locationsList.Count == 0)
        {
            // WIN CONDITION GOES HERE
            
            
            levelController.sequencesCompleted++;
            if (levelController.sequencesCompleted == 1)
            {
                levelController.LoadNextSequence();
                Destroy(this.gameObject);
            }
            else if (levelController.sequencesCompleted == 2)
            {
                //finish game
                ExportData();
                //load ending scene
                FindObjectOfType<FadeInOut>().FadeToLevel("EndScene");
            }


            //double timeScore = System.Math.Round(levelController.elapsedTime, 2);
            //levelController.directionsText.text = "You finished the game in " + timeScore.ToString() + " seconds";

            

        }
        else
        {
            checkIfMoving.timer = 3f;
            Destroy(this.gameObject);
            levelController.SpawnPickup();
            c.Triangulate(c.sceneName);
        }
    }

    private void ExportData()
    {
        //Export the data to csv
        string path = Application.persistentDataPath;
        string exportFile = (DateTime.Now.Ticks).ToString() + "-records.csv";
        string exportPath = path + "\\Export\\" + exportFile;
        ExportTrialData.Write(exportPath, levelController.trialDatas);

        //Export data to xml
        var serializer = new XmlSerializer(typeof(DataCollection));
        var stream = new FileStream(Application.persistentDataPath + "\\export\\" + (DateTime.Now.Ticks).ToString() + "-records.xml", FileMode.Create);
        serializer.Serialize(stream, levelController.trialDatas);
    }

}
