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

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        c = FindObjectOfType<CalculateDirection>();
        sm = FindObjectOfType<SoundManager>();
        checkIfMoving = FindObjectOfType<CheckIfMoving>();
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
                GetComponent<BoxCollider>().enabled = false;
                CompleteTrial(true);

            }
        }
    }
    public void CompleteTrial(bool success)
    {
        //GetComponent<BoxCollider>().isTrigger = false;
        levelController.timer = false;

        List<GameDirection> g = new List<GameDirection>(c.gameDirections);
        currentTrial = new TrialData(levelController.trialNumber, levelController.axis, c.numberOfDirections, levelController.trialTime, g, success);
        levelController.trialDatas.Add(currentTrial);
        ExportTrialData.trialDatas.Add(currentTrial);
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
            // WIN CONDITION
            levelController.sequencesCompleted++;
            if (levelController.sequencesCompleted == 2)
            {
                levelController.LoadNextSequence();
                Destroy(this.gameObject);
            }
            else if (levelController.sequencesCompleted == 3)
            {
                //finish game
                ExportTrialData.ExportData(ExportTrialData.trialDatas);
                ExportTrialData.ExportMovement(ExportTrialData.movements);
                //load ending scene
                FindObjectOfType<FadeInOut>().FadeToLevel("EndScene");
            }
        }
        else
        {
            checkIfMoving.timer = 3f;

            foreach (GameObject pickup in GameObject.FindGameObjectsWithTag("pickup"))
            {
                Destroy(pickup);
            }
            Destroy(this);
            levelController.SpawnPickup();
            c.GetDirection(Parameters.numberOfAxes);

        }
    }
}
