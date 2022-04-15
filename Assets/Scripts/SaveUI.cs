using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SaveUI : MonoBehaviour
{
    public InputField iField;
    public GameObject pauseUI;
    public GameObject Prefab;
    public Transform Container;
    public GameObject scrollView;
    private Pause pause;
    private FirstPersonController player;
    public GameObject confirm;

    void Start()
    {
        pause = FindObjectOfType<Pause>();
        iField.gameObject.SetActive(false);
        scrollView.SetActive(false);
        confirm.SetActive(false);
        player = FindObjectOfType<FirstPersonController>();
    }

    public void ShowInputField()
    {
        pauseUI.SetActive(false);
        iField.gameObject.SetActive(true);
    }

    public void Save()
    {
        SaveManager.activeSave = new SaveData();

        SaveManager.activeSave.saveName = iField.text;
        SaveManager.activeSave.sceneToLoad = SceneManager.GetActiveScene().name;
        SaveManager.activeSave.position = player.transform.position;
        SaveManager.activeSave.rotation = player.transform.rotation;

        if (SaveManager.Save() == 1) 
        { 
            iField.gameObject.SetActive(false);
            pause.Resume();            
        }
        else { iField.text = "File name in use!"; }
    }
    public void Save(string fileName)
    {
        iField.text = fileName;
        confirm.SetActive(true);
        scrollView.SetActive(false);
        iField.enabled = false;
        iField.GetComponentInChildren<Button>().enabled = false;
        pause.Resume();
    }

    public void Overwrite()
    {
        SaveManager.activeSave = new SaveData();

        SaveManager.activeSave.saveName = iField.text;
        SaveManager.activeSave.sceneToLoad = SceneManager.GetActiveScene().name;
        SaveManager.activeSave.position = player.transform.position;
        SaveManager.activeSave.rotation = player.transform.rotation;
        
        SaveManager.Overwrite(SaveManager.activeSave.saveName);
        DestroyChildren();

        iField.enabled = true;
        iField.GetComponentInChildren<Button>().enabled = true;
        iField.gameObject.SetActive(false);
        confirm.SetActive(false);
        
        pause.Resume();
    }

    public void CancelOverwrite()
    {
        confirm.SetActive(false);
        iField.enabled = true;
        iField.GetComponentInChildren<Button>().enabled = true;
        scrollView.SetActive(true);

    }

    public void ShowSaveFiles()
    {
        scrollView.SetActive(true);
        pauseUI.SetActive(false);

        SaveManager.LoadListOfSaves();

        for (int i = 0; i < SaveManager.savedGames.Count; i++)
        {
            GameObject go = Instantiate(Prefab);
            go.SetActive(true);
            go.GetComponentInChildren<Text>().text = SaveManager.savedGames[i].saveName;
            go.transform.SetParent(Container.transform, false);
            int buttonIndex = i;
            if(EventSystem.current.currentSelectedGameObject.name == "LoadGame")
            {
                if (SceneManager.GetActiveScene().name != "MainMenu")
                {
                    go.GetComponent<Button>().onClick.AddListener(() => Load(buttonIndex)) ;
                }
                else { go.GetComponent<Button>().onClick.AddListener(() => LoadFromMainMenu(buttonIndex)); }
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "SaveGame")
            {
                ShowInputField();
                go.GetComponent<Button>().onClick.AddListener(() => Save(go.GetComponentInChildren<Text>().text));
            }
        }
    }

    public void HideSaveFiles()
    {
        scrollView.SetActive(false);
        pauseUI.SetActive(true);
        foreach (Transform button in Container)
        {
            Destroy(button.gameObject);
        }
        iField.gameObject.SetActive(false);
    }
    public void LoadFromMainMenu(int buttonIndex)
    {
        SaveManager.Load(buttonIndex);
        SceneManager.LoadSceneAsync(SaveManager.activeSave.sceneToLoad);
    }
    public void Load(int buttonIndex)
    {
        SaveManager.Load(buttonIndex);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadSceneAsync(SaveManager.activeSave.sceneToLoad);

        DestroyChildren();

        scrollView.SetActive(false);
        pause.Resume();
    }

    private void DestroyChildren()
    {
        if (Container.childCount > 0)
        {
            foreach (Transform button in Container)
            {
                Destroy(button.gameObject);
            }
        }
    }
}
