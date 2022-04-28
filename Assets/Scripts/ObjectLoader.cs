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

    
    void Start()
    {
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
        if (MainMenu.startGeo)
        {
            fileToLoad = sequencesToLoad.Find(x => x.Contains("LandSea"));
        }
        else if(!MainMenu.startGeo) 
        { 
            fileToLoad = sequencesToLoad.Find(x => x.Contains("CoastUpDown")); 
        }
        levelController.axis = fileToLoad;
    }
    public void AddObjectsToLists()
    {
        locationsDict = new Dictionary<int, Locations>();
        ObjectDictionary dictionary = JsonUtility.FromJson<ObjectDictionary>(JsonFileReader.LoadJsonAsResource(fileToLoad));
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
        sequencesToLoad.Remove(fileToLoad);
        s = sequencesToLoad.ToArray();
        if (s.Length != 0)
        {
            fileToLoad = s[0];
        }
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
