using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPickup : MonoBehaviour
{
    private SoundManager sm;
    private LevelController levelController;
    private CalculateDirection c;

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
