using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LevelController : MonoBehaviour
{
    public GameObject pickup;

    public Text directionsText;

    public bool timer = false;
    public float elapsedTime;

    private ObjectLoader objectLoader;
    private CalculateDirection calculateDirection;
    public List<Locations> locationsList;

    public int trialNumber = 0;

    [SerializeField]
    public DataCollection trialDatas;

    // Start is called before the first frame update
    void Start()
    {
        objectLoader = FindObjectOfType<ObjectLoader>();
        calculateDirection = FindObjectOfType<CalculateDirection>();
        trialDatas = new DataCollection();
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Return) && locationsList.Count > 0 && GameObject.FindGameObjectsWithTag("pickup").Length == 0)
        //{
        //    SpawnPickup();
        //}

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
            elapsedTime = 0;
        }

        if (timer)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void SpawnPickup() {
        timer = true;

        trialNumber++;

        int index = Random.Range(1, locationsList.Count);
        calculateDirection.target = Instantiate(pickup, locationsList.ElementAt(index).location, Quaternion.identity);
        calculateDirection.targetRef = calculateDirection.target.transform.position;
        directionsText.text = "";
        directionsText.enabled = true;

        //directionsText.enabled = true;
        //directionsText.text = locationsList.ElementAt(index).description;

        locationsList.RemoveAt(index);
        
    }

    public void SpawnExample()
    {
        Instantiate(pickup, locationsList.ElementAt(0).location, Quaternion.identity);
    }
}
