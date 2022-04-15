using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pauseUI;

    public void NewGameStart()
    {
        if (SaveManager.activeSave != null)
        {
            SaveManager.activeSave = null;
        }
        SceneManager.LoadSceneAsync("StraightCoast");

    }

}
