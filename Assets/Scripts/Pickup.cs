
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private LevelController levelController;
    private AudioSource src;
    public AudioClip successSound;
    private CalculateDirection c;

    private TrialData currentTrial;
    

    private void Start()
    {
        src = GetComponent<AudioSource>();
        levelController = FindObjectOfType<LevelController>();
        c = FindObjectOfType<CalculateDirection>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        currentTrial = new TrialData(levelController.trialNumber, c.numberOfDirections);
        levelController.trialDatas.Add(currentTrial);

        c.numberOfDirections = 0;


        //feedback of some kind - particle explosion maybe?

        

        levelController.timer = false;
        if (levelController.locationsList.Count == 0)
        {
            // WIN CONDITION GOES HERE

            //double timeScore = System.Math.Round(levelController.elapsedTime, 2);
            //levelController.directionsText.text = "You finished the game in " + timeScore.ToString() + " seconds";
        }
        else
        {
            levelController.SpawnPickup();
            //give next direction
            c.Triangulate(c.sceneName);
        }
    }




}
