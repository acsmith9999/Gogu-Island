using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLoader : MonoBehaviour
{
    public List<Locations> locationsList;

    public Dictionary<int, Locations> locationsDict;

    public string terrainName, fileToLoad;

    public bool inRange = false;

    private LevelController levelController;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        //if (locationsList.Count == 0)
        //{
            AddObjectsToLists();
        //}
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (locationsList.Count == 0)
    //    {
    //        AddObjectsToLists();
    //    }
    //    inRange = true;
    //    levelController.directionsText.enabled = true;
    //    levelController.directionsText.text = terrainName;

    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    foreach (GameObject g in GameObject.FindGameObjectsWithTag("pickup"))
    //    {
    //        Destroy(g);
    //    }
    //    locationsList.Clear();
    //    locationsDict.Clear();
    //    inRange = false;
    //    levelController.directionsText.enabled = false;
    //    levelController.locationsList = locationsList;
    //    levelController.timer = false;
    //    levelController.elapsedTime = 0;
    //}

    public void AddObjectsToLists()
    {
        locationsDict = new Dictionary<int, Locations>();
        ObjectDictionary dictionary = JsonUtility.FromJson<ObjectDictionary>(JsonFileReader.LoadJsonAsResource("Locations/" + fileToLoad + ".json"));
        foreach (string dictionaryItem in dictionary.locations)
        {
            LoadObject(dictionaryItem);
        }
        foreach (KeyValuePair<int, Locations> entry in locationsDict)
        {
            Locations temp = entry.Value;
            locationsList.Add(temp);
        }
        levelController.locationsList = locationsList;
    }

    public void LoadObject(string path)
    {
        string myLoadedLocation = JsonFileReader.LoadJsonAsResource(path);
        Locations myObject = JsonUtility.FromJson<Locations>(myLoadedLocation);

        if (locationsDict.ContainsKey(myObject.objectId))
        {
            Debug.LogWarning("Object " + myObject.objectId + " Key already exists");
        }
        else
        {
            locationsDict.Add(myObject.objectId, myObject);
        }

    }
    public void ShowExampleTarget()
    {
        levelController.SpawnExample();
    }
    public void HideExampleTarget()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("pickup"))
        {
            Destroy(g);
        }
    }
}
