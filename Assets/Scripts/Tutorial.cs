using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialBoundaries, targetRing;
    private LevelController lc;
    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        lc = FindObjectOfType<LevelController>();
        tutorialBoundaries = GameObject.FindGameObjectWithTag("tutBoundary");
        tutorialBoundaries.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered == false)
        {
            if (other.CompareTag("Player"))
            {
                targetRing.SetActive(false);
                lc.SpawnTutorial();
                triggered = true;
            }
        }

    }

    public void TutorialBoundaries()
    {
        tutorialBoundaries.SetActive(!tutorialBoundaries.activeInHierarchy);
    }

}
