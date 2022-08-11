using System.Collections.Generic;
using UnityEngine;
using System.IO;

using System.Xml.Serialization;
using UnityStandardAssets.Characters.FirstPerson;

public static class SaveManager
{
    public static SaveData activeSave;
    public static List<SaveData> savedGames = new List<SaveData>();


    public static void LoadListOfSaves()
    {
        savedGames.Clear();

        string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.save");
        foreach(string s in filePaths)
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(s, FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            savedGames.Add(activeSave);
        }
    }

    public static int Save()
    {

        if (File.Exists(Application.persistentDataPath + "/" + activeSave.saveName + ".save"))
        {
            Debug.Log("Filename in use");
            return 0;
        }
        else { 
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(Application.persistentDataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
            serializer.Serialize(stream, activeSave);
            stream.Close();
            savedGames.Add(activeSave);
            Debug.Log("saved");
            return 1;
        }
    }
    public static void Overwrite(string fileName)
    {
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(Application.persistentDataPath + "/" + fileName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();
        savedGames.Add(activeSave);
        Debug.Log("saved");
    }

    public static void Load(int index)
    {
        activeSave = savedGames[index];
    }


}
 
