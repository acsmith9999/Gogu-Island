using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public  class MainMenu : MonoBehaviour
{

    public Canvas instructions;
    public InputField lengthText, radiusText, participantNumberText;

    private void Start()
    {
        instructions.enabled = false;
        lengthText.text = "default";
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //ExportTrialData.ExportData(ExportTrialData.trialDatas);
            //if (!Directory.Exists(Application.streamingAssetsPath + "/Export"))
            //{
            //    Directory.CreateDirectory(Application.streamingAssetsPath + "/Export");
            //}
            //string path = Application.streamingAssetsPath + "\\Export\\";
            //if (!Directory.Exists(path + Parameters.participantNo))
            //{
            //    Directory.CreateDirectory(Application.streamingAssetsPath + "/Export/" + Parameters.participantNo);
            //}
            //path = path + $"\\{Parameters.participantNo}\\";
//            // Folder, where a file is created.  
//            // Make sure to change this folder to your own folder  
//            string folder = path;
//            // Filename  
//            string fileName = Parameters.participantNo + " CSharpCornerAuthors.txt";
//            // Fullpath. You can direct hardcode it if you like.  
//            string fullPath = folder + fileName;
//            // An array of strings  
//            string[] authors = {"Mahesh Chand", "Allen O'Neill", "David McCarter",
//"Raj Kumar", "Dhananjay Kumar"};
//            // Write array of strings to a file using WriteAllLines.  
//            // If the file does not exists, it will create a new file.  
//            // This method automatically opens the file, writes to it, and closes file  
//            File.WriteAllLines(fullPath, authors);
//            // Read a file  
//            string readText = File.ReadAllText(fullPath);
//            Console.WriteLine(readText);
        }
    }
    public static void NewGameStart()
    {
        if (SaveManager.activeSave != null)
        {
            SaveManager.activeSave = null;
        }
        SceneManager.LoadSceneAsync("StraightCoast");
    }

    public void G1()
    {
        list1();
        Geocentric();
        Debug.Log(Parameters.startGeo);
        Debug.Log(Parameters.wordList1);
    }
    public void G2()
    {
        list2();
        Geocentric();
        Debug.Log(Parameters.startGeo);
        Debug.Log(Parameters.wordList1);
    }
    public void A1()
    {
        list1();
        Absolute();
        Debug.Log(Parameters.startGeo);
        Debug.Log(Parameters.wordList1);
    }
    public void A2()
    {
        list2();
        Absolute();
        Debug.Log(Parameters.startGeo);
        Debug.Log(Parameters.wordList1);
    }

    private void list1()
    {
        Parameters.wordList1 = true;
    }
    private void list2()
    {
        Parameters.wordList1 = false;
    }
    private void Geocentric()
    {
        Parameters.startGeo = true;
    }
    private void Absolute()
    {
        Parameters.startGeo = false;
    }
    public void MaleVoice()
    {
        Parameters.helpGender = 0;
    }
    public void FemaleVoice()
    {
        Parameters.helpGender = 1;
    }
    public void PlainProsody()
    {
        Parameters.prosody = 0;
    }
    public void InflectedProsody()
    {
        Parameters.prosody = 1;
    }
    public void HowToPlay()
    {
        instructions.enabled = true;
    }
    public void HideInstructions()
    {
        instructions.enabled = false;
    }

    public void ReadStringInputLength()
    {
        if(int.TryParse(lengthText.text, out int result))
        {
            Parameters.sequenceLength = result;
        }
    }
    public void ReadStringInputRadius()
    {
        if (int.TryParse(radiusText.text, out int result))
        {
            Parameters.targetRadius = result;
        }
    }
    public void ReadParticipantNumber()
    {
        if (participantNumberText.text != null)
        {
            Parameters.participantNo = "p" + participantNumberText.text;
        }
    }
}
