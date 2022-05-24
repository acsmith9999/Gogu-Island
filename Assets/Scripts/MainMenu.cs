using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public  class MainMenu : MonoBehaviour
{

    public Canvas instructions;
    public InputField lengthText;

    private void Start()
    {
        instructions.enabled = false;
        lengthText.text = "15";
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
        Parameters.wordList1 = true;
    }
    public void list2()
    {
        Parameters.wordList1 = false;
    }
    public void Geocentric()
    {
        Parameters.startGeo = true;
    }
    public void Absolute()
    {
        Parameters.startGeo = false;
    }

    public void HowToPlay()
    {
        instructions.enabled = true;
    }
    public void HideInstructions()
    {
        instructions.enabled = false;
    }

    public void ReadStringInput()
    {
        if(int.TryParse(lengthText.text, out int result))
        {
            Parameters.sequenceLength = result;
            Debug.Log(Parameters.sequenceLength);
        }
        
    }
}
