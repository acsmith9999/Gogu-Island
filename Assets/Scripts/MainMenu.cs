using UnityEngine;
using UnityEngine.SceneManagement;

public  class MainMenu : MonoBehaviour
{
    public static bool startGeo = true;
    public static bool wordList1 = true;
    public Canvas instructions;

    private void Start()
    {
        instructions.enabled = false;
    }
    public static void NewGameStart()
    {
        if (SaveManager.activeSave != null)
        {
            SaveManager.activeSave = null;
        }
        SceneManager.LoadSceneAsync("StraightCoast");

    }

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

    public void HowToPlay()
    {
        instructions.enabled = true;
    }
    public void HideInstructions()
    {
        instructions.enabled = false;
    }
}
