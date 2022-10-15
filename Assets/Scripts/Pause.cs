using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool gamePaused;
    public GameObject pauseMenuUI;
    public Image cursor;
    MouseLook m;

    private void Start()
    {
        //cursor.enabled = false;
        pauseMenuUI.SetActive(false);
        m = GameObject.FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;

        //cursor.enabled = false;
        //enableCursor();
        //Cursor.visible = true;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        //StartCoroutine(ScaleTime(1.0f, 0.0f, 0.5f));
        Time.timeScale = 0f;
        gamePaused = true;

        //cursor.enabled = true;
        //cursor.transform.position = Input.mousePosition;
        //lockCursor();
    }
    public void lockCursor()
    {
        m.SetCursorLock(false);
        Cursor.visible = false;
    }
    public void enableCursor()
    {
        m.lockCursor = !m.lockCursor;
    }

    IEnumerator ScaleTime(float start, float end, float time)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < time)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / time);
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        Time.timeScale = end;
    }

    public void MainMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
