using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public  class MainMenu : MonoBehaviour
{
    public static bool startGeo = true;
    public static bool wordList1 = true;

    public static void NewGameStart()
    {
        if (SaveManager.activeSave != null)
        {
            SaveManager.activeSave = null;
        }
        SceneManager.LoadSceneAsync("StraightCoast");

    }

    //public static void StartAbs()
    //{
    //    startGeo = false;
    //    SceneManager.LoadScene("StraightCoast");
    //}

    //public static void StartGeo()
    //{
    //    startGeo = true;
    //    SceneManager.LoadScene("StraightCoast");
    //}

    //public void ToggleWordList()
    //{
    //    string selected = FindObjectOfType<ToggleGroup>().ActiveToggles().First().GetComponentsInChildren<Text>().First(t=>t.name=="Label").text;
    //    Debug.Log(selected);
    //    if (selected == "Wordlist1")
    //    {
    //        wordList1 = true;
    //    }
    //    else { wordList1 = false; }
    //}

    public void list1()
    {
        wordList1 = true;
    }
    public void list2()
    {
        wordList1 = false;
    }
    public void Geocentric()
    {
        startGeo = true;
    }
    public void Absolute()
    {
        startGeo = false;
    }
}
