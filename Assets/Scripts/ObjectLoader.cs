using System.Collections.Generic;
using UnityEngine;

public class ObjectLoader : MonoBehaviour
{
    public List<Locations> locationsList;

    public Dictionary<int, Locations> locationsDict;

    public string fileToLoad;
    public List<string> sequencesToLoad;
    private string[] s;

    private LevelController levelController;

    private int counter = 0;
    
    void Start()
    {
        fileToLoad = "Locations/Tutorial.json";
        levelController = FindObjectOfType<LevelController>();
        GetSequences();
    }
    public void GetSequences()
    {
        sequencesToLoad = new List<string>();
        ObjectDictionary dictionary = JsonUtility.FromJson<ObjectDictionary>(JsonFileReader.LoadJsonAsResource("Locations/Sequences.json"));
        foreach (string dictionaryItem in dictionary.locations)
        {
            sequencesToLoad.Add(dictionaryItem);
        }

    }
    public void AddObjectsToLists()
    {

        locationsDict = new Dictionary<int, Locations>();
        ObjectDictionary dictionary = JsonUtility.FromJson<ObjectDictionary>(JsonFileReader.LoadJsonAsResource(fileToLoad));
        foreach (string dictionaryItem in dictionary.locations)
        {
            if (counter < Parameters.sequenceLength)
            {
                try
                {
                    LoadObject(dictionaryItem);
                    counter++;
                }
                catch
                {
                    Debug.Log("no more items to load");
                }
            }
            
        }
        foreach (KeyValuePair<int, Locations> entry in locationsDict)
        {
            Locations temp = entry.Value;
            locationsList.Add(temp);
        }
        levelController.locationsList = locationsList;
        //sequencesToLoad.Remove(fileToLoad);
        //s = sequencesToLoad.ToArray();
        //if (s.Length != 0)
        //{
        //    fileToLoad = s[0];
        //}

        counter = 0;

    }
    public void WhichFileToLoad()
    {
        if (Parameters.startGeo)
        {
            //fileToLoad = sequencesToLoad.Find(x => x.Contains("LandSea"));
            if(levelController.sequencesCompleted == 1)
            {
                fileToLoad = sequencesToLoad[2];
            }
            else if(levelController.sequencesCompleted == 2)
            {
                fileToLoad = sequencesToLoad[1];
            }
        }
        else if (!Parameters.startGeo)
        {
            //fileToLoad = sequencesToLoad.Find(x => x.Contains("CoastUpDown"));
            if (levelController.sequencesCompleted == 1)
            {
                fileToLoad = sequencesToLoad[1];
            }
            else if (levelController.sequencesCompleted == 2)
            {
                fileToLoad = sequencesToLoad[2];
            }
        }
        levelController.axis = fileToLoad;
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
}
