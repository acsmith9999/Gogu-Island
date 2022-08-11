using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private LevelController levelController;
    private CalculateDirection c;
    private TrialData currentTrial;
    private SoundManager sm;
    private CheckIfMoving checkIfMoving;
    private bool isColliding = false;
    private int i = 0;
    ParticleSystem particleSystem;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        c = FindObjectOfType<CalculateDirection>();
        sm = FindObjectOfType<SoundManager>();
        checkIfMoving = FindObjectOfType<CheckIfMoving>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (i == 0)
            {
                i++;
                if (isColliding) return;
                isColliding = true;
                sm.src.PlayOneShot(sm.successSound);
                particleSystem.Play();
                CompleteTrial(true);
            }
        }
    }
    public void CompleteTrial(bool success)
    {
        levelController.timer = false;

        List<GameDirection> g = new List<GameDirection>(c.gameDirections);
        currentTrial = new TrialData(levelController.trialNumber, levelController.sequencesCompleted, levelController.axis, c.numberOfDirections, levelController.trialTime, g, success);
        levelController.trialDatas.Add(currentTrial);
        ExportTrialData.trialDatas.Add(currentTrial);
        c.numberOfDirections = 0;
        c.gameDirections.Clear();
        levelController.trialTime = 0;

        if (!success)
        {
            sm.src.PlayOneShot(sm.failSound);
        }


        if (levelController.locationsList.Count == 0)
        {
            // WIN CONDITION
            levelController.sequencesCompleted++;
            if (levelController.sequencesCompleted == 2)
            {
                levelController.LoadNextSequence();
                Destroy(this.gameObject,2);
            }
            else if (levelController.sequencesCompleted == 3)
            {
                //finish game
                ExportTrialData.ExportData(ExportTrialData.trialDatas);
                ExportTrialData.ExportMovement(ExportTrialData.movements);
                ExportTrialData.ExportGaze(ExportTrialData.gazeDatas);
                //load ending scene
                FindObjectOfType<FadeInOut>().FadeToLevel("EndScene");
            }
        }
        else
        {
            checkIfMoving.timer = 3f;

            //foreach (GameObject pickup in GameObject.FindGameObjectsWithTag("pickup"))
            //{
            //    Destroy(pickup);
            //}
            
            Invoke("SpawnPickup",2f);
            Destroy(this.gameObject,2);
        }
    }

    private void SpawnPickup()
    {
        levelController.SpawnPickup();
    }
}
