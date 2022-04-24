using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

public class LevelController : MonoBehaviour
{
    public GameObject pickup;

    public Text directionsText;

    public bool timer = false;
    public float elapsedTime;
    public float trialTime;

    private ObjectLoader objectLoader;
    private CalculateDirection calculateDirection;
    public List<Locations> locationsList;

    public int trialNumber = 1;
    public int sequencesCompleted = 0;
    public bool geoFirstSeq;

    public string axis;

    //export this
    public DataCollection trialDatas;

    // Start is called before the first frame update
    void Start()
    {
        objectLoader = FindObjectOfType<ObjectLoader>();
        calculateDirection = FindObjectOfType<CalculateDirection>();
        trialDatas = new DataCollection();
        elapsedTime = 0;
        trialTime = 0;
        geoFirstSeq = MainMenu.startGeo;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) && locationsList.Count > 0 && GameObject.FindGameObjectsWithTag("pickup").Length == 0)
        {
            SpawnPickup();
        }

        //maybe delete
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("pickup"))
            {
                Destroy(g);
            }
            //if (locationsList.Count ==0 && objectLoader.inRange == true)
            //{
            //    objectLoader.AddObjectsToLists();
            //}
            // The only way at the moment to repopulate the items is by leaving the trigger area
            directionsText.enabled = false;
            timer = false;
            //elapsedTime = 0;
        }

        if (timer)
        {
            elapsedTime += Time.deltaTime;
            trialTime += Time.deltaTime;
        }
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    WinGame();
        //}

    }
    private void LateUpdate()
    {
        if (trialTime >= 200)
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

            int index = Random.Range(0, locationsList.Count);
            calculateDirection.target = Instantiate(pickup, locationsList.ElementAt(index).location, Quaternion.identity);
            
            calculateDirection.targetRef = calculateDirection.target.transform.position;
            directionsText.text = "";
            directionsText.enabled = true;

            //directionsText.enabled = true;
            //directionsText.text = locationsList.ElementAt(index).description;

            locationsList.RemoveAt(index);
        }
        else { Debug.Log("No more locations to spawn"); }
    }

    public void SpawnExample()
    {
        Instantiate(pickup, new Vector3(391.8f, 33.43f, 488.1f), Quaternion.identity);
    }
    //public void WinGame()
    //{
    //    FindObjectOfType<FadeInOut>().FadeToLevel("EndScene");
    //}
    public void LoadNextSequence()
    {
        PixelCrushers.DialogueSystem.DialogueManager.StartConversation("RobOnSequenceEnd");
        DialogueLua.SetVariable("GameFinished", true);

        trialNumber = 0;
        axis = objectLoader.fileToLoad;
    }
}
