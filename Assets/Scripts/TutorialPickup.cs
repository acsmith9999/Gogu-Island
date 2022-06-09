using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPickup : MonoBehaviour
{
    private SoundManager sm;
    private LevelController levelController;
    private CalculateDirection c;
    private TrialData currentTrial;

    private void Start()
    {
        sm = FindObjectOfType<SoundManager>();
        levelController = FindObjectOfType<LevelController>();
        c = FindObjectOfType<CalculateDirection>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelController.timer = false;
            sm.src.PlayOneShot(sm.successSound);

            List<GameDirection> g = new List<GameDirection>(c.gameDirections);
            currentTrial = new TrialData(levelController.trialNumber, levelController.sequencesCompleted, levelController.axis, c.numberOfDirections, levelController.trialTime, g, true);
            levelController.trialDatas.Add(currentTrial);
            ExportTrialData.trialDatas.Add(currentTrial);
            c.numberOfDirections = 0;
            c.gameDirections.Clear();
            levelController.trialTime = 0;

            if (levelController.locationsList.Count == 0)
            {
                Destroy(this.gameObject);
                levelController.sequencesCompleted++;
                levelController.TutorialFinished();
            }
            else
            {
                Destroy(this.gameObject);
                levelController.SpawnTutorial();
            }
        }
    }
}
