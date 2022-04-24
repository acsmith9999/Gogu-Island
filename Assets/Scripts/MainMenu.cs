using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public  class MainMenu : MonoBehaviour
{
    public static bool startGeo;
    public static bool wordList1 = true;

    public static void NewGameStart()
    {
        if (SaveManager.activeSave != null)
        {
            SaveManager.activeSave = null;
        }
        SceneManager.LoadSceneAsync("StraightCoast");

    }

    public static void StartAbs()
    {
        startGeo = false;
        SceneManager.LoadScene("StraightCoast");
    }

    public static void StartGeo()
    {
        startGeo = true;
        SceneManager.LoadScene("StraightCoast");
    }

    public void ToggleWordList()
    {
        wordList1 = !wordList1;
    }

}
