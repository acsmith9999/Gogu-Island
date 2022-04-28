using System.Collections.Generic;
using UnityEngine;

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
            // WIN CONDITION
            levelController.sequencesCompleted++;
            if (levelController.sequencesCompleted == 1)
            {
                levelController.LoadNextSequence();
                Destroy(this.gameObject);
            }
            else if (levelController.sequencesCompleted == 2)
            {
                //finish game
                //levelController.ExportData();
                //load ending scene
                FindObjectOfType<FadeInOut>().FadeToLevel("EndScene");
            }
        }
        else
        {
            checkIfMoving.timer = 3f;
            Destroy(this.gameObject);
            foreach(GameObject pickup in GameObject.FindGameObjectsWithTag("pickup"))
            {
                Destroy(pickup);
            }
            levelController.SpawnPickup();
            c.GetDirection(1);
        }
    }
}
